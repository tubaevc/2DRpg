using UnityEngine;

public class BasicEnemy : Enemy
{
    public float detectionRadius = 5f;
    public float attackRadius = 1f;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private Rigidbody2D rb;
    private bool isChasing = false;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer <= attackRadius)
            {
                Attack();
            }
            else
            {
                isChasing = true;
                Move();
            }
        }
        else
        {
            isChasing = false;
            StopChasing();
        }
    }

    public override void Move()
    {
        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            anim.SetBool("Run", true);
        }
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            anim.SetBool("Run", false);
            anim.SetTrigger("Attack");
            // Player health handling
            lastAttackTime = Time.time;
        }
    }

    private void StopChasing()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Idle",true);
        rb.velocity = Vector2.zero;
    }

    public override void GetDamage(float dmg)
    {
        if (currentHealth <= 0) return;

        currentHealth -= dmg;
        Debug.Log("enemy health:"+currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool("Dead", true);
            Destroy(gameObject);
        }
    }
}
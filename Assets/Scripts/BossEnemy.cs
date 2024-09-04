using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : Enemy
{
    public Image backgroundImage;
    public Image healthImage;
  
    public float detectionRadius = 5f;
    public float attackRadius = 1f;

    private PlayerHealth playerHealth;

    public float attackCooldown = 15f;
    private float lastAttackTime;
    private Rigidbody2D rb;
    private bool isChasing = false;
    private bool isDead = false;
    
    
    public GameObject keyPrefab;
    public Transform keyDropPoint;
    public int keyIDToDrop;
    private Key key;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Health bar update
        if (healthImage.fillAmount != backgroundImage.fillAmount)
        {
            backgroundImage.fillAmount = Mathf.Lerp(backgroundImage.fillAmount, healthImage.fillAmount, 0.01f);
        }

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

    public override void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
           // anim.SetBool("Run", false);
            anim.SetTrigger("Attack");
            if (player != null)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.GetDamage(20f);
                }
            }

            lastAttackTime = Time.time;
        }
    }


    public override void Move()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

      //  anim.SetBool("Run", true);
    }

    private void StopChasing()
    {
        //anim.SetBool("Run", false);

        rb.velocity = Vector2.zero;
    }

    public override void GetDamage(float dmg)
    {
        if (isDead) return;
        currentHealth -= dmg;
        //anim.SetTrigger("Hit");
       // transform.Translate(Vector2.right * 1.2f);
        Debug.Log("Enemy current health: " + currentHealth);
        healthImage.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        if (isDead) return;

      //  isDead = true;
       // StopChasing();
       // anim.ResetTrigger("Attack");
       // anim.SetBool("Run", false);
        anim.SetBool("Dead", true);
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        if (rb != null) rb.isKinematic = true;
        Destroy(gameObject);
        DropKey();
    }

    private void DropKey()
    {
        if (keyPrefab != null && keyDropPoint != null)
        {
            GameObject droppedKey = Instantiate(keyPrefab, keyDropPoint.position, Quaternion.identity);
            Key keyComponent = droppedKey.GetComponent<Key>();
            if (keyComponent != null)
            {
                keyComponent.keyID = keyIDToDrop;
            }

            Debug.Log("Boss " + keyIDToDrop + "  dropped num of key !");
        }
        else
        {
            Debug.LogError("unassigned key prefab or keydroptransform");
        }
    }
}
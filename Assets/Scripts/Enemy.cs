using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float maxHealth;
    protected float currentHealth;
    public float moveSpeed;
    public Transform player;
    public Animator anim;
    public abstract void Move();
    public abstract void Attack();

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void GetDamage(float dmg)
    {
        if (currentHealth <= 0) return;

        currentHealth -= dmg;
        currentHealth = Mathf.Max(currentHealth, 0); 
        anim.SetTrigger("Hit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        anim.SetBool("Dead", true);
        Destroy(gameObject);
    }
}
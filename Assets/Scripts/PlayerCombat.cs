using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform attackPoint;
    public int attackDamage = 40;
    private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    private void Update()
    {
        if (Time.time>= nextAttackTime)
        {
        if (Input.GetMouseButtonDown(0))
        {
                _animator.SetTrigger("Attack");

                Attack();

                 nextAttackTime = Time.time + 1f / attackRate;
        }
            if (Input.GetMouseButtonDown(1))
            {
                _animator.SetTrigger("Spell2");

                Attack();

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void Attack()
    {
        

        //detect
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            enemy.GetDamage(attackDamage);
           // enemy.GetComponent<BossEnemy>().GetDamage(attackDamage);
           //  enemy.GetComponent<BasicEnemy>().GetDamage(attackDamage);
            Debug.Log("we hit"+enemy.name);
        }
    }
    public void TakeDamage(float damage)
    {
        GetComponent<PlayerHealth>().GetDamage(damage);
        _animator.SetTrigger("Hurt");

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}

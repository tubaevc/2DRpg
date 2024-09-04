using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image backgroundImage;
    public Image healthImage;
    public float maxHealth;
    float currentHealth;
    [SerializeField] private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetDamage(10);
        }
        if(healthImage.fillAmount != backgroundImage.fillAmount)
        {
            backgroundImage.fillAmount = Mathf.Lerp(backgroundImage.fillAmount, healthImage.fillAmount, 0.01f);
        }
    }
    public void GetDamage(float dmg)
    {
        currentHealth -= dmg;
        animator.SetTrigger("Hurt");
        transform.Translate(Vector2.left * 0.2f);
        Debug.Log("player currenthealth:" + currentHealth);
        healthImage.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Death",true);
        Debug.Log("Player died");
    }

}

using System.Collections;
using UnityEngine;

public class FireBreathe : MonoBehaviour
{
    public float distance = 7f; 
    public float speed = 7f; 

    void Start()
    {
        StartCoroutine(MoveAndDestroy());
    }
    private void OnTriggerEnter(Collider other)
    {  Debug.Log("Triggered with: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            BossEnemy enemyHealth = other.GetComponent<BossEnemy>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(dmg:25f);
            }
        }
    }
    IEnumerator MoveAndDestroy()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + transform.right * distance;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject); 
    }
}
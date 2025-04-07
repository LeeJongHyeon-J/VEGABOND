using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int weaponDamage = 1;
    public float attackCooldown = 1f;
    private bool canDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDamage)
        {
            PlayerHealthUI playerHealth = collision.GetComponent<PlayerHealthUI>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(weaponDamage);
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private System.Collections.IEnumerator AttackCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(attackCooldown);
        canDamage = true;
    }
}
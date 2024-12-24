using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
    }
}

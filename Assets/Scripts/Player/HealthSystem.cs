using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    [SerializeField] GameObject healthBar;
    GameObject healthBarHandle;
    void Start()
    {
        health = maxHealth;
        foreach(Transform child in healthBar.transform)
        {
            if(child.gameObject.name == "Handle Slide Area")
            {
                healthBarHandle = child.GetChild(0).gameObject;
            }
        }
    }
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
        gameObject.GetComponent<PlayerAnimation>().animator.SetBool("Dead", true);    
        gameObject.GetComponent<PixelPerfectMovement>().enabled = false;
        Debug.Log("Player died");
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        //handling health bar
        healthBarHandle.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
        healthBar.GetComponent<Slider>().value = health;
    }
}

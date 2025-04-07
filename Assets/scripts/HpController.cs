using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageAmount = 10f;

    public event System.Action OnDeath;
    public event System.Action<float> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object's own tag is "Enemy"
        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Follower"))
        {
            Debug.Log("Damage");
            TakeDamage(damageAmount);
        }
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        // Handle death logic here, such as disabling the GameObject or playing a death animation
        Debug.Log(gameObject.name + " has died.");
        OnDeath?.Invoke();
    }
}

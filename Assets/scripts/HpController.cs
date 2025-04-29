using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageAmount = 10f;

    public GameObject targetGameObject; // Reference to the GameObject
    private SpriteRenderer spriteRendererComponent;
    public event System.Action OnDeath;
    public event System.Action<float> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        if (targetGameObject != null)
        {
            // Get the component you want to disable
            // For example, disabling a Renderer component
            spriteRendererComponent = targetGameObject.GetComponent<SpriteRenderer>();
            if (spriteRendererComponent == null)
            {
                Debug.LogError("SpriteRenderer component not found on the target GameObject.");
            }
        }
        else
        {
         //  Debug.LogError("Target GameObject is not assigned.");
        }
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
    void Update()
    {
        // Check the health condition
        if (currentHealth < 90)
        {
            spriteRendererComponent.enabled = true;
        }
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

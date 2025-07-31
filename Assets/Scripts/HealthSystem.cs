using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private TextMeshProUGUI healthText; // Reference to the UI text element for displaying health
    [SerializeField] private float invincibilityDuration = 1f; // Duration of invincibility after taking damage
    private float currentHealth;
    private bool isAlive = true;

    private bool cantBeDamaged = false;

    void Start()
    {
        currentHealth = maxHealth;
        if(gameObject.tag == "Player")
        {
            UpdateUI();
        }

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float incomingDamage)
    {
        if (cantBeDamaged && isAlive)
            return;

        currentHealth -= incomingDamage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        if(gameObject.tag == "Player")
        {
            UpdateUI();
        }

        HandleInvincibility(invincibilityDuration); // Example duration for invincibility after taking damage

    }

    public void Die()
    {
        isAlive = false;
        
        if(gameObject.tag != "Player") //if not the player destroy
        {
            Destroy(gameObject);
        }
        if(gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }

    }

    public void RestoreHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        healthText.SetText(currentHealth.ToString());
        healthText.color = Color.Lerp(Color.red, Color.white, currentHealth / maxHealth);
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void HandleInvincibility(float duration)
    {
        cantBeDamaged = true;
        StartCoroutine(EndInvincibility(duration));
    }

    private IEnumerator EndInvincibility(float duration)
    {
        yield return new WaitForSeconds(duration);
        cantBeDamaged = false;

    }
}

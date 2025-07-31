using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    private bool isAlive = true;

    private bool cantBeDamaged = false;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateUI();
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


        UpdateUI();
    }

    public void Die()
    {
        isAlive = false;
        
        if(gameObject.tag != "Player") //if not the player destroy
        {
            Destroy(gameObject);
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

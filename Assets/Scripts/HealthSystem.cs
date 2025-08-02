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

    [Header("Dying")]
    [SerializeField] GameOverSCreen gameOverScreen;
    [SerializeField] private RingController ringController; // Name of the game over scene
    [SerializeField] private PlayerMovement playerMovement; // Reference to the player movement script
    [SerializeField] private ProjectileWeapon projectileWeapon; // Reference to the projectile weapon script (if needed for player)

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // Audio source to play sounds
    [SerializeField] private AudioClip hurtSound; // Sound to play when the player takes damage
    [SerializeField] private AudioClip deathSound; // Sound to play when the player dies
    private float currentHealth;
    private bool isAlive = true;

    private bool cantBeDamaged = false;


    private void Awake()
    {
        if(gameObject.tag == "Player" || gameObject.tag == "Boss")
        {
            gameOverScreen = FindFirstObjectByType<GameOverSCreen>(FindObjectsInactive.Include);
            if (gameOverScreen == null)
            {
                Debug.LogError("GameOverSCreen not found in the scene. Please ensure it is present.");
            }
        }
        
    }

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


        if(gameObject.tag == "Player")
        {
            UpdateUI();
            audioSource.PlayOneShot(hurtSound); // Play hurt sound
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        HandleInvincibility(invincibilityDuration); // Example duration for invincibility after taking damage

    }

    public void Die()
    {
        isAlive = false;

        if (gameObject.tag == "Boss") //if boss win game
        {
            gameOverScreen.Victory();
            //playerMovement.enabled = false; // Disable player movement
            //ringController.enabled = false; // Disable ring controller
            audioSource.PlayOneShot(deathSound); // Play death sound
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Player")
        {
            gameOverScreen.GameOver();
            playerMovement.enabled = false; // Disable player movement
            ringController.enabled = false; // Disable ring controller
            projectileWeapon.enabled = false; // Disable projectile weapon
        }
        else //if not the player destroy
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

using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))] // Ensure the GameObject has a Rigidbody2D component
[RequireComponent(typeof(Collider2D))] // Ensure the GameObject has a Collider2D component
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Rigidbody component for physics interactions
    [SerializeField] private float damage = 10f; // Initial direction of the projectile
    [SerializeField] private float speed = 10f; // Speed of the projectile
    [SerializeField] private float lifetime = 5f; // Lifetime of the projectile in seconds
    [SerializeField] private AudioClip hitSound; // Sound to play when the projectile hits something
    [SerializeField] private AudioSource audioSource; // Audio source to play the sound


    private GameObject parent;





    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the specified direction
        if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }
        /*// Destroy the projectile after its lifetime has elapsed
        if (Time.time - creationTime > lifetime)
        {
            Destroy(gameObject);
        }*/

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == parent) //if projectile collides with parent ignore it
             return;
        Debug.Log("Projectile hit: " + collision.gameObject.name);

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            // If the projectile hits an object with a HealthSystem, apply damage
            healthSystem.TakeDamage(damage); // Example damage value
        }

        // Play the hit sound if it is set
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            
        }
        // Destroy the projectile on collision
        Destroy(gameObject);
    }

    public void SetParent(GameObject parent)
    {
        this.parent = parent;
    }


}

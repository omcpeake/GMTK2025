using UnityEngine;

public class BouncingProjtectile : Projectile
{
    [SerializeField] private int bounceCount = 3; // Number of bounces before destruction
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == owner) // If projectile collides with parent ignore it
            return;
        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            // If the projectile hits an object with a HealthSystem, apply damage
            healthSystem.TakeDamage(damage); // Example damage value
        }
        // Reflect the projectile's direction based on the collision normal
        Vector2 reflectDirection = Vector2.Reflect(rb.linearVelocity.normalized, collision.contacts[0].normal);
        rb.linearVelocity = reflectDirection * speed; // Update the velocity to the new direction
        // Play the hit sound if it is set
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Decrease the bounce count
        bounceCount--;

        // If the bounce count reaches zero, destroy the projectile
        if (bounceCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}

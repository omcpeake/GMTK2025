using UnityEngine;

public class BouncingProjtectile : Projectile
{
    [SerializeField] private int bounceCount = 3; // Number of bounces before destruction
    

    protected override void PostCollision(Collision2D collision)
    {
        // Decrease the bounce count
        bounceCount--;

        // If the bounce count reaches zero, destroy the projectile
        if (bounceCount <= 0)
        {
            Destroy(gameObject);
        }

        // Reflect the projectile's direction based on the collision normal
        /*Vector2 reflectDirection = Vector2.Reflect(rb.linearVelocity.normalized, collision.contacts[0].normal);
        rb.linearVelocity = reflectDirection * speed; // Update the velocity to the new direction*/
        Vector2 reflectDirection = Vector2.Reflect(transform.right, collision.contacts[0].normal);
        transform.right = reflectDirection; // Update the projectile's direction to the reflected direction


    }
}

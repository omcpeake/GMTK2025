using Unity.VisualScripting;
using UnityEngine;
//[RequireComponent(typeof(Rigidbody2D))] // Ensure the GameObject has a Rigidbody2D component
[RequireComponent(typeof(Collider2D))] // Ensure the GameObject has a Collider2D component
public class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb; // Rigidbody component for physics interactions
    [SerializeField] protected float damage = 10f; // Initial direction of the projectile
    [SerializeField] protected float speed = 10f; // Speed of the projectile
    //[SerializeField] protected float lifetime = 5f; // Lifetime of the projectile in seconds
    [SerializeField] protected AudioClip hitSound; // Sound to play when the projectile hits something
    [SerializeField] protected AudioSource audioSource; // Audio source to play the sound
    [SerializeField] protected LayerMask EnemyLayer; // Layer mask to identify enemy layers

    protected GameObject owner;
    protected bool ownedByPlayer = false;

    private void Start()
    {
        transform.parent = null; // Ensure the projectile is not a child of any other object
    }


    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the specified direction
        /*if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }*/
        gameObject.transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        /*// Destroy the projectile after its lifetime has elapsed
        if (Time.time - creationTime > lifetime)
        {
            Destroy(gameObject);
        }*/

    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Projectile") //if projectile collides with another projectile, ignore
            return;

        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            if(collision.gameObject.tag == "Enemy" && ownedByPlayer == false)
            {
                Debug.Log("enemy projectile hit another enemy: " + collision.gameObject.name);
            }
            else
            {
                // If the projectile hits an object with a HealthSystem, apply damage
                healthSystem.TakeDamage(damage); // Example damage value
                Debug.Log("Projectile hit: " + collision.gameObject.name + " with damage: " + damage);
            }
                
        }

        // Play the hit sound if it is set
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            
        }

        PostCollision(collision); // Call the PostCollision method for additional behavior

    }

    virtual protected void PostCollision(Collision2D collision)
    {
        // Destroy the projectile on collision
        Destroy(gameObject);
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    public void SetOwnedByPlayer(bool ownedByPlayer)
    {
        this.ownedByPlayer = ownedByPlayer;
    }

    protected bool CheckiftargetIsEnemy(Collision2D collision)
    {
        return (collision.gameObject.layer == 6 && ownedByPlayer == false);
    }

}

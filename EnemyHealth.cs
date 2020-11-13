using UnityEngine;



public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;            // The amount of health the enemy starts the game with.
    // The current health the enemy has.
    public float currentHealth;
    private string playerName;
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.


    void Awake()
    {
        // Setting up the references.
        capsuleCollider = GetComponent<CapsuleCollider>();
        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    public void takeDamage(float amount)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;
        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;


        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    public void Death()
    {
        // The enemy is dead.
        isDead = true;

        //Just sayitng its dead
        Debug.Log("Its dead");

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;
        // Find and disable the Nav Mesh Agent.
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // After 1 second destory the enemy.
        Destroy(gameObject, 0f);

    }



}

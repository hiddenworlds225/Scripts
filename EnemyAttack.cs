using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public float attackDamage = 10;               // The amount of health taken away per attack.


    Animator animhurt;
    GameObject player;                          // Reference to the player GameObject.
    Transform enemypos;
    PlayerInfo playerInfo;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange = false;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        enemypos = GetComponent<Transform>();
        playerInfo = player.GetComponent<PlayerInfo>();
        animhurt = player.GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, enemypos.position);

        if (distance < 1)
        {
            Debug.Log("HE'S RIGHT THERE!!!!!");
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        


        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
            Debug.Log("in range");
        }


    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;
        Debug.Log("Am being called");
        // If the player has health to lose...
        if (playerInfo.health > 0)
        {
            // ... damage the player.
            playerInfo.Damaged(attackDamage);
          //  animhurt.g
        }
    }

}

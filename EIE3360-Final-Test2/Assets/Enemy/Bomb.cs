using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;     // The time in seconds between each attack.
    float attackDamage = 0.20f;               // The amount of health taken away per attack.

    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    //float timer;                                // Timer for counting up to the next attack.
    GameObject coal;
    private bool attacked = true;

	void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Fire");
        coal = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }

        // If the player has zero or less health...
        if (playerHealth.Health <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        // Reset the timer.
        //timer = 0f;

        // If the player has health to lose...
        if (playerHealth.Health > 0&&attacked)
        {
            attacked = false;
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);

            enemyHealth.Death();

            //// Find and disable the Nav Mesh Agent.
            //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            //// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            //GetComponent<Rigidbody>().isKinematic = true;

            //// Increase the score by the enemy's score value.
            ////ScoreManager.score += scoreValue;

            //// After 2 seconds destory the enemy.
            //Destroy(gameObject, 1f);
        }
    }
}

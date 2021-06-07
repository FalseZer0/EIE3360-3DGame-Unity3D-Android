using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Transform fire;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fire = GameObject.FindGameObjectWithTag("Fire").transform;
        playerHealth = fire.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void FixedUpdate ()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.Health > 0)
        {
            if (gameObject.tag == "Bear")
            {
                nav.SetDestination(player.position);
            }
            else if (gameObject.tag == "Bomb")
            {
                if (nav != null && nav.enabled)
                    nav.SetDestination(fire.position);
            }
        }
        else
        {
            nav.enabled = false;
        }
    }
}

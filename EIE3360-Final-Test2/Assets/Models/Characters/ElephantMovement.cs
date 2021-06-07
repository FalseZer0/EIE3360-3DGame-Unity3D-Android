using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ElephantMovement : MonoBehaviour
{
    Transform fire;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        fire = GameObject.FindGameObjectWithTag("Fire").transform;
        playerHealth = fire.gameObject.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void FixedUpdate()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.Health > 0)
        {
            if(nav!=null&&nav.enabled)
                nav.SetDestination(fire.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}

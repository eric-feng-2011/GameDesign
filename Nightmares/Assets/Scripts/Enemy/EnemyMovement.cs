using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	//This is the transform of the player and what the ZomBunny will be moving towards
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
		//Finds the location of the player object
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

	//Nav Mesh Agents don't keep in time with Physics. That is why we choose to use Update instead of FixedUpdate
    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
			//Sets the nav destination to the location of the player. Allows the ZomBunny to follow the player around
            nav.SetDestination (player.position);
        }
        else
        {
			//Because a nav-mesh would only make sense on an active enemy,
			//once the enemy has died, we will disable the NAV
            nav.enabled = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	//Amount of time in betweeen attacks
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;	//Player GameObject
    PlayerHealth playerHealth;  //Reference to a script on another GameObject
    EnemyHealth enemyHealth;	//Reference to the enemyHealth script on the same gameObject
    bool playerInRange;
    float timer;		//Keep everything in sync and make sure that the ZomBunny doesn't attack too fast or too slow


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player"); //Creates reference to player in scene
        playerHealth = player.GetComponent <PlayerHealth> ();	//Creates reference to PlayerHealth script
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

	/* Triggers are used in other to detect collisions and 'stuff' behind-the-scenes.
	This function basically makes it so that whenever some Collider enters the trigger, in this case
	determined by the if statement to be the player, the following code is executed. */
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

	/* This is just the inverse of the previous function. Notice the '-exit' and '-enter'
	differences. i.e. One is when something enters a trigger, another is when something exits a trigger. */
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
		//Checks how much time has passed in between attacks. Accumulates if no attack.
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
			//Once the animation trigger is triggered, the enemy executes the animation and stops moving.
			//This does not do anything to the player. That is in the PlayerHealth script.
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
			//Causes player to take damage. The rest of the update is then executed by the playerHealth script
            playerHealth.TakeDamage (attackDamage);
        }
    }
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
	//When the enemy dies, they will not suddenly disappear, but rather sink through the floor at speed sinkSpeed.
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10; //Score Value
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;  //Reference to the capsule collider around the enemy
    bool isDead;		//Boolean values in order to check if dead and sinking
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
		//The GetComponentInChildren is necessary because the ParticleSystem hitParticles is a child of the enemy
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
			/*Translate the transfrom just means to move it
			Previous we used movePosition to move an object, but b/c we don't have to worry about
			physics when sinking, we will just use the translate function instead */
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

	//This second function of the argument determines where in the cap-collider the enemy has been hit.
	//This is necessary for the hitParticles ParticleSystem animation to work correctly.
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
		//If already dead, we don't want to do anything and will just return out of the function
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
		//Changing the location of the transform of the particleSystem to that of the hitPoint.
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

		//Because triggers don't act as physical obstacles, this will allow the player to go through them on death
        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
		//Disable the NavMeshAgent. Makes it so that the dead enemy doesn't go around anymore.
		//Could potentially be good for efficiency? Don't need to keep track of this NAV anymore.
		//Note the difference between .enabled and .setActive - the former is for components while the latter is for whole GameObjects
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		/* When a collider in the scene moves, Unity will recalculate everything for the level.
		 * The isKinematic portion is important, because it allows Unity to ignore the specified
		RigidBody and collider in its calculations. */
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
		//Notice that because score is a static variable of ScoreManager, we can access and modify the variable using the class name
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f); //Destroys the GameObject after 2 seconds.
    }
}

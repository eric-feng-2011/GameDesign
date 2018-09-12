using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();			//Shooting out a ray to see what we hit
    RaycastHit shootHit;				//Return whatever it is that we hit
    int shootableMask;					//Just like how the camera raycast was only supposed to hit the floor, so should this only hit shootable objects
    ParticleSystem gunParticles;		//Everything below this are special effects. Gun particles on shoot
    LineRenderer gunLine;				//The gunline or 'bullets'
    AudioSource gunAudio;				//The gun sound
    Light gunLight;						//The gun light
    float effectsDisplayTime = 0.2f;	//How long the effects will be viewable before disappearing


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");	//Setting the shootable layer to be 'shootable'
        gunParticles = GetComponent<ParticleSystem> ();		//Creating references to other components of gunEnd
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {	
		//Very similar to the enemy attack. Checks the interval in between attacks except along with input from user
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

		//Checks if enought time has passed for the special effects to disappear/disable
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {	
		//Disables the following effects
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

		//Turns the gunParticles off and on again in order to reset loop
        gunParticles.Stop ();
        gunParticles.Play ();

		//Creating a line with two points. The first '0' point is just the 'position' of the barrel of the gun
		//Second point calculated later with physics and raycasting
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

		//Sets the ray to be the end of the gun barrel, and the direction to be in the positive Z-axis
		//Notice that generally, we just always have the forward direction of an object be the + z-axis
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

		/*Short summary of the following:
			1. Shoot a ray. If it hits something return that.
			2. Otherwise, just draw a really long line that has no real significance */

		//Basically: shooting out a ray in the direction of 'shootRay' for a length of range, 
		//getting information from 'shootHit'. Only objects on the shootableMask can be hit
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
			//Get back the enemyHealth script from the object that the ray hit: 
			//what is stored in the shoothit is what it collides with
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

			//Any object besides an enemy does not have an enemyhealth script so it would return null
            if(enemyHealth != null)
            {
				//Make the enemy take damage according to the enemyHealth script
				//The second argument is where the shot occured - the ray casted and the information returned includes that
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }

			//If this if statement executes, we know that we hit something and we set the second (1) point 
			//of the gunLine to be that location. 
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
			//If nothing is hit, then it has the second (1) point of the ray all the way in the distance.
			//The length of the ray is * range
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}

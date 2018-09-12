using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	//Take note that in order to use UI elements, we need to be 'using UnityEngine.UI'
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;			//The audio clip that will play on death will be different from the hurt audio
    public float flashSpeed = 5f;		//The speed at which the flash will pop up
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);		//Completely red


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;		//A reference to another script. When player is dead want to disable
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> (); //Creating a reference to the PlayerMovement script
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {	
			//Then the screen flashes the flashColour
            damageImage.color = flashColour;
        }
        else
        {
			//Then the screen slowly fades away the damage image. Notice that we used Lerp here as well for smoothness
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

	//Because this is a public method, other scripts can reference it
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

		//When the player dies, this causes the playerMovement script to become disabled
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}

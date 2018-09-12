using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	//References to the player health and how long to wait before restarting game
    public PlayerHealth playerHealth;
	public float restartDelay = 5f;

    Animator anim;
	float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

			//Checks if there has been enougth time for the game to restart and if so, restarts
			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay) {
				Application.LoadLevel ("Level 01");		
				//Another way to reference level could be Application.LoadLevel(Application.loadedLevel);
				//This basically means open up the current scene instead of the one that is referenced.
			}
        }
    }
}

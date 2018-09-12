using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	//We have all these variables public so that we can change them in the editor for different situations as opposed to having to
	//have our scripts reference them inside itself.
    public PlayerHealth playerHealth;
    public GameObject enemy;			//The enemy that we want to spawn
    public float spawnTime = 3f;		//The spawntime in between the enemies
    public Transform[] spawnPoints;		//Where we could spawn the enemies. The individual items are Transforms.


    void Start ()
    {
		//Continue calling Spawn function at the start of the game. 
		//The first spawnTime is the time to wait before calling. The second is the time to wait after calling before repeating.
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
		//If the player is dead, no more spawns
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

		//This finds a random index / spawnPoint from the list of possible spawnPoints array
		//Random.Range does the random portion
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		//Creates an 'enemy' object with the position and rotation of the transform selected using spawnPointIndex
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}

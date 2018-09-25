using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject flower;
    public float offset = 0.5f;

    PlayerMovement playerMovement;

    Vector3 playerStart;
    float playerSpeed;
    float timer = 0;

    AudioSource audioSource;
    bool musicStarted;

    ArrayList flowerTimes = new ArrayList(new float[]{0.35f, 1.0f, 1.25f, 1.7f, 2.15f, 2.6f, 3.05f, 4.05f, 4.5f,
                           5.95f, 6.4f, 6.85f, 7.3f, 7.75f, 8.2f, 8.65f, 10.1f, 11.55f,
                           12.2f, 12.45f, 12.9f, 13.35f, 13.8f, 14.25f, 15.25f, 15.7f,
        17.15f, 17.6f, 18.05f, 18.5f, 18.95f, 19.4f, 19.85f, 20.85f, 21.3f});

    private void Awake()
    {
        for (int i = 0; i < flowerTimes.Count; i++) {
            flowerTimes[i] = (float) flowerTimes[i] * 2;
        }
        playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.Log("Wrong Player GameObject Inserted");
            Application.Quit();
        }
        playerSpeed = playerMovement.speed;
        playerStart = player.GetComponent<Transform>().position;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= (offset * 2) && !musicStarted) {
            audioSource.Play();
            musicStarted = audioSource.isPlaying;
        }
        FlowerSpawn();
	}

    void FlowerSpawn() {
        if (flowerTimes.Count != 0)
        {
            float flowerSpawnTime = (float)flowerTimes[0];
            if (timer >= flowerSpawnTime + offset)
            {
                //Debug.Log(timer);
                flowerTimes.RemoveAt(0);
                Instantiate(flower, new Vector3(playerStart.x + playerSpeed * timer + 3, playerStart.y, playerStart.z), Quaternion.identity);
            }
        }
    }
}

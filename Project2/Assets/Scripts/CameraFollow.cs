using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    Vector3 offset;

    void Awake() {
        offset = gameObject.transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = player.transform.position + offset;
	}
}

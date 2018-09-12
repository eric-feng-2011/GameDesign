using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	Vector3 offset;

	void Start()
	{
		//This causes the offset to be the distance between the camera and the player.
		//We want the offset to remain constant
		offset = transform.position - target.position;
	}

	//This function will, every physics step, reorientate the camera to the player
	void FixedUpdate()
	{
		//The targetCamPosition 
		Vector3 targetCamPosition = target.position + offset;	

		/* Lerp moves smoothly between two vectors at the speed of the final input
		In this case, the speed is the smoothing, but b/c we don't want it every 1/50 of a sec (the rate
		the frame updates), we also multiply it by Time.deltaTime */
		transform.position = Vector3.Lerp(transform.position, targetCamPosition, smoothing * Time.deltaTime);
	}
}

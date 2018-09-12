using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floormask;

	float camRayLength = 100f;

	//This is very much like Start function, but gets called whether or not the script is enabled
	void Awake(){
		floormask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	//This is an update that gets called for every physics update
	//Normal update runs with rendering (whatever that means)
	void FixedUpdate(){
		//Axis Raw gives -1, 0, 1 as opposed to Normal Axis which gives a range from -1 to 1. 
		//The difference between speeding up and snapping to full speed.
		float h = Input.GetAxisRaw ("Horizontal"); 
		float v = Input.GetAxisRaw ("Vertical");

		//Applying the various movement functions to the player
		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		//Setting the values for the Vector in movement
		movement.Set(h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime; //DeltaTime is the time between each update call

		//Moves a player to an input position. In this case, it is relative to where he is already at.
		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning(){
		//This creates a ray from the screen to wherever the mouse is at
		Ray camray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//To get back information from the ray, need to have the following
		RaycastHit floorhit;

		//This is an if-statement to carry out some procedures.
		//Checks the camray, if it hits, the length to check it for, and that it only attempts to hit the floor
		if (Physics.Raycast (camray, out floorhit, camRayLength, floormask)) 
		{
			//If the floor was hit, creates a vector from the mouse position (where the ray hit) to the player
			Vector3 playerToMouse = floorhit.point - transform.position;
			playerToMouse.y = 0f;

			//Object to store rotation. LookRotation takes in the angle to the forward vector? We put it as the playerToMouse vector here.
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			//Moves player to rotate to the newRotation
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v){
		//Checks if player is walking or not
		bool walking = h != 0f || v != 0f;
		//Set the boolean variable IsWalking (that we defined) to walking)
		anim.SetBool ("IsWalking", walking);
	}
}

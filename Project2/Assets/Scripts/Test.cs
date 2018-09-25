using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
	}
}

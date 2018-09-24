using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    public float speed;
    public float jumpF;

    bool jump = false;
    bool canJump = true;

    Vector2 horizontalMove;
    Vector2 jumpForce;

    private void Start() {
        horizontalMove = transform.right * speed;
        jumpForce = transform.up * jumpF;

        gameObject.GetComponent<Rigidbody2D>().velocity = horizontalMove;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            if (jump) {
                canJump = false;
                // Do we want second jump to be the same height as the first one or the same force?
                // gameObject.GetComponent<Rigidbody2D>().AddForce(jumpForce);
                gameObject.GetComponent<Rigidbody2D>().velocity = jumpForce + horizontalMove; 
            }
            else {
                jump = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = jumpForce + horizontalMove;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && jump) {
            gameObject.GetComponent<Rigidbody2D>().velocity = -jumpForce + horizontalMove;
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Floor")) {
            Debug.Log("Jump Reset");
            jump = false;
            canJump = true;
        }
        if (collision.gameObject.CompareTag("Flower")) {
            gameEnd();
        }
    }

    private void gameEnd() {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}

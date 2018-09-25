using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    
    public float speed;
    public float jumpF;

    public GameObject gameOver;
    public GameObject winText;
    public AudioSource audioSource;

    bool jump = false;
    bool canJump = true;

    Vector2 horizontalMove;
    Vector2 jumpForce;

    Animator playerAnimator;

    private void Start() {
        horizontalMove = transform.right * speed;
        jumpForce = transform.up * jumpF;
        playerAnimator = gameObject.GetComponent<Animator>();

        gameObject.GetComponent<Rigidbody2D>().velocity = horizontalMove;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            if (jump) {
                canJump = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = jumpForce + horizontalMove; 
                playerAnimator.SetTrigger("Jump");
            }
            else {
                jump = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = jumpForce + horizontalMove;
                playerAnimator.SetTrigger("Jump");
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && jump) {
            gameObject.GetComponent<Rigidbody2D>().velocity = -jumpForce + horizontalMove;
            playerAnimator.SetBool("Duck", true);
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Floor")) {
            //Debug.Log("Jump Reset");
            playerAnimator.SetBool("Duck", false);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(horizontalMove.x, 0, 0);
            jump = false;
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal")) {
            winText.SetActive(true);
            StartCoroutine(gameEnd());
        }
        if (other.gameObject.CompareTag("Flower"))
        {
            gameOver.SetActive(true);
            StartCoroutine(gameEnd());
        }
    }

    IEnumerator gameEnd() {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        audioSource.Stop();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}

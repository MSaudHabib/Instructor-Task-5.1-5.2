using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerHorizontalSpeed;
    public float jumpSpeed;
    public float playerVerticalSpeed;
    private Rigidbody2D playerRb;
    public float horizontalInput;
    public float verticalInput;
    public KnightControl playerControl;
    bool onGroundCheck = true;
    bool isClimbing;
    bool isLadder;
    public Collider2D floorCollider1;
    public Collider2D floorCollider2;
    public Collider2D floorCollider3;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput > 0)
        {
            transform.eulerAngles = Vector3.zero;
            playerControl.walking();
        }
        else if(horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            playerControl.walking();
        }
        else if(onGroundCheck)
        {
            playerControl.idle();
        }

        if (Input.GetButtonDown("Jump"))
        {
            onGroundCheck = false;
            playerControl.jump();
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
        }

        if(playerRb.velocity.y <= 0)
        {
            onGroundCheck = true;
        }

        if(isLadder && Mathf.Abs(verticalInput) > 0)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector2(horizontalInput * playerHorizontalSpeed, playerRb.velocity.y);

        if (isClimbing)
        {
            playerRb.gravityScale = 0;
            playerRb.velocity = new Vector2(playerRb.velocity.x, verticalInput * playerVerticalSpeed);
        }
        else
        {
            playerRb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder 1"))
        {
            isLadder = true;
            floorCollider1.enabled = false;
        }
        else if(collision.CompareTag("Ladder 2"))
        {
            isLadder = true;
            floorCollider1.enabled = false;
        }
        else if (collision.CompareTag("Ladder 3"))
        {
            isLadder = true;
            floorCollider1.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder 1"))
        {
            isLadder = false;
            isClimbing = false;
            floorCollider1.enabled = true;
        }
        else if (collision.CompareTag("Ladder 2"))
        {
            isLadder = false;
            isClimbing = false;
            floorCollider2.enabled = true;
        }
        else if (collision.CompareTag("Ladder 3"))
        {
            isLadder = false;
            isClimbing = false;
            floorCollider3.enabled = true;
        }
    }
}

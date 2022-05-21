using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce; // how high we can jump in the air

    private Rigidbody2D rb; // rigidbody

    public bool isJumping; // to know if the player is jumping or not
    public bool isRunning; // to know if the player is running/sprinting
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // makes sure we link our rigidbody to rb
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // to allow the player to move
        Jump(); // to all ow the player to jump
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            moveSpeed += 4; // add some speed
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            moveSpeed -= 4; // subtract some speed
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // moves us right
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // move us left
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false) // we hit spcae and we aren't jumping
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false; // for now when touching anything we're not jumping
    }
}

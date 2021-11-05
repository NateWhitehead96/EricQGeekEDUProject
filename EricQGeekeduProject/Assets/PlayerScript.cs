using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int moveSpeed = 5; // this will be how fast we move
    public Rigidbody rb; // this is a reference to our rigidbody
    public int jumpForce = 50; // this is the jump force
    public bool isJumping; // a true or false if we're jumping
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() // our movement inputs
    {
        if (Input.GetKey("w")) // moving forward
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s")) // moving backward
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a")) // moving left
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey("d")) // moving right
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown("space") && isJumping == false) // jumping
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 0, 1, 0
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }

}

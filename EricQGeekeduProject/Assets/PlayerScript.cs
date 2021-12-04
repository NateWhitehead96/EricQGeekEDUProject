using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int moveSpeed = 5; // this will be how fast we move
    public Rigidbody rb; // this is a reference to our rigidbody
    public int jumpForce = 50; // this is the jump force
    public bool isJumping; // a true or false if we're jumping

    public float horizontalSpeed; // how fast we rotate left and right
    public float verticalSpeed; // how fast we rotate up and down

    public float xRotation; // holding our current x rotation
    public float yRotation; // holding our current y rotation

    public Vector3 MoveDirection; // new forward position

    // Bullet stuff
    public GameObject Bullet; // a prefab of our bullet
    public float BulletForce; // how fast the bullet will travel
    public Transform ShootingPosition; // where the bullet comes from
    public ParticleSystem GunBlast; // reference to the particle system
    // Ammo counting
    public float MaxAmmo;
    public float CurrentAmmo;

    private int Health = 3;

    public Animator animator; // our animator reference

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // freezes our mouse position to the center of the screen
        rb = GetComponent<Rigidbody>();
        GunBlast.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerRotate();
        Shoot();
        Reload();

        // when we want to reset our rotation
        if (Input.GetKeyDown("z"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    void Move() // our movement inputs
    {
        //if (Input.GetKey("w")) // moving forward
        //{
        //    //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Time.deltaTime);
        //    transform.Translate(transform.forward * (moveSpeed * Time.deltaTime));
        //}
        //if (Input.GetKey("s")) // moving backward
        //{
        //    //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed * Time.deltaTime);
        //    transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey("a")) // moving left
        //{
        //    //transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        //    transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey("d")) // moving right
        //{
        //    //transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        //    transform.Translate(transform.right * moveSpeed * Time.deltaTime);
        //}

        float horizontal = Input.GetAxisRaw("Horizontal"); // store our horizontal input
        float vertical = Input.GetAxisRaw("Vertical"); // store our vertical input
        MoveDirection = (transform.forward * vertical) + (transform.right * horizontal); // apply those to our move direction
        Vector3 force = MoveDirection * (moveSpeed * Time.deltaTime); // create a force using move speed and our new direction
        transform.position += force; // apply all of that to our position
        

        if (Input.GetKeyDown("space") && isJumping == false) // jumping
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 0, 1, 0
            isJumping = true;
        }

        if(force.x > 0 || force.y > 0 || force.z > 0) // when we're moving set walking to true
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
    }

    void PlayerRotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X"); // holds our x mouse position
        yRotation = mouseX * horizontalSpeed * Time.deltaTime; // our new y rotation
        float mouseY = Input.GetAxisRaw("Mouse Y"); // holds our new y mouse position
        xRotation = mouseY * verticalSpeed * Time.deltaTime; // our new x rotation

        Vector3 PlayerRotation = transform.rotation.eulerAngles; // making a local variable to hold our current rotation
        PlayerRotation.y += yRotation; // setting our y rotation
        PlayerRotation.x -= xRotation; // setting our x rotation
        transform.rotation = Quaternion.Euler(PlayerRotation); // apply the rotation to our character
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && CurrentAmmo > 0) // when we left click
        {
            GameObject newBullet = Instantiate(Bullet, ShootingPosition.position, transform.rotation); // make a new bullet at the bullet shoot position with players rotation
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce); // give force to bullet
            GunBlast.Play();
            animator.SetBool("isShooting", true);
            CurrentAmmo--;
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isShooting", false);
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown("r"))
        {
            //CurrentAmmo = MaxAmmo;
            StartCoroutine(ReloadAction());
        }
    }

    IEnumerator ReloadAction() // reloading and we wait some time while reloading
    {
        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(0.5f);
        CurrentAmmo = MaxAmmo;
        animator.SetBool("isReloading", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;

        if (collision.gameObject.CompareTag("Enemy")) // if the thing we're hitting is an enemy
        {
            Health--; // subtract 1 health
            if(Health <= 0)
            {
                print("I am dead");
            }
        }
    }

    public int GetMyHealth()
    {
        return Health;
    }

}

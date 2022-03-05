using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Gun
{
    BulletGun,
    LaserGun
}

public class PlayerScript : MonoBehaviour
{
    public Gun currentGun; // this knows what gun we are currently holding

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
    public bool reloading; // keeps track of our reloading

    // Ammo counting
    public float MaxAmmo;
    public float CurrentAmmo;

    public int Health = 3;

    public Animator animator; // our animator reference

    public GameObject PauseCanvas; // a reference to our pause canvas

    public AudioSource shootSound; // a reference to the sound we make when shooting

    public LaserBeam beam; // new projectile, the laserbeam
    // Start is called before the first frame update
    void Start()
    {
        PauseCanvas.SetActive(false); // make sure our pause canvas is inactive to start
        Cursor.lockState = CursorLockMode.Locked; // freezes our mouse position to the center of the screen
        rb = GetComponent<Rigidbody>();
        GunBlast.Stop();
        shootSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerRotate();
        Shoot();
        Reload();
        SwitchGun();

        // when we want to reset our rotation
        if (Input.GetKeyDown("z"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Health <= 0) // when we die go to game over
        {
            SceneManager.LoadScene("GameOver");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseCanvas.activeInHierarchy) // pause canvas is on the screen
            {
                Time.timeScale = 1;
                PauseCanvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;
                PauseCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None; // we're not going to lock our mouse to the middle of the screen
            }
        }

    }

    public void SetHealth(int damage) // using a setter for our private health variable
    {
        Health -= damage;
        print(Health);
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
        if (currentGun == Gun.BulletGun)
        {
            //left mouse button clicked   ammo is more than 0 we arent reloading    pause canvas is not showing
            if (Input.GetMouseButtonDown(0) && CurrentAmmo > 0 && reloading == false && PauseCanvas.activeInHierarchy == false) // when we left click
            {
                GameObject newBullet = Instantiate(Bullet, ShootingPosition.position, transform.rotation); // make a new bullet at the bullet shoot position with players rotation
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce); // give force to bullet
                GunBlast.Play();
                shootSound.Play();
                animator.SetBool("isShooting", true);
                CurrentAmmo--;
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("isShooting", false);
            }
        }
        if(currentGun == Gun.LaserGun)
        {
            if (Input.GetMouseButton(0) && CurrentAmmo > 0 && reloading == false && PauseCanvas.activeInHierarchy == false)
            {
                beam.FireLaser();
                beam.beam.enabled = true; // show the laser
            }
            if (Input.GetMouseButtonUp(0))
            {
                beam.beam.enabled = false; // disable the laserbeam
            }
        }
    }

    void SwitchGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // when we press the 1 key equip the bullet gun
            currentGun = Gun.BulletGun;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // when we press the 2 key equip the laser gun
            currentGun = Gun.LaserGun;
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown("r") && CurrentAmmo < MaxAmmo) // if we press r and we have fired any bullets we can reload
        {
            //CurrentAmmo = MaxAmmo;
            StartCoroutine(ReloadAction());
        }
    }

    IEnumerator ReloadAction() // reloading and we wait some time while reloading
    {
        animator.SetBool("isReloading", true);
        reloading = true;
        yield return new WaitForSeconds(0.5f);
        CurrentAmmo = MaxAmmo;
        animator.SetBool("isReloading", false);
        reloading = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;

        
    }

    public int GetMyHealth()
    {
        return Health;
    }

}

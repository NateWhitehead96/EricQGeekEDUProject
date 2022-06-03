using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform player; // need access to the parent player

    public Vector2 mousePosition;
    // shooting
    public Transform shootPos; // shoot position
    public GameObject Bullet; // bullet
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // replace the rigidbody stuff, it was the thing not letting the gun move
        Vector2 lookDirection = mousePosition - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg; // final angle, so gun always points at mouse
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if(angle > 90 || angle < -90) // flip the player based on what side we're point at
        {
            player.localScale = new Vector3(-2f, 2f, 1);
            transform.localScale = new Vector3(-.2f, -.2f); // flip the gun when the player flips
        }
        else
        {
            player.localScale = new Vector3(2, 2, 1);
            transform.localScale = new Vector3(0.2f, .2f);
        }

        // Shoot controls
        if (Input.GetMouseButtonDown(0)) // when we left click
        {
            GameObject newBullet = Instantiate(Bullet, shootPos.position, shootPos.rotation); // spawn the bullet
            newBullet.GetComponent<Rigidbody2D>().AddForce(shootPos.right * 10, ForceMode2D.Impulse); // apply force to bullet
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossWorm : MonoBehaviour
{
    public Animator anim; // for the animations
    public Transform player; // players position

    public bool slam;
    public bool fire;

    public Vector3 rotationFix; // fix the rotation to look at player

    public Slider HealthBar;
    public GameObject BossCanvas; // so we can hide and show the boss health
    public float distance; // this will be the distance from the boss, to the player
    public int health;

    public GameObject Fireball; // the fire ball it'll fire
    public float timer; // help with giving the boss an order to slam or spit fire
    public Transform ShootPosition; // where the fireballz will come out

    public BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // link the naimator on the gameobject to the script
        box.enabled = false; // make sure the box doesnt hurt the player on start
        HealthBar.maxValue = health; // set the max hp
        BossCanvas.SetActive(false); // hide health until we want to show it
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = health;
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= 100)
        {
            BossCanvas.SetActive(true);
        }

        if(timer >= 5 && distance <= 50 && slam == false) // timer has run out and player is close, slam
        {
            StartCoroutine(BossSlam()); // slam attack
        }
        if(timer >= 5 && distance > 50 && fire == false)
        {
            StartCoroutine(BossFire()); // fireball attack
        }
        timer += Time.deltaTime;
        anim.SetBool("slamming", slam); // handle the slamming
        anim.SetBool("firing", fire); // handle the firing

        transform.LookAt(player.position + rotationFix); // have the worm look at player always
    }

    IEnumerator BossSlam()
    {
        slam = true;
        yield return new WaitForSeconds(2);
        box.enabled = true;
        yield return new WaitForSeconds(1);
        box.enabled = false;
        slam = false;
        timer = 0;
    }
    IEnumerator BossFire()
    {
        fire = true;
        GameObject newFireball = Instantiate(Fireball, ShootPosition.position, ShootPosition.rotation);
        newFireball.GetComponent<FireBall>().MoveLocation = player.position; // this makes fire move
        yield return new WaitForSeconds(3);
        fire = false;
        timer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    collision.gameObject.GetComponent<PlayerScript>().Health--;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().Health--;
        }
    }
}

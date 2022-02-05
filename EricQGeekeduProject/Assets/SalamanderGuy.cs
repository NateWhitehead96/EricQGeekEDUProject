using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SalamanderGuy : MonoBehaviour
{
    public Animator animator; // be our animation controller

    public bool walking;
    public bool attacking;

    public NavMeshAgent agent; // the AI stuff
    public Transform Player; // players position and that
    public AudioSource hurtSound;
    public int health; // for the health of the enemy

    public Vector3 offsetRotation;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerScript>().transform; // find the player 
        hurtSound = FindObjectOfType<SoundManager>().enemyHurtSound;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Player.position); // make it walk towards the player
        //transform.rotation = Quaternion.Euler(transform.rotation.x + offsetRotation.x, transform.rotation.y + offsetRotation.y, transform.rotation.z + offsetRotation.z);
        walking = true;
        animator.SetBool("isWalking", walking);
        animator.SetBool("isAttacking", attacking);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // is the thing we're colliding with is a bullet
        {
            health -= 1;
            hurtSound.Play();
            Destroy(collision.gameObject); // this will destory the bullet
            if (health <= 0) // if the enemies health is 0 or less than 0 destory it
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Player") && attacking == false) // when the enemy runs into the player and isnt attacking
        {
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        attacking = true;
        Player.gameObject.GetComponent<PlayerScript>().SetHealth(1);
        yield return new WaitForSeconds(1);
        attacking = false;
    }
}

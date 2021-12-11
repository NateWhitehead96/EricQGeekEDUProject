using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{

    public NavMeshAgent nav; // the AI used to move around the map
    public Transform PlayerPosition; // so the ai knows the players position

    public int Health; // how many hits the enemy can take

    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition = FindObjectOfType<PlayerScript>().transform; // this will ensure the ai always have the players transform
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(PlayerPosition.position);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // is the thing we're colliding with is a bullet
        {
            Health -= 1;
            Destroy(collision.gameObject); // this will destory the bullet
            if(Health <= 0) // if the enemies health is 0 or less than 0 destory it
            {
                Destroy(gameObject);
            }
        }
    }
}

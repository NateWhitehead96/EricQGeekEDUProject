using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEncounter : MonoBehaviour
{
    public GameObject keycard; // the thing the monster drops for progression
    public GameObject player; // to help with knowing when the monster sees player
    public int health;

    public Transform raypos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(raypos.position, Vector3.left, Mathf.Infinity); // shoot a laser to the left to search for player
        if (hit.collider.GetComponent<Player>()) // make sure hte laser hits player
        {
            player = hit.collider.gameObject; // assign player to the hit player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime); // make monster move
        }
        if(health <= 0) // monster runs out of health
        {
            Instantiate(keycard, transform.position, transform.rotation); // spawn the key
            Destroy(gameObject); // kill the monster
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--; // lower health on monster
            Destroy(collision.gameObject); // destroy bullet
        }
    }
}

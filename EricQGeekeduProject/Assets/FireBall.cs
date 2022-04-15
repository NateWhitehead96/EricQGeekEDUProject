using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector3 MoveLocation; // where the player is
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(MoveLocation * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, MoveLocation, moveSpeed * Time.deltaTime); // move fix
        if(transform.position == MoveLocation) // optional, but will destroy the fireball when it gets to its destination
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject); // once it touches anything destroy for now
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().Health--;
        }
    }
}

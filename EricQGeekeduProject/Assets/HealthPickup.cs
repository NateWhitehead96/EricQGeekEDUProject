using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>())
        {
            collision.gameObject.GetComponent<PlayerScript>().Health++; // give player health
            if(collision.gameObject.GetComponent<PlayerScript>().Health >= 3) // if player is at max health
            {
                collision.gameObject.GetComponent<PlayerScript>().Health = 3; // set player health to max health
            }
            Destroy(gameObject); // destroy the pickup
        }
    }
}

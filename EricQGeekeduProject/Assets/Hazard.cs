using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float MaxDistance;
    public float MinDistance;
    public int Direction; // either 1 -1
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z >= MaxDistance)
        {
            Direction = -1;
        }
        if(transform.position.z <= MinDistance)
        {
            Direction = 1;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Direction * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // for now just kill 1 shot the enemy
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().SetHealth(1); // now player takes 1 damage from the hazard
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Direction = 0;
        }
    }
}

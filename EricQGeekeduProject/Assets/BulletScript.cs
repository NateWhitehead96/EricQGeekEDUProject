using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float deathTimer; // when this hits 10 seconds destroy bullet

    private void Update()
    {
        deathTimer += Time.deltaTime;
        if(deathTimer >= 10)
        {
            Destroy(gameObject); // we have this just to make sure the bullets will die eventually
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) // if we're colliding with things at are not the player ! = not
        {
            Destroy(gameObject);
        }
    }
}

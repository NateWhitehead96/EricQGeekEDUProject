using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) // if we're colliding with things at are not the player ! = not
        {
            Destroy(gameObject);
        }
    }
}

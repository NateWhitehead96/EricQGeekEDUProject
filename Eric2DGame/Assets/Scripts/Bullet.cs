using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float timer; // count down to kill bullet
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    // if the player ever shoots the bullet into nothingness then it'll delete eventually
        if(timer >= 5)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // if the bullet hits anything, destroy it
    }
}

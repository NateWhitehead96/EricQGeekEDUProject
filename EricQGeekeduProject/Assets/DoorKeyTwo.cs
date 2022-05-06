using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyTwo : DoorKey // inheritance. we're using the doorkey script as a parent class so we get all of its variables
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KeyPad"))
        {
            doorToOpen.isOpened = true;
        }
    }
}

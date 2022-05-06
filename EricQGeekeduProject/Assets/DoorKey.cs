using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public DoorOpen doorToOpen; // reference to the door we want to open

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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            doorToOpen.isOpened = true; // open the door
        }
    }
}

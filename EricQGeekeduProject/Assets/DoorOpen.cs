using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool isOpened; // to know if the door is open or not
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpened == true)
        {
            transform.Translate(Vector3.down * 2 * Time.deltaTime); // this will move the door down
        }
    }
}

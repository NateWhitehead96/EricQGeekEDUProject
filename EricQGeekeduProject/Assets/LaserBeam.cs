using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform shootPosition; // barrel of the gun
    public LineRenderer beam; // the actual laser beam
    public Vector3 endPoint; // how far the laser beam can travel
    public Vector3 mousePosition; // where the cursor is

    // Start is called before the first frame update
    void Start()
    {
        beam = GetComponent<LineRenderer>();
        beam.startWidth = 0.5f;
        beam.endWidth = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireLaser()
    {
        beam.SetPosition(0, shootPosition.position); // the start of the line
        mousePosition = Input.mousePosition; // set the mouse position
        mousePosition.z = 100f;
        endPoint = Camera.main.ScreenToWorldPoint(mousePosition); // this is the end position
        beam.SetPosition(1, endPoint); // set that position for the beam

        RaycastHit hit; // the raycast (invisible laserbeam) that will detects whatever the laser hits
        if(Physics.Raycast(shootPosition.position, endPoint, out hit, 100))
        {
            print(hit.collider.gameObject); // for now just print what we hit with the laser
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<SalamanderGuy>().health--; // lower it health by 1
                if(hit.collider.gameObject.GetComponent<SalamanderGuy>().health <= 0)
                {
                    Destroy(hit.collider.gameObject); // when the enemy has no more health destroy it
                }
            }
        }
    }
}

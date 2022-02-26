using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour
{
    public Transform startPoint;
    public LineRenderer line;
    private Vector3 endPoint;
    private Vector3 mousePos;

    bool firing;
    public int ammo;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            FireLaser();
        }
        else
        {
            line.enabled = false;
        }
    }

    public void FireLaser()
    {
        line.SetPosition(0, startPoint.position); // start pos, barrel of gun
        mousePos = Input.mousePosition;
        mousePos.z = 100f;
        endPoint = Camera.main.ScreenToWorldPoint(mousePos);
        //endPoint = line.GetPosition(0) + transform.forward * 9; // finding the end point length
        line.SetPosition(1, endPoint); // set the end length point

        RaycastHit hit;
        if(Physics.Raycast(startPoint.position, endPoint, out hit, 100))
        {
            //endPoint = hit.collider.gameObject.transform.position; // shorten up the laser to just hit the first target
            print(hit.collider.gameObject); // this is where we'd check if it's an enemy, and deal dmg
        }

        line.enabled = true;
        if(!firing)
            StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage()
    {
        firing = true;
        ammo--;
        // deal the damage here as well
        yield return new WaitForSeconds(0.2f);
        firing = false;
    }
}

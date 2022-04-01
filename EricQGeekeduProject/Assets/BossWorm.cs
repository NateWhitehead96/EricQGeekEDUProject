using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorm : MonoBehaviour
{
    public Animator anim; // for the animations
    public Transform player; // players position

    public bool slam;
    public bool fire;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // link the naimator on the gameobject to the script
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("slamming", slam); // handle the slamming
        anim.SetBool("firing", fire); // handle the firing

        transform.LookAt(player); // have the worm look at player always
    }
}

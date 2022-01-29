using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderGuy : MonoBehaviour
{
    public Animator animator; // be our animation controller

    public bool walking;
    public bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", walking);
        animator.SetBool("isAttacking", attacking);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 positionOne; // we have 2 positions to know our start position and end position 
    public Vector3 PositionTwo;

    public Vector3 moveDirection; // to know which position the platform is going
    public float distance; // how far it is to the target position

    public float moveSpeed; // how fast the platform moves
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = positionOne; // set our move direction to start
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDirection, moveSpeed * Time.deltaTime); // move to the target position
        distance = Vector3.Distance(transform.position, moveDirection); // how far platform is from target position
        if(distance <= 1) // close to target position
        {
            if(moveDirection == positionOne) // if we we're going to position one switch to go to pos 2
            {
                moveDirection = PositionTwo;
            }
            else if (moveDirection == PositionTwo) // if we're going to position 2 switch to go to pos 1
            {
                moveDirection = positionOne;
            }
        }
    }
}

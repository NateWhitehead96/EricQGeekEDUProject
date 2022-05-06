using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 positionOne;
    public Vector3 PositionTwo;

    public Vector3 moveDirection;
    public float distance;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = positionOne; // set our move direction to start
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDirection, moveSpeed * Time.deltaTime);
        distance = Vector3.Distance(transform.position, moveDirection);
        if(distance <= 1)
        {
            if(moveDirection == positionOne)
            {
                moveDirection = PositionTwo;
            }
            else if (moveDirection == PositionTwo)
            {
                moveDirection = positionOne;
            }
        }
    }
}

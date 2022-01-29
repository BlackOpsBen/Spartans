using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float maxMoveSpeed = 2.0f;
    [SerializeField] private float acceleration = 1.0f;

    private float moveDirection = 0.0f;

    private float speed = 0.0f;

    private void Update()
    {
        UpdateSpeed();
        PerformMove();
    }

    private void UpdateSpeed()
    {
        speed += moveDirection * acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, -maxMoveSpeed, maxMoveSpeed);

        if (moveDirection == 0.0f)
        {
            speed = Mathf.Lerp(speed, 0.0f, acceleration * Time.deltaTime);
        }
    }

    private void PerformMove()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }

    public void SetMoveDirection(float direction)
    {
        moveDirection = direction;
    }
}

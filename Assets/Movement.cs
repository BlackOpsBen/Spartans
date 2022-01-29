using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float maxMoveSpeed = 2.0f;
    [SerializeField] private float acceleration = 5.0f;

    private Stance stance;

    private float moveDirection = 0.0f;

    private float speed = 0.0f;

    private Animator animator;

    private void Start()
    {
        stance = GetComponent<Stance>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateSpeed();
        PerformMove();
        UpdateAnimation();
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

    private void UpdateAnimation()
    {
        animator.SetBool("isRunning", Mathf.Abs(speed) > 0.2f);
    }

    public void SetMoveDirection(float direction)
    {
        moveDirection = direction;

        if (stance.GetIsRaising())
        {
            UpdateGameObjectFacing();
        }
    }

    private void UpdateGameObjectFacing()
    {
        if (moveDirection == -1.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (moveDirection == 1.0f)
        {
            transform.localScale = Vector3.one;
        }
    }
}

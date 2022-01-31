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

    private bool isPlayer = false;

    private void Start()
    {
        if (GetComponent<PlayerInterface>())
        {
            stance = GetComponent<Stance>();
            isPlayer = true;
        }
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateSpeed();
        PerformMove();
        UpdateAnimation();

        if (isPlayer)
        {
            if (stance.GetIsRaising())
            {
                UpdateGameObjectFacing();
            }
        }
        else
        {
            UpdateGameObjectFacing();
        }
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
    }

    private void UpdateGameObjectFacing()
    {
        if (moveDirection == -1.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f) * 1.5f;
        }
        else if (moveDirection == 1.0f)
        {
            transform.localScale = Vector3.one * 1.5f;
        }
    }
}

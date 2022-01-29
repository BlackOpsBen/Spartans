using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterface : MonoBehaviour
{
    private Movement movement;

    [SerializeField] private PlayerInterface nearestSpartan;
    private float distToNearestSpartan = float.MaxValue;
    private float stoppingDist = 2.25f;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        FindNearestSpartan();
        MoveTowardNearestSpartan();
    }

    private void FindNearestSpartan()
    {
        distToNearestSpartan = float.MaxValue;
        for (int i = 0; i < PlayerController.Instance.GetSpartans().Count; i++)
        {
            float distance = Vector2.Distance(PlayerController.Instance.GetSpartans()[i].transform.position, transform.position);
            if (distance < distToNearestSpartan)
            {
                distToNearestSpartan = distance;
                nearestSpartan = PlayerController.Instance.GetSpartans()[i];
            }
        }
    }

    private void MoveTowardNearestSpartan()
    {
        if (distToNearestSpartan > stoppingDist)
        {
            float direction = 1.0f;

            if (nearestSpartan != null && transform.position.x > nearestSpartan.transform.position.x)
            {
                direction = -1.0f;
            }

            SetMoveInput(direction);
        }
        else
        {
            SetMoveInput(0.0f);
        }
    }

    public void SetMoveInput(float direction)
    {
        movement.SetMoveDirection(direction);
    }

    public float GetDistanceToNearestSpartan()
    {
        return distToNearestSpartan;
    }
}

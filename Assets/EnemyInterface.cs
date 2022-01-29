using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterface : MonoBehaviour
{
    private Movement movement;

    [SerializeField] private PlayerInterface nearestSpartan;
    private float distToNearestSpartan = float.MaxValue;
    private float stoppingDist = 5.0f;
    [SerializeField] private List<PlayerInterface> spartans = new List<PlayerInterface>();

    private void Start()
    {
        spartans.AddRange(FindObjectsOfType<PlayerInterface>());
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
        for (int i = 0; i < spartans.Count; i++)
        {
            float distance = Vector2.Distance(spartans[i].transform.position, transform.position);
            if (distance < distToNearestSpartan)
            {
                distToNearestSpartan = distance;
                nearestSpartan = spartans[i];
            }
        }
    }

    private void MoveTowardNearestSpartan()
    {
        if (distToNearestSpartan > float.Epsilon)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    private Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    public void SetMoveInput(float direction)
    {
        movement.SetMoveDirection(direction);
    }
}

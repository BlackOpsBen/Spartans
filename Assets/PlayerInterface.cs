using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    private Movement movement;
    private Stance stance;

    private void Start()
    {
        movement = GetComponent<Movement>();
        stance = GetComponent<Stance>();
    }

    public void SetMoveInput(float direction)
    {
        movement.SetMoveDirection(direction);
    }

    public void SetStanceInput(bool value)
    {
        stance.SetIsRaising(value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<PlayerInterface> spartans = new List<PlayerInterface>();

    private void Start()
    {
        spartans.AddRange(FindObjectsOfType<PlayerInterface>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float direction = context.ReadValue<float>();

            foreach (PlayerInterface spartan in spartans)
            {
                spartan.SetMoveInput(direction);
            }
        }

        if (context.canceled)
        {
            foreach (PlayerInterface spartan in spartans)
            {
                spartan.SetMoveInput(0.0f);
            }
        }
        
    }

    public void OnRaiseShield(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            foreach (PlayerInterface spartan in spartans)
            {
                spartan.SetStanceInput(true);
            }
        }

        if (context.canceled)
        {
            foreach (PlayerInterface spartan in spartans)
            {
                spartan.SetStanceInput(false);
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            foreach (PlayerInterface spartan in spartans)
            {
                spartan.StartAttackInput();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private List<PlayerInterface> spartans = new List<PlayerInterface>();

    private void Start()
    {
        spartans.AddRange(FindObjectsOfType<PlayerInterface>());
        LivesCounter.Instance.SetLives(spartans.Count);
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

                AudioManager.Instance.PlaySoundFromGroup(0, AudioManager.SFX_SHUFFLE, false);
            }
        }

        if (context.canceled)
        {
            foreach (PlayerInterface spartan in spartans)
            {
                spartan.SetStanceInput(false);

                AudioManager.Instance.PlaySoundFromGroup(0, AudioManager.SFX_SHUFFLE, false);

                AudioManager.Instance.PlaySoundFromGroup(1, AudioManager.DIALOG_HAOO, false);
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

    public Transform GetRandomSpartan()
    {
        int rand = UnityEngine.Random.Range(0, spartans.Count);

        return spartans[rand].transform;
    }

    public List<PlayerInterface> GetSpartans()
    {
        return spartans;
    }

    public void RemoveDeadSpartan(PlayerInterface deadSpartan)
    {
        spartans.Remove(deadSpartan);

        if (spartans.Count == 0)
        {
            GameManager.Instance.GameOver();
        }

        LivesCounter.Instance.SetLives(spartans.Count);
    }
}

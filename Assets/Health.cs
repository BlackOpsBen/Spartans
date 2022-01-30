using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject deathPFX;

    [SerializeField] private int startingHP = 1;

    private int currentHP;

    private void Start()
    {
        currentHP = startingHP;
    }

    public void Hit()
    {
        currentHP--;
        if (currentHP == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathPFX, transform.position, Quaternion.identity);

        PlayerInterface playerInterface = GetComponent<PlayerInterface>();

        if (playerInterface != null)
        {
            PlayerController.Instance.RemoveDeadSpartan(playerInterface);

            AudioManager.Instance.PlaySoundFromGroup(1, AudioManager.DIALOG_SPARTAN_DEATH, true);
        }
        else
        {
            KillCounter.Instance.IncreaseKills();

            AudioManager.Instance.PlaySoundFromGroup(1, AudioManager.DIALOG_ENEMY_DEATH, false);

            AudioManager.Instance.PlaySoundFromGroup(1, AudioManager.DIALOG_KILL, true);
        }

        Destroy(gameObject);
    }
}

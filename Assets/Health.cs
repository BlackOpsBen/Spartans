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
        }

        Destroy(gameObject);
    }
}

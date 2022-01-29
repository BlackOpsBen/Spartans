using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Vector2 attackPosition;
    [SerializeField] private float attackRotation = -5.0f;

    [SerializeField] private float attackSpeed = 10.0f;

    [SerializeField] private float resetSpeed = 5.0f;

    [SerializeField] private GameObject thrustObject;

    private bool isAttacking = false;

    private bool isReady = true;

    private float attackPercent = 0.0f;

    private Stance stance;

    private void Start()
    {
        stance = GetComponent<Stance>();
    }

    private void Update()
    {
        UpdateAttackPercent();
        UpdateGameObjectTransform();
    }

    private void UpdateAttackPercent()
    {
        if (isAttacking)
        {
            attackPercent += attackSpeed * Time.deltaTime;
        }
        else
        {
            attackPercent -= resetSpeed * Time.deltaTime;
        }

        attackPercent = Mathf.Clamp(attackPercent, 0.0f, 1.0f);

        if (attackPercent == 1.0f)
        {
            isAttacking = false;
        }
        else if (attackPercent == 0.0f)
        {
            isReady = true;
        }
    }

    private void UpdateGameObjectTransform()
    {
        thrustObject.transform.localPosition = Vector3.Lerp(Vector3.zero, attackPosition, attackPercent);
        thrustObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(0.0f, attackRotation, attackPercent));
    }

    public void PrimaryAttack()
    {
        if (!stance.GetIsRaising() && isReady)
        {
            isAttacking = true;
            isReady = false;
        }
    }
}

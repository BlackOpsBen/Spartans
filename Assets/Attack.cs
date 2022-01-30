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

    [SerializeField] private Transform thrustObject;
    [SerializeField] private Transform attackStartPoint;
    [SerializeField] private Transform attackEndPoint;

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
            CheckForHit();
            isAttacking = false;
        }
        else if (attackPercent == 0.0f)
        {
            isReady = true;
        }
    }

    private void UpdateGameObjectTransform()
    {
        thrustObject.localPosition = Vector3.Lerp(Vector3.zero, attackPosition, attackPercent);
        thrustObject.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(0.0f, attackRotation, attackPercent));
    }

    public void PrimaryAttack()
    {
        if (!stance.GetIsRaising() && isReady)
        {
            isAttacking = true;
            isReady = false;

            AudioManager.Instance.PlaySFX("SpearAttack");
        }
    }

    private void CheckForHit()
    {
        Vector2 direction = attackEndPoint.position - attackStartPoint.position;

        float distance = Vector2.Distance(attackEndPoint.position, attackStartPoint.position);

        RaycastHit2D hit = Physics2D.Raycast(attackStartPoint.position, direction, distance, LayerMask.GetMask("Enemy"));

        Debug.DrawLine(attackStartPoint.position, attackEndPoint.position, Color.red, 0.2f);

        if (hit.collider != null)
        {
            Health enemyHealth = hit.transform.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.Hit();

                AudioManager.Instance.PlaySFX("SpearHit");
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform sword;
    [SerializeField] private Vector2 anticipationPos;
    [SerializeField] private float anticipationRot;
    [SerializeField] private Vector2 attackPos;
    [SerializeField] private float attackRot;
    [SerializeField] private Vector2 readyPos;
    [SerializeField] private float readyRot;

    [SerializeField] private float anticipationTime = 1.0f;
    [SerializeField] private float attackTime = 0.1f;
    [SerializeField] private float resetTime = 0.5f;

    [SerializeField] private float attackDistance = 3.0f;

    [SerializeField] private Transform hitStart;
    [SerializeField] private Transform hitEnd;

    private float anticipationTimer = 0.0f;
    private float attackTimer = 0.0f;
    private float resetTimer = 0.0f;

    private bool isAnticipating = false;
    private bool isAttacking = false;
    private bool isResetting = false;
    private bool isReady = true;

    private EnemyInterface enemyInterface;

    [SerializeField] private ParticleSystem slashPFX;
    [SerializeField] private ParticleSystem hitPFX;

    private void Start()
    {
        enemyInterface = GetComponent<EnemyInterface>();
    }

    private void Update()
    {
        if (isReady && enemyInterface.GetDistanceToNearestSpartan() < attackDistance)
        {
            Swing();
        }

        UpdateAnticipationTimer();

        UpdateAttackTimer();

        UpdateResetTimer();

        UpdateSwordTransform();
    }

    private void UpdateAnticipationTimer()
    {
        if (isAnticipating)
        {
            anticipationTimer += Time.deltaTime;
            if (anticipationTimer > anticipationTime)
            {
                isAnticipating = false;
                isAttacking = true;
                anticipationTimer = 0.0f;

                AudioManager.Instance.PlaySFX("SwordAttack");

                slashPFX.Play();
            }
        }
    }

    private void UpdateAttackTimer()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackTime)
            {
                isAttacking = false;
                isResetting = true;
                attackTimer = 0.0f;
                CheckForHit();
            }
        }
    }

    private void UpdateResetTimer()
    {
        if (isResetting)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > resetTime)
            {
                isResetting = false;
                isReady = true;
                resetTimer = 0.0f;
            }
        }
    }

    private void UpdateSwordTransform()
    {
        Vector3 posA = readyPos;
        Vector3 posB = readyPos;
        float rotA = readyRot;
        float rotB = readyRot;
        float t = 0.0f;

        if (isAnticipating)
        {
            t = anticipationTimer / anticipationTime;
            posA = readyPos;
            posB = anticipationPos;
            rotA = readyRot;
            rotB = anticipationRot;
        }
        else if (isAttacking)
        {
            t = attackTimer / attackTime;
            posA = anticipationPos;
            posB = attackPos;
            rotA = anticipationRot;
            rotB = attackRot;
        }
        else if (isResetting)
        {
            t = resetTimer / resetTime;
            posA = attackPos;
            posB = readyPos;
            rotA = attackRot;
            rotB = readyRot;
        }

        sword.localPosition = Vector3.Lerp(posA, posB, t);
        sword.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(rotA, rotB, t));
    }

    private void Swing()
    {
        isAnticipating = true;
        isReady = false;
    }

    private void CheckForHit()
    {
        Vector2 direction = hitEnd.position - hitStart.position;

        float distance = Vector2.Distance(hitEnd.position, hitStart.position);

        RaycastHit2D hit = Physics2D.Raycast(hitStart.position, direction, distance, LayerMask.GetMask("Player"));

        Debug.DrawLine(hitStart.position, hitEnd.position, Color.red, 0.2f);

        if (hit.collider != null)
        {
            Health spartanHealth = hit.transform.GetComponent<Health>();

            if (spartanHealth != null)
            {
                spartanHealth.Hit();

                AudioManager.Instance.PlaySFX("SwordHit");
            }

            if (hit.transform.CompareTag("Shield"))
            {
                AudioManager.Instance.PlaySoundFromGroup(0, AudioManager.SFX_SHIELD_HIT, false);

                hitPFX.Play();

                CameraShake.Instance.AddTrauma(0.5f);
            }
        }
    }
}

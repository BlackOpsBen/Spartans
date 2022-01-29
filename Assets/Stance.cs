using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stance : MonoBehaviour
{
    [SerializeField] private float switchSpeed = 5.0f;
    [SerializeField] private GameObject shieldSprite;
    [SerializeField] private GameObject spearSprite;
    [SerializeField] private Vector2 shieldPosUp;
    [SerializeField] private Vector2 shieldPosDown;
    [SerializeField] private Vector2 spearPosUp;
    [SerializeField] private Vector2 spearPosDown;
    private float rotationsUp = 90.0f;

    private float raisedPercent = 0.0f;

    private bool isRaising = false;

    private void Update()
    {
        UpdateStancePercent();
        UpdateGameObjectTransforms();
    }

    private void UpdateStancePercent()
    {
        if (isRaising)
        {
            raisedPercent += switchSpeed * Time.deltaTime;
        }
        else
        {
            raisedPercent -= switchSpeed * Time.deltaTime;
        }
        raisedPercent = Mathf.Clamp(raisedPercent, 0.0f, 1.0f);

        Debug.Log("Stance percent = " + raisedPercent);
    }

    private void UpdateGameObjectTransforms()
    {
        shieldSprite.transform.localPosition = Vector3.Lerp(shieldPosDown, shieldPosUp, raisedPercent);
        shieldSprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(0.0f, rotationsUp, raisedPercent));
        spearSprite.transform.localPosition = Vector3.Lerp(spearPosDown, spearPosUp, raisedPercent);
        spearSprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(0.0f, rotationsUp, raisedPercent));
    }

    public void SetIsRaising(bool value)
    {
        isRaising = value;
        Debug.Log("isRaising = " + isRaising);
    }
}

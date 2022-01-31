using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

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

    private float trauma = 0.0f;

    [SerializeField] private float defaultAmount = 0.1f;
    [SerializeField] private float maxRoll = 5.0f;
    [SerializeField] private float maxOffset = 5.0f;
    [SerializeField] private float frequency = 5.0f;
    [SerializeField] private float decayTime = 0.5f;

    private float seed = 0.0f;

    private void Update()
    {
        DecreaseTrauma();
        ShakeCamera();
    }

    private void DecreaseTrauma()
    {
        trauma -= Time.deltaTime / decayTime;
        trauma = Mathf.Clamp(trauma, 0.0f, 1.0f);
    }

    private void ShakeCamera()
    {
        float shake = Mathf.Pow(trauma, 2.0f);
        float roll = maxRoll * shake * GetPerlinNoise(seed, Time.time * frequency);
        float offsetX = maxOffset * shake * GetPerlinNoise(seed + 1, Time.time * frequency);
        float offsetY = maxOffset * shake * GetPerlinNoise(seed + 2, Time.time * frequency);

        transform.localPosition = new Vector3(offsetX, offsetY);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, roll);
    }

    private float GetPerlinNoise(float seed, float time)
    {
        return Mathf.PerlinNoise(seed, time) - 0.5f;
    }

    public void AddTrauma(float amount)
    {
        trauma += amount;
    }

    public void AddTrauma()
    {
        trauma += defaultAmount;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance { get; private set; }

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

    [SerializeField] private TextMeshProUGUI killsLabel;

    private int kills = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateKillsLabel();
    }

    public void IncreaseKills()
    {
        kills++;
        UpdateKillsLabel();
    }

    private void UpdateKillsLabel()
    {
        killsLabel.text = kills.ToString();
    }
}

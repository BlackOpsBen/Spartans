using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesCounter : MonoBehaviour
{
    public static LivesCounter Instance { get; private set; }

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

    [SerializeField] private TextMeshProUGUI livesLabel;

    private int lives = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLivesLabel();
    }

    public void SetLives(int numLives)
    {
        lives = numLives;
        UpdateLivesLabel();
    }

    private void UpdateLivesLabel()
    {
        livesLabel.text = lives.ToString();
    }

    public int GetLives()
    {
        return lives;
    }
}

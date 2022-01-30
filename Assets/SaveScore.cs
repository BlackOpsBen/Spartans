using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private GameObject scoreboardUI;

    public void SubmitScore()
    {
        scoreboardUI.SetActive(true);

        int score = KillCounter.Instance.GetKills();

        if (HighScores.Instance == null)
        {
            Debug.LogWarning("Highscores instance missing!");
        }
        else
        {
            HighScores.Instance.UploadScore(nameInput.text, score);
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        submitButton.interactable = nameInput.text.Length >= 1;
    }
}

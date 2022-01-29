using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private GameObject scoreboardUI;

    public void SubmitScore()
    {
        int score = KillCounter.Instance.GetKills();

        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);

            HighScores.UploadScore(nameInput.text, score);
        }

        scoreboardUI.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        submitButton.interactable = nameInput.text.Length >= 1;
    }
}

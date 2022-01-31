using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsUI;
    [SerializeField] private GameObject highscoresUI;

    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnBack()
    {
        instructionsUI.SetActive(false);
        highscoresUI.SetActive(false);
    }

    public void OnInstructions()
    {
        instructionsUI.SetActive(true);
    }

    public void OnHighscores()
    {
        highscoresUI.SetActive(true);
    }
}

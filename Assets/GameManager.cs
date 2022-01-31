using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private float gameOverUIDuration = 2.0f;

    [SerializeField] private GameObject submitUI;

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

    public void GameOver()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        gameOverUI.SetActive(true);
        yield return new WaitForSeconds(gameOverUIDuration);
        gameOverUI.SetActive(false);
        submitUI.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

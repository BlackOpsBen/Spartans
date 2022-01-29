using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button submitButton;

    public void SubmitScore()
    {
        Debug.Log("Name: " + nameInput.text + " - Length = " + nameInput.text.Length);
    }

    private void Update()
    {
        submitButton.interactable = nameInput.text.Length >= 1;
    }
}

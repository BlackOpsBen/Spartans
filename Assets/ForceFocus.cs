using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceFocus : MonoBehaviour
{
    [SerializeField] private InputField inputField;

    private void OnEnable()
    {
        if (inputField != null)
        {
            StartCoroutine(Select());
        }
    }

    private IEnumerator Select()
    {
        yield return new WaitForEndOfFrame();
        inputField.ActivateInputField();
    }
}

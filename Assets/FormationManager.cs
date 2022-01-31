using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    [SerializeField] private FollowPlayer cam;

    [SerializeField] private float spacing = 1.0f;

    [SerializeField] private float tolorance = 5.0f;

    [SerializeField] private float regroupSpeed = 5.0f;

    /*private void LateUpdate()
    {
        List<PlayerInterface> spartans = PlayerController.Instance.GetSpartans();
        float halfCount = spartans.Count / 2;
        for (int i = 0; i < spartans.Count; i++)
        {
            float desiredXPos = ((i - halfCount) * spacing) + cam.GetMedian();

            if (spartans[i].transform.position.x > desiredXPos + tolorance)
            {
                spartans[i].SetMoveInput(-0.5f);
            }
            else if (spartans[i].transform.position.x < desiredXPos - tolorance)
            {
                spartans[i].SetMoveInput(0.5f);
            }
        }
    }*/

    private void Update()
    {
        List<PlayerInterface> spartans = PlayerController.Instance.GetSpartans();

        float halfCount = spartans.Count / 2;

        for (int i = 0; i < spartans.Count; i++)
        {
            float desiredXPos = ((i - halfCount) * spacing) + cam.GetMedian();

            if (spartans[i].transform.position.x > desiredXPos + tolorance || spartans[i].transform.position.x < desiredXPos - tolorance)
            {
                spartans[i].transform.position = Vector3.Lerp(spartans[i].transform.position, new Vector3(desiredXPos, spartans[i].transform.position.y, spartans[i].transform.position.z), Time.deltaTime * regroupSpeed);
            }
        }
    }
}

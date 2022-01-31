using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    public static FormationManager Instance { get; private set; }

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

    [SerializeField] private FollowPlayer cam;

    [SerializeField] private float spacing = 1.0f;

    [SerializeField] private float tolorance = 5.0f;

    [SerializeField] private float regroupSpeed = 5.0f;

    [SerializeField] private float regroupDelay = 1.0f;
    [SerializeField] private float regroupTimeLimit = 2.0f;
    private float regroupTimer = float.MaxValue;

    private void Update()
    {
        regroupTimer += Time.deltaTime;

        if (regroupTimer > regroupDelay && regroupTimer < regroupTimeLimit)
        {
            AdjustPositions();
        }
    }

    private void AdjustPositions()
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

    public void Regroup()
    {
        regroupTimer = 0.0f;
    }
}

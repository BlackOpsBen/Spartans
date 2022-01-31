using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float lerpSpeed = 5.0f;

    private float xMedian;

    private void Update()
    {
        List<PlayerInterface> spartans = PlayerController.Instance.GetSpartans();

        if (spartans.Count > 0)
        {
            xMedian = 0.0f;

            List<float> xPoints = new List<float>();

            foreach (PlayerInterface spartan in spartans)
            {
                xPoints.Add(spartan.transform.position.x);
            }

            float xMax = Mathf.Max(xPoints.ToArray());
            float xMin = Mathf.Min(xPoints.ToArray());
            xMedian = xMax - ((xMax - xMin) / 2);
        }

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xMedian, Time.deltaTime * lerpSpeed), transform.position.y, transform.position.z);
    }

    public float GetMedian()
    {
        return xMedian;
    }
}

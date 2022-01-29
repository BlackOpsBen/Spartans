using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 target;
    public float lerpSpeed = 5.0f;

    private void Update()
    {
        Bounds bounds = new Bounds();
        foreach (PlayerInterface spartan in PlayerController.Instance.GetSpartans())
        {
            bounds.Encapsulate(spartan.transform.position);
        }

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, bounds.center.x, Time.deltaTime * lerpSpeed), transform.position.y, transform.position.z);
    }
}

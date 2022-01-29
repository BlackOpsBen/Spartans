using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;
    public float lerpSpeed = 5.0f;

    private void Update()
    {
        float lerpXPos = Mathf.Lerp(transform.position.x, target.transform.position.x, Time.deltaTime * lerpSpeed);
        transform.position = new Vector3(lerpXPos, transform.position.y, transform.position.z);
    }
}

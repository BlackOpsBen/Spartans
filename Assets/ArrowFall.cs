using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFall : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private bool isFalling = true;

    private void Update()
    {
        MoveArrow();
    }

    private void MoveArrow()
    {
        if (isFalling)
        {
            float newYPos = transform.position.y - speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
        }
    }
}

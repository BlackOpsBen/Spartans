using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRise : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float lifespan = 1.0f;
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifespan)
        {
            Destroy(gameObject);
        }

        float newYPos = transform.position.y + speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
    }
}

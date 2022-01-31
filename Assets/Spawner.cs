using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject prefab, float offset, Transform parent)
    {
        Vector3 position = new Vector3(transform.position.x + offset * Mathf.Sign(transform.localPosition.x), transform.position.y);

        Instantiate(prefab, position, Quaternion.identity, parent);
    }
}

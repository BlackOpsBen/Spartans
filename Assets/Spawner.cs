using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform characterParent;

    public void Spawn(GameObject prefab, float offset)
    {
        Vector3 position = new Vector3(transform.position.x + offset * Mathf.Sign(transform.position.x), transform.position.y);

        Instantiate(prefab, position, Quaternion.identity, characterParent);
    }
}

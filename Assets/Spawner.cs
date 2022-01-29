using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform characterParent;

    public void Spawn(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity, characterParent);
    }
}

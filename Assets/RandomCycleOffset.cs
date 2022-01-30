using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCycleOffset : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        if (animator = GetComponent<Animator>())
        {
            float rand = UnityEngine.Random.Range(0.0f, 1.0f);
            animator.SetFloat("cycleOffset", rand);
        }
    }
}

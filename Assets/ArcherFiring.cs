using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFiring : MonoBehaviour
{
    [SerializeField] private float firingInterval = 5.0f;

    private float timer = 0.0f;

    private Animator animator;

    private float maxOffset = 0.5f;

    private void Start()
    {
        float randOffset = UnityEngine.Random.Range(0.0f, maxOffset);

        timer -= randOffset;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > firingInterval)
        {
            timer = 0.0f;

            animator.SetTrigger("Fire");

            Debug.Log("Start fire...");
        }
    }

    // Called by Animation Event
    public void OnFire()
    {
        Debug.Log("Archer fired!");
    }
}

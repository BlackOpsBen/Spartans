using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFiring : MonoBehaviour
{
    [SerializeField] private float firingInterval = 5.0f;

    [SerializeField] private Transform arrowSmallSpawnPos;
    [SerializeField] private GameObject arrowSmallPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpawnYPos = 6.5f;

    [SerializeField] private float arrowFallDelayMin = 1.0f;
    [SerializeField] private float arrowFallDelayMax = 1.5f;

    private Transform target;
    private Vector3 targetLastPos;

    [SerializeField] private float maxShotDeviation = 5.0f;

    private float timer = 0.0f;

    private Animator animator;

    private float maxOffset = 0.5f;

    private PlayerController playerController;

    private void Start()
    {
        float randOffset = UnityEngine.Random.Range(0.0f, maxOffset);

        timer -= randOffset;

        animator = GetComponent<Animator>();

        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > firingInterval)
        {
            timer = 0.0f;

            animator.SetTrigger("Fire");
        }
    }

    // Called by Animation Event
    public void OnFire()
    {
        Instantiate(arrowSmallPrefab, arrowSmallSpawnPos.position, Quaternion.identity, arrowSmallSpawnPos);

        if (PlayerController.Instance.GetSpartans().Count < 1)
        {
            target = Camera.main.transform;
        }
        else
        {
            PickTarget();
        }

        StartCoroutine(SpawnArrow());
    }

    private void PickTarget()
    {
        target = playerController.GetRandomSpartan();
        targetLastPos = target.position;
    }

    private IEnumerator SpawnArrow()
    {
        float randDelay = UnityEngine.Random.Range(arrowFallDelayMin, arrowFallDelayMax);

        yield return new WaitForSeconds(randDelay);

        GameObject arrow = Instantiate(arrowPrefab);

        float randDeviation = UnityEngine.Random.Range(-maxShotDeviation, maxShotDeviation);

        if (target != null)
        {
            arrow.transform.position = new Vector3(target.position.x + randDeviation, arrowSpawnYPos, 0.0f);
        }
        else
        {
            arrow.transform.position = new Vector3(targetLastPos.x + randDeviation, arrowSpawnYPos, 0.0f);
        }
    }
}

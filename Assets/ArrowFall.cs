using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFall : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private bool isFalling = true;

    private float despawnTime = 5.0f;
    private float timer = 0.0f;

    [SerializeField] private ParticleSystem hitPFX;

    private void Update()
    {
        MoveArrow();
        UpdateDespawnTimer();
    }

    private void MoveArrow()
    {
        if (isFalling)
        {
            float newYPos = transform.position.y - speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
        }
    }

    private void UpdateDespawnTimer()
    {
        if (!isFalling)
        {
            timer += Time.deltaTime;
            if (timer > despawnTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isFalling = false;

        GetComponent<Collider2D>().enabled = false;

        transform.parent = collision.gameObject.transform;

        Stance spartanStance = collision.GetComponent<Stance>();

        if (spartanStance != null && !spartanStance.GetIsUp())
        {
            Health spartanHealth = collision.GetComponent<Health>();

            if (spartanHealth != null)
            {
                spartanHealth.Hit();

                //AudioManager.Instance.PlaySFX("FleshHit");
            }
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            AudioManager.Instance.PlaySoundFromGroup(0, AudioManager.SFX_SHIELD_HIT, false);
        }
        else
        {
            AudioManager.Instance.PlaySFX("ArrowHitGround");
        }

        hitPFX.Play();
    }
}

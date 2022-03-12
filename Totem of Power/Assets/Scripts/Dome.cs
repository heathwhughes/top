using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dome : MonoBehaviour
{
    [SerializeField] public float health = 100f;

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        // set isHit to false?
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDome(other.GetComponentInParent<Enemy>().damage);
        Destroy(other.GetComponentInParent<Enemy>().gameObject);
    }

    private void DamageDome(float damage)
    {
        GetComponent<Animator>().SetTrigger("hitTrigger");
        health -= damage;

        if (health <= 0)
        {
            DestroyDome();
        } 
    }

    private void DestroyDome()
    {
        Destroy(gameObject);
    }
}

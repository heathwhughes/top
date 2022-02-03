using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dome : MonoBehaviour
{
    [SerializeField] float health = 100f;

    private void OnTriggerEnter(Collider other)
    {
        DamageDome(other.GetComponentInParent<Enemy>().Damage);
        Destroy(other.GetComponentInParent<Enemy>().gameObject);
    }

    private void DamageDome(float damage)
    {
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

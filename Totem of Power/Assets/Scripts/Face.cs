using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    Dome dome;
    float healthbarValue = 100f;

    // bool triggeredLevelFinished = false;

    private void Start()
    {
        dome = FindObjectOfType<Dome>();
        healthbarValue = dome.health;
    }

    // Update is called once per frame
    void Update()
    {
        healthbarValue = dome.health;
        GetComponent<Slider>().value = healthbarValue;
    }
}

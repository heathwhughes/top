using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField] private float damageTimerSeconds = 1f;
    Face face;
    private Enemy[] enemiesOnFace;

    // Start is called before the first frame update
    
    IEnumerator Start()
    {
        face = GetComponent<Face>();
        Debug.Log(face.damage);

        while (face.active)
        {
            yield return new WaitForSeconds(damageTimerSeconds);
            //face.DealDamage();
            enemiesOnFace = face.GetComponentsInChildren<Enemy>();

            foreach (Enemy enemy in enemiesOnFace)
            {
                face.DealDamage(enemy);
            }
        }
        
    }

    
}

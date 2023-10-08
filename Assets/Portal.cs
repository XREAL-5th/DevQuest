using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && !GameManager.main.CheckEnemyLeft())
            GameManager.main.GameEnd();
    }
}

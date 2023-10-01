using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpControl : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private int healthPoint;


    [SerializeField]
    private float currentHp;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = healthPoint;
        hpBar.value = (float)currentHp/(float)healthPoint;
    }

    public void Damaged(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
            hpBar.value = 0;
            GameObject.Destroy(gameObject);
        }

        //hpBar.value = Mathf.Lerp(hpBar.value, (float)currentHp / (float)healthPoint, Time.deltaTime * 10);
        hpBar.value = (float)currentHp / (float)healthPoint;
    }
}

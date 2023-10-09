using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpControl : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private int healthPoint;
    [SerializeField]
    private TextMeshProUGUI hpText;


    [SerializeField]
    private float currentHp;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        currentHp = healthPoint;
        hpBar.value = (float)currentHp / (float)healthPoint;
        hpText.text = currentHp.ToString();
    }

    public void Damaged(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
            hpBar.value = 0;
            hpText.text = currentHp.ToString();
            GameManager.main.score++;
            gameObject.SetActive(false);
        }
        hpText.text = currentHp.ToString();
        //hpBar.value = Mathf.Lerp(hpBar.value, (float)currentHp / (float)healthPoint, Time.deltaTime * 10);
        hpBar.value = (float)currentHp / (float)healthPoint;
    }
}

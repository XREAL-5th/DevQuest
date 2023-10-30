using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static System.Net.Mime.MediaTypeNames;

public class HpControl : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private int healthPoint;
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private GameObject floatingText;
    [SerializeField] private Animator animator;



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
        animator.SetTrigger("stun");
        if (currentHp <= 0)
        {
            currentHp = 0;
            hpBar.value = 0;
            hpText.text = currentHp.ToString();
            GameManager.main.score++;
            gameObject.SetActive(false);
        }
        hpText.text = currentHp.ToString();
        //hpBar.value = Mathf.Lerp((float)currentHp+damage / (float)healthPoint, (float)currentHp / (float)healthPoint, Time.deltaTime * 10);
        hpBar.value = (float)currentHp / (float)healthPoint;
        if (currentHp > 0)
        {
            GameObject cloneText = Instantiate(floatingText);
            cloneText.transform.position = floatingText.transform.position;
            cloneText.GetComponent<TextMeshPro>().text = "-" + damage;
            StartCoroutine(FloatingText(cloneText));
        }
    }

    IEnumerator FloatingText(GameObject Text)
    {
        float time = 0f;
        float moveSpeed = 5.0f;
        Color alpha = Color.red;
        while (3f > time)
        {
            time += Time.deltaTime;
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * 2.0f); // 텍스트 알파값
            Text.GetComponent<TextMeshPro>().color = alpha;
            yield return new WaitForFixedUpdate();
        }
        Destroy(Text);
    }
}

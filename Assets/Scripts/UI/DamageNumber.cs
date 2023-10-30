using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public float disappearTime;
    public TextMeshProUGUI damageText;

    [SerializeField] private float disappearSpeed = 0.2f;
    private Color textColor;

    public void Setup(int damageAmount)
    {
        damageText.text = '-' + damageAmount.ToString();
        textColor = damageText.color;
        disappearSpeed = 1;
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
    }

    private void Update()
    {
        transform.position += new Vector3(0, disappearSpeed) * Time.deltaTime;
        disappearTime -= Time.deltaTime;

        if (disappearTime < 0)
        {
            // Start disappearing
            textColor.a -= disappearSpeed * Time.deltaTime;
            damageText.color = textColor;

            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
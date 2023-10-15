using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    private float range = 100f;
    public Camera aim;
    public GameObject attackEffect;
    public GameObject fireEffect;

    private RaycastHit hit;
    private GameObject enemy;
    private bool canFire = true;

    [Header("Gun Properties")]
    public GameObject bulletPrefab;

    [Header("Skill")]
    public GameObject hideImg;
    public GameObject text;
    public TextMeshProUGUI hideSkillTimeTexts;
    private bool isHideSkills = false;
    private float skillTimes = 10;
    private float getSkillTimes = 0;


    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        hideSkillTimeTexts = text.GetComponent<TextMeshProUGUI>();
        hideImg.SetActive(false);
    }
    public void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2") && canFire)
        {
            StartCoroutine(Bomb());
            StartCoroutine(SkillTimeChk(0));
        }
    }

    private void Shoot()
    {



        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, range))
        {
            var bullet = Instantiate(bulletPrefab, aim.transform.position, aim.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(aim.transform.forward* 50, ForceMode.Impulse);

            if (hit.transform.gameObject.layer == 7)
            {
                enemy = hit.transform.gameObject;
                enemy.GetComponent<Enemy>().TakeDamage(10);
            }

            GameObject impactObj = Instantiate(attackEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObj, 2f);
        }
    }

    IEnumerator Bomb()
    {
        canFire = false;
        ShootFireBall();
        yield return new WaitForSeconds(10);
        canFire = true;


    }

    private void ShootFireBall()
    {
        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, range))
        {
            var fireBall = Instantiate(fireEffect, aim.transform.position, aim.transform.rotation);
            fireBall.GetComponent<Rigidbody>().AddForce(aim.transform.forward * 50, ForceMode.Impulse);

            if (hit.transform.gameObject.layer == 7)
            {
                enemy = hit.transform.gameObject;
                enemy.GetComponent<Enemy>().TakeDamage(20);
            }

        }
    }

    public void HideSkillSetting(int skillNum)
    {
        hideImg.SetActive(true);
        getSkillTimes = skillTimes;
        isHideSkills = true;
    }
    private void HideSkillCheck()
    {
        
    }
    IEnumerator SkillTimeChk(int skillNum)
    {
        yield return null;
        if(getSkillTimes > 0)
        {
            getSkillTimes -= Time.deltaTime;
            if(getSkillTimes< 0)
            {
                getSkillTimes = 0;
                isHideSkills = false;
                hideImg.SetActive(false);
            }
            hideSkillTimeTexts.text = getSkillTimes.ToString("00");
            float time = getSkillTimes / skillTimes;
            hideImg.GetComponent<Image>().fillAmount = time;
            //hideImg.fill
        }
    }

}

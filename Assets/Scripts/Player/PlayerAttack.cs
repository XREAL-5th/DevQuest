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
    public bool canShoot = false;

    [Header("Gun Properties")]
    public GameObject bulletPrefab;

    [Header("Skill")]
    public GameObject hideBtn;
    public GameObject textPros;
    public TextMeshProUGUI hideSkillTimeTexts;
    public Image hideSkillImg;
    private bool isHideSkills = false;
    private float skillTimes = 10;
    private float getSkillTimes = 0;


    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        hideSkillTimeTexts = textPros.GetComponent<TextMeshProUGUI>();
        hideBtn.SetActive(false);
    }
    public void Update()
    {
        if(canShoot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
            if (Input.GetButtonDown("Fire2") && canFire)
            {
                HideSkillSetting();
                StartCoroutine(Bomb());

                //HideSkillCheck();
            }
            if (isHideSkills)
            {
                StartCoroutine(SkillTimeChk());
            }
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

    public void HideSkillSetting()
    {
        hideBtn.SetActive(true);
        getSkillTimes = skillTimes;
        isHideSkills = true;
    }
    private void HideSkillCheck()
    {
        if(isHideSkills)
        {
            StartCoroutine(SkillTimeChk());
        }
    }
    IEnumerator SkillTimeChk()
    {
        yield return null;
        if(getSkillTimes > 0)
        {
            getSkillTimes -= Time.deltaTime;
            if(getSkillTimes < 0)
            {
                

                getSkillTimes = 0;
                isHideSkills = false;
                hideBtn.SetActive(false);
            }
            hideSkillTimeTexts.text = getSkillTimes.ToString("00");
            float time = getSkillTimes / skillTimes;
            hideSkillImg.fillAmount = time;
            //hideImg.fill
        }
    }

}

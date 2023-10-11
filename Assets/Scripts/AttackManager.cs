using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    public ParticleSystem hiteffect;
    public Image img_Skill;
    public ParticleSystem skilleffect;
    
    void Start(){

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shot();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(CoolTime(5f));
            SkillQ();
        }
    }


    void Shot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(ray, out RaycastHit hitInfo);

        ParticleSystem newParticleSystem = Instantiate(hiteffect, hitInfo.point, Quaternion.identity);
        newParticleSystem.Play();
        
        if (hitInfo.collider.tag == "Enemy")
        {
            HPManager HPMGR = hitInfo.transform.gameObject.GetComponent<HPManager>();
            HPMGR.hp -= 1;
            HPMGR.hitpoint = hitInfo;
            HPMGR.hit = true;
            //Debug.Log(HPMGR.hp);
        }

        Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration);
        
    }

    IEnumerator CoolTime(float cool)
    {
        Debug.Log("쿨타임 시작");

       
        float elapsedTime = 0;

        while (elapsedTime < cool)
        {
            elapsedTime += Time.deltaTime;
            img_Skill.fillAmount = elapsedTime / cool;
            yield return null;
        }

        img_Skill.fillAmount = 1.0f; // 확실하게 1로 설정

        Debug.Log("쿨타임 종료");


    }

    void SkillQ()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(ray, out RaycastHit hitInfo);

        ParticleSystem newParticleSystem = Instantiate(skilleffect, hitInfo.point, Quaternion.identity);
        newParticleSystem.Play();

        Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration);

    }

}

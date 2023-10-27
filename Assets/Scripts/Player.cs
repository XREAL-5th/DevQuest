using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float attackPower = 10.0f; // 초기 공격력
    private Player player;
    public ItemSO itemData; // 아이템 정보를 저장한 Scriptable Object
    public BadItemSO baditemData; // 아이템 정보를 저장한 Scriptable Object
    public bool isGood;  


    public float skillCooldown = 10.0f;
    private bool isSkillReady = true;
    public TMP_Text cooldownText; // 스킬 쿨타임 UI Text 변수 추가



    private void Start()
    {
        SetAttackDamage(attackPower);
        cooldownText = GameObject.Find("CooldownText").GetComponent<TextMeshProUGUI>(); // Text? TextMeshProUGUI?
        cooldownText.text = "";
    }

    private void Update()
    {
        if (isSkillReady && Input.GetKeyDown(KeyCode.E))
        {
            // 스킬 발동
            isSkillReady = false; // 스킬 사용 중으로 변경
            StartCoroutine(ActivateSkill());
        }
    }

    private IEnumerator ActivateSkill()
    {
        // 스킬 발동
        // isSkillReady = false; // 스킬 사용 중으로 변경
        Debug.Log("이동기 스킬을 사용합니다.");

        // 스킬 로직 추가: 이동기
        // 움직임 구현
        transform.Translate(transform.forward * 5.0f);

        float timer = skillCooldown;
        while (timer > 0)
        {
            cooldownText.text = timer.ToString("F1"); // UI에 쿨타임 표시
            yield return new WaitForSeconds(1.0f);
            timer -= 1.0f;
        }

        cooldownText.text = ""; // 쿨타임 종료 시 UI에서 텍스트 숨김
        isSkillReady = true; // 스킬 재사용 가능으로 변경
        Debug.Log("스킬 재사용 가능");
    }


    public void SetAttackDamage(float Power)
    {
        attackPower = Power;
    }


    public float GetAttackPower()
    {
        return player.attackPower;
    }



    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 아이템인지 확인
        Debug.Log("====충돌(in player)");

        Item item = other.GetComponent<Item>();
        if (item != null && item.itemData != null)
        {
            Debug.Log("null 아님");
            if(other.CompareTag("item1"))
            {
                isGood = true;
            }
            else
            {
                isGood = false;
            }
            // 아이템을 먹음 (EatItem 함수 호출)
            EatItem(item); 
        }
        else
        {
            Debug.Log("null로 인식함");
        }
        
    }
    


    // 아이템을 먹을 때 호출되는 함수
    public void EatItem(Item item)
    {
        if(isGood == true)
        {
            Debug.Log("isGood Item");

            // 아이템의 효과를 적용
            attackPower *= item.itemData.attackPowerMultiplier;
            SetAttackDamage(attackPower);
            Debug.Log("공격력 배 :" + item.itemData.attackPowerMultiplier);
            Debug.Log("공격력 : " + attackPower);
            // Debug.Log("실제 공격력 : " + GetAttackPower());



            item.destroyItem();
            Debug.Log("아이템 삭제");

            if (item.itemData.vfxPrefab != null)
            {
                // 아이템 먹었을 때 생성되는 효과 재생
                Instantiate(itemData.vfxPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("vfxPrefab이 null입니다.");
            }
        }
        else
        {
            Debug.Log("is Not Good Item");
            Debug.Log("효과 적용 전 공격력 : " + attackPower);

            // 아이템의 효과를 적용
            attackPower = attackPower/2;
            SetAttackDamage(attackPower);
            Debug.Log("공격력 반 값");
            Debug.Log("공격력 : " + attackPower);


            item.destroyItem();
            Debug.Log("아이템 삭제");

            if (item.baditemData.vfxPrefab != null)
            {
                // 아이템 먹었을 때 생성되는 효과 재생
                Instantiate(baditemData.vfxPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("vfxPrefab이 null입니다.");
            }
        }
    }

}

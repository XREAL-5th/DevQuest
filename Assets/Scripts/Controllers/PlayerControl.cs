using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    int _mask = 1 << (int)Define.Layer.Enemy;
    bool _fired;
    GameObject _target;
    private int _maxKillCount = 6;
    private int _curKillCount = 0;

    PlayerStat _stat;

    public void UseStatItem(int hp, int maxHp, int attack, int defense)
    {
        _stat.Hp += hp;
        _stat.MaxHp += maxHp;
        _stat.Attack += attack;
        _stat.Defense += defense;
    }

    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _firePos;
    [SerializeField] GameObject _skill;
    [SerializeField] float[] cooldownMax;
    [SerializeField] float[] cooldown;

    public float GetCooldown(int idx)
    {
        return cooldown[idx];
    }

    public float GetCooldownMax(int idx)
    {
        if (idx >= 0 && idx < cooldownMax.Length)
            return cooldownMax[idx];
        Debug.Log($"Unavailable Skill idx: {idx}");
        return 0.0f;
    }

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        cooldown = new float[] { 0.0f };
        cooldownMax = new float[] { 5.0f };

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        Managers.Input.KeyAction -= OnKeyAction;
        Managers.Input.KeyAction += OnKeyAction;

    }

    private bool _activeSkill = false;
    private bool _canSkill = true;

    void BulletFire(Vector3 dir)
    {
        GameObject bullet = Instantiate(_bullet, _firePos.position, Quaternion.LookRotation(dir));
        if (_activeSkill && _canSkill)
        {
            _canSkill = false;
            _activeSkill = false;
            StartCoroutine(CoSkillCoolTime(bullet, 0));
        }
    }

    IEnumerator CoSkillCoolTime(GameObject bullet, int idx)
    {
        Instantiate(_skill, _firePos.position, _firePos.rotation, bullet.transform);
        cooldown[idx] = cooldownMax[idx];
        while (cooldown[idx] > 0.0f)
        {
            cooldown[idx] -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        _canSkill = true;
    }

    public bool clearedGame()
    {
        return _curKillCount >= _maxKillCount;
    }

    void OnHitEvent(Vector3 dir)
    {
        BulletFire(dir);
        if(_target != null)
        {
            Stat targetStat = _target.GetComponent<Stat>();
            int damage = Mathf.Max(0, _stat.Attack - targetStat.Defense);
            targetStat.OnAttacked(_stat);
        }
    }

    void OnKeyAction(KeyCode evt)
    {
        if (evt == KeyCode.F && _canSkill)
            _activeSkill = true;
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        
        if(raycastHit)
        { 
            switch(evt)
            {
                case Define.MouseEvent.PointerDown:
                    {
                        if(!_fired)
                        {
                            if (hit.collider.gameObject.layer == (int)Define.Layer.Enemy)
                            {
                                _target = hit.collider.gameObject;
                                OnHitEvent(ray.direction);
                                _fired = true;
                            }
                            else
                            {
                                BulletFire(ray.direction);
                            }
                        }
                        break;
                    }
                case Define.MouseEvent.PointerUp:
                    {
                        _target = null;
                        _fired = false;
                        break;
                    }
            }
        }

    }
}


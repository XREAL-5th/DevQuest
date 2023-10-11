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

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

    }

    void BulletFire()
    {
        Instantiate(_bullet, _firePos.position, _firePos.rotation);
    }

    public bool clearedGame()
    {
        return _curKillCount >= _maxKillCount;
    }

    void OnHitEvent()
    {
        BulletFire();
        if(_target != null)
        {
            Stat targetStat = _target.GetComponent<Stat>();
            int damage = Mathf.Max(0, _stat.Attack - targetStat.Defense);
            targetStat.Hp -= damage;

            Debug.Log(targetStat.Hp);
            if (targetStat.Hp <= 0)
            {
                _curKillCount++;
                Managers.Resource.Destroy(_target);
            }

        }
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
                            _firePos = hit.transform;
                            if (hit.collider.gameObject.layer == (int)Define.Layer.Enemy)
                            {
                                _target = hit.collider.gameObject;
                                OnHitEvent();
                                _fired = true;
                            }
                            else
                            {
                                BulletFire();
                            }
                        }
                        break;
                    }
                case Define.MouseEvent.PointerUp:
                    {
                        _target = null;
                        _fired = false;
                        _firePos = null;
                        break;
                    }
            }
        }

    }
}


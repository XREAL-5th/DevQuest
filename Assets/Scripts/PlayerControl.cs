using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    int _mask = 1 << (int)Define.Layer.Enemy;
    bool _fired;
    GameObject _target;

    PlayerStat _stat;

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

    void OnHitEvent()
    {
        BulletFire();
        if(_target != null)
        {
            Stat targetStat = _target.GetComponent<Stat>();
            Stat myStat = gameObject.GetComponent<PlayerStat>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            targetStat.Hp -= damage;

            Debug.Log(targetStat.Hp);
            if (targetStat.Hp <= 0)
            {
                Destroy(_target);
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
                        break;
                    }
            }
        }

    }
}


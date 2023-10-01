using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerStat _stat;

    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _firePos;

    public enum PlayerState
    {
        Attack,
    }

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

    int _mask = 1 << (int)Define.Layer.Enemy;
    GameObject _target;
    bool _fired;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        
        if (hit.collider.gameObject.layer == (int)Define.Layer.Enemy)
        {
            switch(evt)
            {
                case Define.MouseEvent.PointerDown:
                    {
                        if(!_fired)
                        {
                            _target = hit.collider.gameObject;
                            BulletFire();
                            _fired = true;
                        }
                        break;
                    }
                case Define.MouseEvent.PointerUp:
                    {
                        _fired = false;
                        break;
                    }
            }
        }

    }
}


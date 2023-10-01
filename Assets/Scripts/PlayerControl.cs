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
        Debug.Log("Bullet!");
        Instantiate(_bullet, _firePos.position, _firePos.rotation);
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Enemy);

    void OnMouseEvent(Define.MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Enemy && evt == Define.MouseEvent.PointerUp)
            {
                BulletFire();
            }
        }
    }
}

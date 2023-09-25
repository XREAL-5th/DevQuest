using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagicController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private GameObject hitEffect;
    public float damage = 1000f;

    private bool _cooldown = false;
    private Vector3 cameraDeltas = Vector3.zero;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (_cooldown)
        {
            var shake = Random.insideUnitSphere * 0.12f;
            playerCamera.transform.localPosition += shake;
            cameraDeltas += shake;
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Vector3 origin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        // Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(origin, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " + hit.collider.name);
                if (!_cooldown)
                {
                    StartCoroutine(ShotEffect());
                    StartExplosion(hit.point);
                    hit.collider.GetComponentInParent<Enemy>().InflictDamage(damage);
                }
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        // soundEffect.Play();

        _cooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        playerCamera.transform.localPosition -= cameraDeltas;
        cameraDeltas = Vector3.zero;
        _cooldown = false;
    }

    private void StartExplosion(Vector3 point)
    {
        var particleInstance = Instantiate(hitEffect, point, Quaternion.identity);
        ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.loop = false;
        Destroy(particleInstance, main.duration);
    }
}
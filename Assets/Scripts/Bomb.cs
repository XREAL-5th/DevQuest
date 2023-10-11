using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject bombPrefab;

    // ¿Ã∆Â∆Æ 
    public Transform bombImpact;
    [SerializeField]
    private ParticleSystem bombEffect;


    private float destroyTime = 3.0f;

    bool activateSkill = true;
    float reloadTime = 5.0f;

    Coroutine coroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        bombEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && activateSkill == true)
        {
            activateSkill = false;

            coroutine = StartCoroutine("ShootBomb", reloadTime);           
        }
    }

    private IEnumerator ShootBomb(float t)
    {
        yield return new WaitForSeconds(t);
        activateSkill = true;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}

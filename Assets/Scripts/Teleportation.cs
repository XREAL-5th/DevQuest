using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Button TeleportButton;
    private bool TeleportEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.T) && TeleportEnabled)
            StartCoroutine(SkillInput());   
    }

    IEnumerator SkillInput()
    {
        TeleportEnabled = false;
        gameObject.transform.position = gameObject.GetComponent<PlayerRay>().BulletClone.transform.position;
        TeleportButton.interactable = false;
        yield return new WaitForSeconds(10);

        TeleportButton.interactable = true;
        TeleportEnabled = true;
    }
}

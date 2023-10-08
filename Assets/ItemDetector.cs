using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ItemDetector : MonoBehaviour
{
    public FireMagicController firemagic;
    
    public void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        if (!obj.CompareTag("Item"))
        {
            return;
        }
        var type = obj.GetComponent<ItemComponent>().type;
        switch (type)
        {
            case ItemData.Type.Pill:
                firemagic.damage += 1000f;
                break;
            case ItemData.Type.Soju:
                firemagic.upgrade = true;
                break;
        }
        
        Destroy(obj);
    }
}

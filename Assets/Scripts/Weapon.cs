using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isFiring = false;
    public ParticleSystem muzzleFlash;

    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.Emit(1);
    }
    
    public void StopFiring()
    {
        isFiring = false;
    }
}


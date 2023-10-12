using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public int playermode;
    Item item;

    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Vector3 playerForwardDirection;

    private void Start()
    {
        playermode = 0;
    }

    private void Update()
    {
        playerPosition = transform.position;
        playerRotation = transform.rotation;
        playerForwardDirection = transform.forward;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Item(Clone)")
        {
            item = other.GetComponent<Item>();
            playermode = item.ItemData.Function;
            //Debug.Log(item.ItemData.Function);
        }
    }
}

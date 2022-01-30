using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorScript : MonoBehaviour
{
    public UnlockDoor_Key key;
    public bool readyToUnlock;

    private void Update()
    {
        if (key.picked)
        {
            readyToUnlock = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (readyToUnlock && collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor_Key : MonoBehaviour
{
    public GameObject doorToUnlock;

    public bool picked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            picked = true;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisterX : MonoBehaviour
{
    public GameObject eText;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eText.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    GameObject chest;
    // Start is called before the first frame update
    void Start()
    {
        chest = GameObject.FindGameObjectWithTag("Chest");
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
                print("yay");
                Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isFull;
    public bool isKey;

    GameObject[] actives;
    GameObject key; 
    // Start is called before the first frame update
    void Start()
    {
        isFull = false;
        actives = GameObject.FindGameObjectsWithTag("Active");
        key = GameObject.FindGameObjectWithTag("ChestKey");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Active"))
        {
            if (isFull == false)
            {
                isFull = true;
                var copy = Instantiate(gameObject, transform.position, transform.rotation);
                copy.SetActive(false);
                Destroy(collision.gameObject);
            }

            else if (isFull)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
        if (collision.gameObject.CompareTag("ChestKey"))
        {
            if (isFull == false)
            {
                isFull = true;
                isKey = true;
                var copy = Instantiate(gameObject, transform.position, transform.rotation);
                copy.SetActive(false);
                Destroy(collision.gameObject);
            }
        }

        
    }
}

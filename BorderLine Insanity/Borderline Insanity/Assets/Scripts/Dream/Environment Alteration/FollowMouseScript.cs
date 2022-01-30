using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseScript : MonoBehaviour
{
    public float moveSpeed = 1;
    bool followMouse;

    DreamManager dreamManager;

    // Use this for initialization
    void Start()
    {
        followMouse = true;
        dreamManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DreamManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followMouse)
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

            if (Input.GetMouseButtonDown(0))
            {
                followMouse = false;
            }
        }
        else
        {
            /*if (Input.GetMouseButton(0))
            {
                followMouse = true;
            }*/
        }
    }

    private void OnMouseDrag()
    {
        if (dreamManager.dreamEditMode == false)
        {
            followMouse = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }        
    }

    private void OnMouseUp()
    {
        followMouse = false;
    }
}

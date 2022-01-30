using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyMovmentScript : MonoBehaviour
{
    float originalY, originalRange;

    public float range; // You can change this in the Unity Editor to change the range of y positions that are possible.
    public float speed;
    public float horizontalSpeed;

    Vector3 newPos, startPos;

    public bool stopped, finishedOrder;

    public Transform target;

    float velo;
   
    public enum Side {Left, Right } public Side side;

    private void Start()
    {
        originalY = transform.position.y;
        originalRange = range;

        startPos = transform.position;

        finishedOrder = false;
        stopped = false;
    }

    void LateUpdate()
    {                   
        if (finishedOrder == false)
        {
            newPos = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, horizontalSpeed * Time.deltaTime), originalY + (Mathf.Sin(Time.time * speed) * range), transform.position.z);

            transform.position = newPos;

            switch (side)
            {
                case Side.Left:
                    if (transform.position.x <= target.position.x + .4f)
                    {
                        newPos = new Vector3(target.position.x, Mathf.Lerp(transform.position.y, target.position.y, speed));
                        //speed = Mathf.Lerp(speed, 0, horizontalSpeed * Time.deltaTime);
                        range = Mathf.SmoothDamp(range, 0, ref velo, horizontalSpeed);
                        stopped = true;
                    }
                    break;
                case Side.Right:
                    if (transform.position.x >= target.position.x - .4f)
                    {
                        newPos = new Vector3(target.position.x, Mathf.Lerp(transform.position.y, target.position.y, speed));
                        //speed = Mathf.Lerp(speed, 0, horizontalSpeed * Time.deltaTime);
                        range = Mathf.SmoothDamp(range, 0, ref velo, horizontalSpeed);
                        stopped = true;
                    }
                    break;
            }
        }
        else if (finishedOrder)
        {
            //range = Mathf.SmoothDamp(range, originalRange, ref velo, horizontalSpeed);
            range = originalRange;

            transform.position = newPos;

            switch (side)
            {
                case Side.Left:
                    newPos = new Vector3(Mathf.Lerp(transform.position.x, target.position.x - 15, horizontalSpeed * Time.deltaTime), originalY + (Mathf.Sin(Time.time * speed) * range), transform.position.z);
                    if (transform.position.x <= target.position.x -13)
                    {
                        NextCustomer();
                    }

                    break;
                case Side.Right:
                    newPos = new Vector3(Mathf.Lerp(transform.position.x, target.position.x + 15, horizontalSpeed * Time.deltaTime), originalY + (Mathf.Sin(Time.time * speed) * range), transform.position.z);
                    if (transform.position.x <= target.position.x + 17)
                    {
                        NextCustomer();
                    }
                    break;
            }
        }
    }

    void NextCustomer()
    {
        GameObject newCustomer = Instantiate(gameObject, startPos, Quaternion.identity);
        newCustomer.GetComponent<BouncyMovmentScript>().stopped = false;
        newCustomer.transform.SetParent(target);
        newCustomer.GetComponent<BouncyMovmentScript>().finishedOrder = false;
        Destroy(gameObject);
    }
}

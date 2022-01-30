using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject obj;

    public Vector3 offset;

    public bool smooth, deleteOnDestroy; // deleteOnDestroy : if the followed gameObject was destroyed then the following game object will be also destroyed

    public float smoothing;

    Vector3 velo;
    private void Update()
    {
        transform.position = obj.transform.position + offset;

        if (smooth)
            transform.position = Vector3.SmoothDamp(transform.position, obj.transform.position + offset, ref velo, smoothing);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropCreate : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SpawnObject()
    {
        GameObject a = Instantiate(prefab) as GameObject;
        a.AddComponent<FollowMouseScript>();

        a.transform.DetachChildren();
    }
}

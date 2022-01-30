using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCreatePanel : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey("q"))
        {
            panel.SetActive(true);
        }
        else if (Input.GetKeyUp("q"))
        {
            panel.SetActive(false);
        }
    }
}

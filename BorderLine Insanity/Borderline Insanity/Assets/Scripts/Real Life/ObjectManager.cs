using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum ObjectTYPE { DOOR , BED}

public class ObjectManager : MonoBehaviour
{
    public GameObject press_text, optionsWindow;

    public ObjectTYPE type;

    private void Start()
    {
        press_text.SetActive(false);
        optionsWindow.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            press_text.SetActive(true);
            if (Input.GetKey("e"))
            {
                press_text.GetComponent<TextMeshPro>().color = new Color(0, 0, 0, 0);
                optionsWindow.SetActive(true);

                if (type == ObjectTYPE.DOOR)
                {
                    OutDoorOptions();
                }
                else if (type == ObjectTYPE.BED)
                {
                    TranslateToDream();
                }
                press_text.SetActive(false);
            }
        }               
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            press_text.SetActive(false);
            press_text.GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 255);
            optionsWindow.SetActive(false);
        }           
    }

    void TranslateToDream()
    {

    }

    void OutDoorOptions()
    {
        // Put the map/city
        // Because it's a demo, you can only go out to work
        SceneManager.LoadScene(1);
    }
}

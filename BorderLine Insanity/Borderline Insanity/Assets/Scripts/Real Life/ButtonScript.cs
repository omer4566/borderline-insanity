using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public int howMuchTime;

    public void Work()
    {

    }

    private void Update()
    {
        switch (gameObject.name)
        {
            case "Time":

                GetComponent<TextMeshPro>().text = howMuchTime.ToString("00") + ":00";

                if (Input.GetKeyDown("return"))
                {
                    //PlayerPrefs.SetInt("timesWorked", howMuchTime);
                    PlayerPrefs.SetInt("takenTime", howMuchTime);
                    SceneManager.LoadScene(2);
                }

                if (Input.GetKeyDown("w"))
                {
                    howMuchTime += 1;
                    if (howMuchTime > 15)
                    {
                        howMuchTime = 15;
                    }
                }
                else if (Input.GetKeyDown("s"))
                {
                    howMuchTime -= 1;
                    if (howMuchTime <= 0)
                    {
                        howMuchTime = 1;
                    }
                }
                break;
                // Multiply by 3 to know how much minutes in real life the dream will last
        }
    }
}

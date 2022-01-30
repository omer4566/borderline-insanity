using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//public enum Character {CHAD }

public class CharacterSceneCaller : MonoBehaviour
{
    public GameObject phone;

    private int rank, pt;

    [Header("Regular Scene")]
    public Scene regular_scenes;

    [Header("Plot Scene")]
    public Scene[] plot_scene;

    private void Start()
    {

    }

    private void Update()
    {
        /*if (phone.activeSelf && Input.GetKeyDown("e"))
        {
            phone.SetActive(false);
        }
        else if (phone.activeSelf == false && Input.GetKeyDown("e"))
        {
            phone.SetActive(true);
        }*/
    }


    public void ChooseScene(Character ch)
    {
        switch (ch)
        {
            case Character.CHAD:
                Debug.Log("Loading chad");

                rank = PlayerPrefs.GetInt("Chad_rank");
                pt = PlayerPrefs.GetInt("Chad_pt");

                ScenePicker(ch);
                break;
        }

    }

    void ScenePicker(Character character)
    {
        switch (character)
        {
            case Character.CHAD:
                //Condition to play the next friendship level scene
                if (pt >= PlayerPrefs.GetInt("Chad_cap"))
                {
                    Debug.Log("Plot");

                    SceneManager.LoadScene(plot_scene[PlayerPrefs.GetInt("Chad_rank")].handle);
                }
                //Condition to play day to day/regular scene
                else
                {
                    Debug.Log("Hangout");
                    SceneManager.LoadScene(regular_scenes.handle);
                }
                break;
        }
    }
}

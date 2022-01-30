using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HangoutButton : MonoBehaviour
{
    public Character character;

    public CharacterSceneCaller sceneCaller;

    public TextMeshProUGUI textMesh;

    public float timeForNextText;
    private float current_timeForNextText;

    int replicasIndexer = 0;
    public string[] replicas;

    bool textStarted;

    [Header("Plot Scene")]
    public Scene[] plot_scenes;
    [Header("Regular Scene")]
    public Scene regular_scenes;

    private void Start()
    {
        current_timeForNextText = timeForNextText;
    }

    private void LateUpdate()
    {

        if (textStarted)
        {
            sceneCaller.phone.SetActive(true);
            TypeText();

        }

        else if (textStarted == false && GetComponent<Button>().enabled == false)
        {
            current_timeForNextText -= Time.deltaTime;
            if (current_timeForNextText <= 0)
            {
                replicasIndexer += 1;
                current_timeForNextText = timeForNextText;

                if (replicasIndexer < replicas.Length)
                {
                    textStarted = true;                   
                }
                else
                {
                    sceneCaller.plot_scene = plot_scenes;
                    sceneCaller.regular_scenes = regular_scenes;

                    sceneCaller.ChooseScene(character);
                    Debug.Log("Arriving to scene");
                }
            }

            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                current_timeForNextText = 0;
            }
        }
           
    }

    public void ButtonPressed()
    {
        switch (character)
        {
            case Character.CHAD:
                textStarted = true;
                break;
        }
        GetComponent<Button>().enabled = false;
    }

    void TypeText()
    {
        if (replicasIndexer > replicas.Length)
            textStarted = false;

        if (textMesh.text == "")
            textMesh.text = replicas[0];
        else
            textMesh.text = textMesh.text + " \n" + replicas[replicasIndexer];

        textStarted = false;
    }
}

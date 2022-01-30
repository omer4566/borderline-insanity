using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Important info")]
    public float textTime;

    public string charName;

    public Sprite charPortrait;

    public string currentLine;

    [Header("Json file")]
    public TextAsset dialogue; // The Json file

    private int current_answerAmount;

    [Header("Do not touch")]
    
    public AudioClip voice;
    private AudioSource audioSource; 
    [TextArea(1, 3)]
    
    public List<string> speech; // Character's speech
    
    public TextMeshProUGUI textMesh, nameMesh; // Text meshes

    public Image portrait;

    int index = 0;        
    
    private DialogueLines[] dialogueItems;
    private Answers[] answerLines;
    
    public GameObject answers_OBJ;

    private Vector2 good_pos, bad_pos, neut_pos; 
    public GameObject answerGOOD_button, answerBAD_button, answerNEUT_button;

    public bool answered;

    public int friendship_pt;

    // Execute at the start of the scene
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        good_pos = answerGOOD_button.transform.position;
        bad_pos = answerBAD_button.transform.position;
        neut_pos = answerNEUT_button.transform.position;

        portrait.sprite = charPortrait;

        textMesh.text = "";
        current_answerAmount = 1;

        nameMesh.text = charName;
    
        dialogueItems = JsonUtility.FromJson<Dialogue>(dialogue.text).lines; // Import the lines from the Json file
        answerLines = JsonUtility.FromJson<Dialogue>(dialogue.text).answers1; // Import the answers from the Json file

        foreach (DialogueLines dia in dialogueItems)
        {
            speech.AddRange(dia.line1);
        }

        answers_OBJ.SetActive(false);

        StartCoroutine(TypeText());
    }

    // Execute every frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            PlayerPrefs.DeleteAll();
        }

        currentLine = speech[index];

        if (index >= speech.ToArray().Length - 1) // Here it is time to answer
        {
            if (speech[index] == textMesh.text)
            SetAnswers();
        }
        else
        {
            if (Input.GetKeyDown("e") || Input.GetKey("z")) // Get the next line
            {
                NextSentence();
                MixPositions();
            }
        }
    }

    void SetAnswers()
    {
        switch (current_answerAmount)
        {
            #region First Answer
            case 1:
                answers_OBJ.SetActive(true);

                foreach (Answers ans in answerLines)
                {
                    answerGOOD_button.GetComponentInChildren<TextMeshProUGUI>().text = ans.option1;
                    answerBAD_button.GetComponentInChildren<TextMeshProUGUI>().text = ans.option2;
                    answerNEUT_button.GetComponentInChildren<TextMeshProUGUI>().text = ans.option3;
                }

                if (answered)
                {
                    #region Answer Response
                    foreach (Answers ans in answerLines)
                    {
                        // If the answer was positive
                        if (answerGOOD_button.GetComponent<AnswerButton>().chosen)
                        {
                            speech.Add(ans.response1); answerGOOD_button.GetComponent<AnswerButton>().chosen = false;
                        }
                        // If the answer was negative
                        if (answerBAD_button.GetComponent<AnswerButton>().chosen)
                        {
                            speech.Add(ans.response2); answerBAD_button.GetComponent<AnswerButton>().chosen = false;
                        }
                        // If the answer was neutral
                        if (answerNEUT_button.GetComponent<AnswerButton>().chosen)
                        {
                            speech.Add(ans.response3); answerNEUT_button.GetComponent<AnswerButton>().chosen = false;
                        }
                    }
                    #endregion

                    foreach (DialogueLines dia in dialogueItems)
                    {
                        speech.AddRange(dia.line2);
                        answers_OBJ.SetActive(false);
                        current_answerAmount = 2;
                        NextSentence();
                        answered = false;
                    }
                }
                break;
            #endregion
            #region Second Answer
            case 2:
                answers_OBJ.SetActive(true);

                answerLines = JsonUtility.FromJson<Dialogue>(dialogue.text).answers2;
                foreach (Answers ans in answerLines)
                {
                    answerGOOD_button.GetComponentInChildren<TextMeshProUGUI>().text = ans.option1;
                    answerBAD_button.GetComponentInChildren<TextMeshProUGUI>().text = ans.option2;
                    answerNEUT_button.GetComponentInChildren<TextMeshProUGUI>().text = ans.option3;
                }

                if (answered)
                {
                    #region Answer Response
                    foreach (Answers ans in answerLines)
                    {
                        // If the answer was positive
                        if (answerGOOD_button.GetComponent<AnswerButton>().chosen)
                        {
                            speech.Add(ans.response1); answerGOOD_button.GetComponent<AnswerButton>().chosen = false;
                        }
                        // If the answer was negative
                        if (answerBAD_button.GetComponent<AnswerButton>().chosen)
                        {
                            speech.Add(ans.response2); answerBAD_button.GetComponent<AnswerButton>().chosen = false;
                        }
                        // If the answer was neutral
                        if (answerNEUT_button.GetComponent<AnswerButton>().chosen)
                        {
                            speech.Add(ans.response3); answerNEUT_button.GetComponent<AnswerButton>().chosen = false;
                        }
                    }
                    #endregion
                    foreach (DialogueLines dia in dialogueItems)
                    {
                        speech.AddRange(dia.line3);                     
                    }
                    answers_OBJ.SetActive(false);
                    current_answerAmount = 3;
                    NextSentence();
                    answered = false;
                }
                break;
            #endregion
            #region Third Answer
            case 3:
                //Debug.Log(friendship_pt);
                var relation = transform.parent.GetComponent<RelationshipsManager>();
                relation.relation_pt = friendship_pt;
                relation.AddPoints();
                    break;
                #endregion
        }
    }

    IEnumerator TypeText()
    {
        foreach (char letter in speech[index].ToCharArray())
        {
            audioSource.PlayOneShot(voice);
            audioSource.pitch = Random.Range(0.7f,1.0f);
            audioSource.volume = Random.Range(0.8f, 1.0f);

            textMesh.text += letter;           
            yield return new WaitForSeconds(textTime);            
        }
    }

    void NextSentence()
    {
        if (textMesh.text == speech[index])
        {
            if (index < speech.ToArray().Length)
            {
                index++;
                textMesh.text = "";
                StartCoroutine(TypeText());
            }
            else
            {
                textMesh.text = "";
            }
        }
        else
        {
            StopAllCoroutines();
            textMesh.text = "";
            textMesh.text = speech[index];
        }
    }

    void MixPositions()
    {
        var rand = Random.Range(1, 4);
        switch(rand)
        {
            case 1:
                answerBAD_button.transform.position = good_pos;
                answerGOOD_button.transform.position = neut_pos;
                answerNEUT_button.transform.position = bad_pos;
                break;
            case 2:
                answerBAD_button.transform.position = neut_pos;
                answerGOOD_button.transform.position = good_pos;
                answerNEUT_button.transform.position = bad_pos;
                break;
            case 3:
                answerBAD_button.transform.position = bad_pos;
                answerGOOD_button.transform.position = neut_pos;
                answerNEUT_button.transform.position = good_pos;
                break;
            case 4:
                answerBAD_button.transform.position = bad_pos;
                answerGOOD_button.transform.position = good_pos;
                answerNEUT_button.transform.position = neut_pos;
                break;
        }
    }
}

#region Data classes
[System.Serializable]
public class Dialogue
{
    public DialogueLines[] lines;
    public Answers[] answers1, answers2;
}

[System.Serializable]
public class DialogueLines
{
    public string[] line1;
    public string[] line2;
    public string[] line3;
}

[System.Serializable]
public class Answers
{
    public string option1;
    public string option2;
    public string option3;

    public string response1;
    public string response2;
    public string response3;
}
#endregion
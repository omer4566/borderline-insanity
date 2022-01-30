using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnswerTYPE { GOOD, BAD, NEUT}

public class AnswerButton : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    public AnswerTYPE type;

    public int friendship_pt;

    public bool chosen = false;

    void Awake()
    {
        switch(type)
        {
            case AnswerTYPE.BAD:
                friendship_pt = 0;
                break;

            case AnswerTYPE.GOOD:
                friendship_pt = 3;
                break;

            case AnswerTYPE.NEUT:
                friendship_pt = 1;
                break;
        }
    }

    public void AClick()
    {
        dialogueSystem.answered = true;
        chosen = true;
        dialogueSystem.friendship_pt += friendship_pt;
    }
}

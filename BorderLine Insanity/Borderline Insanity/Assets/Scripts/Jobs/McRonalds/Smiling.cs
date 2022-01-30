using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smiling : MonoBehaviour
{
    public float maxAppearanceTime;
    public float maxReactionTime;

    private float appearanceTime;
    private float reactionTime;

    public float maxPressingTime;
    private float pressingTime;

    bool haveToSmile, smilingPressed;

    Vector3 startingPos;

    public Sprite happyBoss, normalBoss, angryBoss;
    public Image smileFill;

    public Transform goTo_transform;

    public Animator mcRonalds_Animator;

    private void Start()
    {
        appearanceTime = maxAppearanceTime;
        reactionTime= maxReactionTime;

        startingPos = transform.position;

        GetComponent<SpriteRenderer>().sprite = normalBoss;

        pressingTime = 0;
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, new Vector3(-5.82f, 2.21f), appearanceTime * Time.deltaTime);
        if (haveToSmile)
        {
            transform.position = Vector3.Lerp(transform.position, goTo_transform.position, 5 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startingPos, Time.deltaTime);
        }
    }

    private void Update()
    {
        smileFill.fillAmount = pressingTime / maxPressingTime;

        if (mcRonalds_Animator.GetCurrentAnimatorStateInfo(0).IsName("UiRollIn"))
        {
            appearanceTime -= Time.deltaTime;
        }
        
        if (appearanceTime <= 0)
        {
            reactionTime -= Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = normalBoss;

            haveToSmile = true;            
            
            if (mcRonalds_Animator.GetCurrentAnimatorStateInfo(0).IsName("UiRollOut") && reactionTime <= 0 && transform.position.x >= goTo_transform.position.x - 1)
            {
                reactionTime = maxAppearanceTime;
                appearanceTime = maxAppearanceTime;
                haveToSmile = false;
            }
            else if (reactionTime <= 0 && transform.position.x >= goTo_transform.position.x - 1)
            {
                reactionTime = maxAppearanceTime;
                appearanceTime = maxAppearanceTime;
                haveToSmile = false;
                GetComponent<SpriteRenderer>().sprite = angryBoss;
                StartCoroutine(PointsPenalty());
            }
        }
        if (haveToSmile && smilingPressed && pressingTime >= maxPressingTime)
        {
            reactionTime = maxAppearanceTime;
            appearanceTime = maxAppearanceTime;
            haveToSmile = false;
            GetComponent<SpriteRenderer>().sprite = happyBoss;
        }

        if (mcRonalds_Animator.GetCurrentAnimatorStateInfo(0).IsName("UiRollIn"))
        {
            if (Input.GetKey("e") && pressingTime < maxPressingTime)
            {
                pressingTime += 0.01f;
                smilingPressed = true;
            }            
        }
        if (Input.GetKeyUp("e"))
        {
            smilingPressed = false;
        }
        if (smilingPressed == false && pressingTime >= 0)
        {
            pressingTime -= 0.01f;
        }
        //pressingTime += 0.1f;      
    }

    IEnumerator PointsPenalty()
    {
        yield return new WaitForSeconds(0.1f);
        mcRonalds_Animator.GetComponentInChildren<DingButton>().scoreValue -= 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour
{
    BouncyMovmentScript movmentScript;
    public Image timeLeft_IMG; 

    public bool customerAvalible;
    private float timeLeft;

    public DingButton ding;
    public RandomOrder randomOrder;
    public Order order;

    public GameObject mcRonals_Pannel, canvas;

    public float max_timeLeft;

    [Header("Skins")]
    public Sprite [] skin;

    private void Start()
    {
        //randomOrder.enabled = true;
        movmentScript = GetComponent<BouncyMovmentScript>();
        customerAvalible = false;
        timeLeft = max_timeLeft;
        canvas.SetActive(false);

        var randonSkin = Random.Range(0,skin.Length);
        GetComponent<SpriteRenderer>().sprite = skin[randonSkin];
        randomOrder.OnClick();
    }

    private void Update()
    {
        if (timeLeft <= 0)
        {
            SkippedOrder();
            timeLeft = 1;
        }
        else if (movmentScript.stopped)
        {
            customerAvalible = true;
            canvas.SetActive(true);

            timeLeft -= Time.deltaTime;
            timeLeft_IMG.fillAmount = timeLeft / max_timeLeft;

        }
            // If the player didn't manage to finish the order                   
        if (ding.clientSatisfied)
        {
            timeLeft = max_timeLeft;
            canvas.SetActive(false);
            mcRonals_Pannel.GetComponent<Animator>().Play("UiRollOut");
            movmentScript.stopped = false;
            StartCoroutine(StopTheOrder());
            movmentScript.finishedOrder = true;
        }
    }

    private void OnMouseDown()
    {
        if (customerAvalible)
        {
            mcRonals_Pannel.SetActive(true);
            mcRonals_Pannel.GetComponent<Animator>().Play("UiRollIn");

            //randomOrder.enabled = true;
        }
    }

    public IEnumerator StopTheOrder()
    {
        yield return new WaitForSeconds(2.1339612f);

        switch (movmentScript.side)
        {
            case BouncyMovmentScript.Side.Left:
                if (transform.position.x <= movmentScript.target.position.x - 13)
                {
                    Destroy(gameObject);
                }

                break;
            case BouncyMovmentScript.Side.Right:
                if (transform.position.x <= movmentScript.target.position.x + 17)
                {
                    Destroy(gameObject);
                }
                break;
        }
        //randomOrder.enabled = false;
        movmentScript.finishedOrder = false;
        //mcRonals_Pannel.SetActive(false);
        customerAvalible = false;

        Debug.Log("Order Stoped");
    }

    public void SkippedOrder()
    {
        if (mcRonals_Pannel.activeSelf)
        {
            mcRonals_Pannel.GetComponent<Animator>().Play("UiRollOut");
        }
        else
        {
            mcRonals_Pannel.SetActive(true);
            mcRonals_Pannel.GetComponent<Animator>().Play("Empty");
        }
        ding.scoreValue -= 1;
        movmentScript.stopped = false;
        movmentScript.finishedOrder = true;
        customerAvalible = false;
        canvas.SetActive(false);
        StartCoroutine(ding.DisableOrder());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DingButton : MonoBehaviour
{
    public Image v, x, topBun;
    public RandomOrder randomOrder;
    public Order order;
    public GameObject Orders;

    public float scoreValue;
    public TextMeshProUGUI score, payDay_txt;

    private Vector3 topBun_position;

    public GameObject [] ingredients;

    public bool clientSatisfied;

    [Header("How much orders (Change only this)")]
    public int lenght;
    public int current_length;

    private bool isPayday = true;

    private void Start()
    {
        score.text = "Score: 0";
        topBun_position = topBun.rectTransform.localPosition;

        current_length = 0;
    }

    private void Update()
    {
        score.text = "Score: " + scoreValue;

        if (current_length >= lenght)
        {
            FinishWork();
        }
    }

    public void OnClick()
    {

        if ((randomOrder.isCheese == order.isCheese) && (randomOrder.isLettuce == order.isLettuce) && (randomOrder.isTomato == order.isTomato) && (randomOrder.isPickle == order.isPickle) && (randomOrder.isOnion == order.isOnion))
        {
            scoreValue += 1;
            v.enabled = true;
            clientSatisfied = true;
            //sound
        }
        else
        {
            x.enabled = true;
            //xSound.Play();
        }        
        StartCoroutine(DisableOrder());

    }

    public IEnumerator DisableOrder()
    {
        yield return new WaitForSeconds(2f);
        //Orders.SetActive(false);
        v.enabled = false;
        x.enabled = false;

        topBun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // Set topBun's body type to static
        topBun.rectTransform.localPosition = topBun_position;
        clientSatisfied = false;

        order.OReset();
        randomOrder.ResetOrder();
        //randomOrder.OnClick();

        foreach (GameObject ingri in ingredients)
        {
            ingri.SetActive(true);
        }


        current_length++;
        Debug.Log("Order Disabled");
    }

    public IEnumerator ExitScene(float seconds, int scene)
    {
        yield return new WaitForSecondsRealtime(seconds);

        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }

    void FinishWork()
    {
        if (isPayday)
        {
            // Salary calculation
            int salary = PlayerPrefs.GetInt("Salary");
            float final_salary = salary + (salary * (scoreValue / 10.0f));
            PlayerPrefs.SetInt("Salary", Convert.ToInt32(final_salary));

            payDay_txt.transform.parent.gameObject.SetActive(true);
            
            // Payday display in text 
            if (final_salary > salary)
                payDay_txt.text = "Payday : " + "<#00FF00>" + final_salary + "</color>";
            else if (final_salary < salary)
                payDay_txt.text = "Payday : " + "<#FF0000>" + final_salary + "</color>";
            else if (final_salary == salary)
                payDay_txt.text = "Payday : " + final_salary;

            StartCoroutine(ExitScene(5, 0));
            isPayday = false;

            Time.timeScale = 0.1f;
        }
        
    }
}   

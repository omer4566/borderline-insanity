using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomOrder : MonoBehaviour
{
    public TextMeshProUGUI cheese, lettuce, pickle, onion, tomato;
    public bool isCheese = false, isLettuce = false, isPickle = false, isOnion = false, isTomato = false;
    int current;
    TextMeshProUGUI currentTopping;


    public void OnClick ()
    {
        int random = Random.Range(1, 5);
        for (int i = 0; i <= random; i++)
        {
            TextMeshProUGUI[] Toppings = new TextMeshProUGUI[] { cheese, lettuce, pickle, onion, tomato };
            current = Random.Range(0, 5);
            currentTopping = Toppings[current];

            if (currentTopping == cheese)
            {
                isCheese = true;
                cheese.enabled = true;
            }
            if (currentTopping == lettuce)
            {
                isLettuce = true;
                lettuce.enabled = true;
            }
            if (currentTopping == onion)
            {
                isOnion = true;
                onion.enabled = true;
            }
            if (currentTopping == tomato)
            {
                isTomato = true;
                tomato.enabled = true;
            }
            if (currentTopping == pickle)
            {
                isPickle = true;
                pickle.enabled = true;
            }
        }
    }

    public void ResetOrder()
    {
        isCheese = false; isLettuce = false; isPickle = false; isOnion = false; isTomato = false;
        cheese.enabled = false; lettuce.enabled = false; pickle.enabled = false; onion.enabled = false; tomato.enabled = false;
    }
}

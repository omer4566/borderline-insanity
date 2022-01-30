using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public int counter = 0;
    string extras;
    public Image cheese, lettuce, pickle, onion, tomato;
    public bool isCheese = false, isLettuce = false, isPickle = false, isOnion = false, isTomato = false;
    Image currentImage;


    void main()
    {
        switch (extras)
        {
            case "Cheese":
                currentImage = cheese; 
                break;
            case "Lettuce":
                currentImage = lettuce;
                break;
            case "Pickle":
                currentImage = pickle;
                break;
            case "Tomato":
                currentImage = tomato;
                break;
            case "Onion":
                currentImage = onion;
                break;

        }

        switch (counter) {
            case 1:
                currentImage.rectTransform.localPosition = new Vector2(12, -68);
                break;
            case 2:
                currentImage.rectTransform.localPosition = new Vector2(12, -55);
                break;
            case 3:
                currentImage.rectTransform.localPosition = new Vector2(12, -42);
                break;
            case 4:
                currentImage.rectTransform.localPosition = new Vector2(12, -29);
                break;
            case 5:
                currentImage.rectTransform.localPosition = new Vector2(12, -16);
                break;
        }
    }

    public void OnclickCheese()
    {
        counter++;
        extras = "Cheese";
        main();
        isCheese = true;
    }
    public void OnClickLettuce()
    {
        counter++;
        extras = "Lettuce";
        main();
        isLettuce = true;
    }
    public void OnClickPickle()
    {
        counter++;
        extras = "Pickle";
        main();
        isPickle = true;
    }
    public void OnClickTomato()
    {
        counter++;
        extras = "Tomato";
        main();
        isTomato = true;
    }
    public void OnClickOnion()
    {
        counter++;
        extras = "Onion";
        main();
        isOnion = true;
    }
    public void OReset()
    {
        counter = 0;
        pickle.rectTransform.localPosition = new Vector2(1000, 3);
        onion.rectTransform.localPosition = new Vector2(1000, 3);
        tomato.rectTransform.localPosition = new Vector2(10500, 3);
        cheese.rectTransform.localPosition = new Vector2(10500, 3);
        lettuce.rectTransform.localPosition = new Vector2(1000, 3);
        isCheese = false;
        isOnion = false;
        isTomato = false;
        isPickle = false;
        isLettuce = false;
    }
}

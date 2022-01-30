using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Active : MonoBehaviour
{
    //Features
    private string name;
    private int cooldown;
    private GameObject gameObj;
    private Image img;
   
    public Active(string name, int cooldown, GameObject gameObj, Image img)
    {
        this.name = name;
        this.cooldown = cooldown;
        this.gameObj = gameObj;
        this.img = img;
    }
    public Active(Active a)
    {
        this.name = a.name;
        this.cooldown = a.cooldown;
        this.gameObj = a.gameObj;
        this.img = a.img;
    }
    public string getName() { return this.name; }
    public void setName(string name) { this.name = name;}
    public int getCooldown() { return this.cooldown; }
    public void setCooldown(int cooldown) { this.cooldown = cooldown; }
    public GameObject getGameObject() { return this.gameObj; }
    public void setGameObject(GameObject gameObj) { this.gameObj = gameObj; }
    public Image getImg() { return this.img; }
    public void setImg(Image img) { this.img = img; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour
{
    public bool explored;

    SpriteRenderer spriteRenderer;
    Color iniColour;

    public float fadingTime;

    float velo;
    private void Start()
    {
        explored = false;
        spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        explored = true;
    }

    private void LateUpdate()
    {
        iniColour = spriteRenderer.color;

        if (explored)
        {
            spriteRenderer.color = new Color(iniColour.r, iniColour.g, iniColour.b, Mathf.SmoothDamp(iniColour.a, 0 ,ref velo, fadingTime * Time.deltaTime));

            if (spriteRenderer.color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

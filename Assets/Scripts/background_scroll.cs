using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_scroll : MonoBehaviour
{

    public new GameObject camera; //put the new cause it was throwing a suggestion error thing
    private float backgroundWidth;
    
    void Start()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    void Update()
    {
        if (transform.position.x + backgroundWidth < camera.transform.position.x)
        {
            Vector2 newPos = new Vector2(transform.position.x + 2 * backgroundWidth - 0.1f, transform.position.y);
            transform.position = newPos;
        }
    }
}

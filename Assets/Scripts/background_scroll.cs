using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_scroll : MonoBehaviour
{

//had some merge conflicts, please look over this

    public float scrollSpeed = 1f;
    public float tileSize = 10f;

    private Vector3 startPosition;
    public new GameObject camera;
    private float backgroundWidth;
    private void Start() { }
    
    
    
    /*void Start()
    {
        startPosition = transform.position;
    }*/

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPosition + Vector3.back * newPosition;
    }
}

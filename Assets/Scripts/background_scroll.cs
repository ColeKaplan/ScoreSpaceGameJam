using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_scroll : MonoBehaviour
{

    public float scrollSpeed = 1f;
    public float tileSize = 10f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPosition + Vector3.back * newPosition;
    }
}

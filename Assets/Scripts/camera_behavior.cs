using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_behavior : MonoBehaviour
{

    public GameObject cowboy;
    
    void Start()
    {
        transform.position = new Vector3(0,0,-10);
    }

    
    void Update()
    {

        transform.position = new Vector3(cowboy.transform.position.x + 6.0f, 0, -10);
    }
}

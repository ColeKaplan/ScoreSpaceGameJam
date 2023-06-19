using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_behavior : MonoBehaviour
{

    public GameObject cowboy;
    public Camera cam;

    public float targetAspect = 0.5625f;  // 16:9 aspect ratio
    public float orthographicSize = 20;
    
    void Start()
    {
        transform.position = new Vector3(0,0,-10);
        /*cam = GetComponent<Camera>();
        float currentAspect = (float)Screen.width / Screen.height;
        cam.orthographicSize = orthographicSize * (targetAspect / currentAspect);*/
    }

    
    void Update()
    {

        transform.position = new Vector3(cowboy.transform.position.x + 6.0f, 0, -10);
        
    }
}

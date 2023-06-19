using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_behavior : MonoBehaviour
{

    public GameObject cowboy;
    public Camera cam;
    public AnimationCurve curve; 
    public float screenShakeDuration; 

    public float width = 16f;
    public float height = 9f;
    
    void Start()
    {
        transform.position = new Vector3(0,0,-10);
        cam = GetComponent<Camera>();

    }

    
    void Update()
    {

        transform.position = new Vector3(cowboy.transform.position.x + 6.0f, 0, -10);
        
        float targetAspect = width / height;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if(scaleHeight < 1.0f)
        {  
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
    public void startScreenShake()
    {
        StartCoroutine(screenShake());
    }
    public IEnumerator screenShake()
    {
        Debug.Log("screenshake");
        float elapsedTime = 0f; 

        while(elapsedTime < screenShakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / screenShakeDuration);
            transform.position = new Vector3(cowboy.transform.position.x + 6.0f, 0, -10) + Random.insideUnitSphere*strength;
            yield return null;
        }
        transform.position = new Vector3(cowboy.transform.position.x + 6.0f, 0, -10);
    }
}

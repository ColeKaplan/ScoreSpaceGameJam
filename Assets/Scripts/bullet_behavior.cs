using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_behavior : MonoBehaviour
{
    
    private float speed = 8;
    public GameObject camera;
    private float spinningInterval = .1f;
    private float timer = 0f; // Timer to track the elapsed time

    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * speed;
    }

    
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed;


        timer += Time.deltaTime; // Increase the timer by the elapsed time

        if (timer >= spinningInterval)
        {
            Spin();
            timer = 0f; // Reset the timer after shooting
            
        }

        /*if (!IsVisible(gameObject, camera.GetComponent<Camera>()))
        {
            Debug.Log("bullet destroyed - camera");
            Destroy(gameObject);
        }*/
        //transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("turret"))
        {
            Debug.Log("bullet destroyed - turret");
            Destroy(gameObject);
        }
    }

    bool IsVisible(GameObject gameObject, Camera camera)
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(gameObject.transform.position);
        return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    void Spin()
    {
        this.transform.Rotate(0f, 0f, 45f);
    }
}

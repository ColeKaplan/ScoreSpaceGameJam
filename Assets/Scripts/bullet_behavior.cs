using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_behavior : MonoBehaviour
{
    
    private float speed = 6f;
    public GameObject camera;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (!IsVisible(gameObject, camera.GetComponent<Camera>()))
        {
            Debug.Log("bullet destroyed - camera");
            Destroy(gameObject);
        }
        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
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
}

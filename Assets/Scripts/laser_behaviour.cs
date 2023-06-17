using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_behaviour : MonoBehaviour
{

    public float speed = 4;
    private float timer = 0f; // Timer to track the elapsed time
    private float killTime = 10.0f;
    public float movementDelay = .5f;

    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * speed;
        StartCoroutine(KillTimer());
    }


    void Update()
    {
        
        timer += Time.deltaTime; // Increase the timer by the elapsed time

        if (timer >= movementDelay)
        {
            //gameObject.GetComponent<Rigidbody2D>().velocity = transform.rotation * Vector3.right * speed;
            gameObject.transform.position += transform.rotation * Vector3.right * speed;
            timer = 0f; // Reset the timer after shooting

        }

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

    IEnumerator KillTimer()
    {
        float elapsed = 0;
        while (elapsed < killTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}

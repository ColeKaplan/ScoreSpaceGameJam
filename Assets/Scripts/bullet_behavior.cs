using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_behavior : MonoBehaviour
{
    
    private float speed = 12;
    public new GameObject camera;
    private float spinningInterval = .1f;
    private float timer = 0f; // Timer to track the elapsed time
    private float killTime = 2.0f;

    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * speed;
        StartCoroutine(KillTimer());
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
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("turret"))
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<turret_behaviour>().getHit(1);
        }
        if (other.gameObject.CompareTag("laser"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
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

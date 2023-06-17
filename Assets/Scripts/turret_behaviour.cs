using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_behaviour : MonoBehaviour
{

    public enum TurretType
    {
        LINE,
        FOLLOWING,
        OCTO
    }

    public TurretType turretType;
    public GameObject laserPrefab;
    public GameObject cowboyPrefab;
    public int health = 3;
    public float shootingInterval = 1f;

    private float timer = 0f; // Timer to track the elapsed time


    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Orient turret upwards

    }

// Update is called once per frame
void Update()
    {
        if (turretType == TurretType.FOLLOWING)
        {
            if (cowboyPrefab != null)
            {
                Vector3 targetDirection = cowboyPrefab.transform.position - transform.position;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            }
        }

        timer += Time.deltaTime; // Increase the timer by the elapsed time

        if (timer >= shootingInterval)
        {
            Shoot();
            timer = 0f; // Reset the timer after shooting
        }

        if (turretType == TurretType.OCTO)
        {
            for (int i = 0; i < 8; i++)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, 45f * i);
                GenerateLaser(rotation);
            }
        }

        if (transform.position.x < cowboyPrefab.transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (laserPrefab != null)
        {
            GenerateLaser(transform.rotation);
        }
    }

    private void GenerateLaser(Quaternion rotation)
    {
        //rotation *= Quaternion.Euler(0f, 90f, 0f);
        Vector3 spawnPosition = transform.position + (rotation * Vector3.up * 0.5f); // Distance from turret

        GameObject laser = Instantiate(laserPrefab, spawnPosition, rotation);
        // Add any necessary logic to handle the laser prefab behavior

        // Adjust the laserPrefab instantiation as needed based on your game's requirements
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
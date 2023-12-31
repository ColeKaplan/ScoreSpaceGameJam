using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class turret_behaviour : MonoBehaviour
{

    public enum TurretType
    {
        LINE,
        FOLLOWING,
        OCTO,
        DOUBLE
    }

    public TurretType turretType;
    public GameObject laserPrefab;
    public GameObject cowboyPrefab;
    public GameObject hatPrefab; 
    public int health = 3;
    public float shootingInterval = 1f;
    public int maxDroppedHats = 3;
    private bool canShoot = true;

    public float timer = 0f; // Timer to track the elapsed time


    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Orient turret upwards

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

        if (timer >= shootingInterval && turretType != TurretType.OCTO)
        {
            Shoot();
            timer = 0f; // Reset the timer after shooting
        }

        if (timer >= shootingInterval && turretType == TurretType.OCTO)
        {
            Shoot();
            timer = 0f;
        }

        if (transform.position.x < cowboyPrefab.transform.position.x - 2)
        {
            canShoot = false;
        }

        if (transform.position.x < cowboyPrefab.transform.position.x - 5)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (laserPrefab != null && canShoot)
        {
            GenerateLaser(transform.rotation);
        }
    }

    private void GenerateLaser(Quaternion rotation)
    {
        if(turretType == TurretType.OCTO)
        {
            for (int i = 0; i < 7; i++)
            {
                Quaternion rotationOcto = Quaternion.Euler(0f, 0f, 45f * (i+1));
                Vector3 spawnPosition = this.transform.GetChild(i).position;

                GameObject laser = Instantiate(laserPrefab, spawnPosition, rotationOcto);
            }
        } else
        {
            rotation *= Quaternion.Euler(0f, 0f, 90f);
            Vector3 spawnPosition = this.transform.GetChild(0).position;

            GameObject laser = Instantiate(laserPrefab, spawnPosition, rotation);

            if (turretType == TurretType.DOUBLE)
            {
                Vector3 spawnPosition2 = this.transform.GetChild(1).position;

                GameObject laser2 = Instantiate(laserPrefab, spawnPosition2, rotation);
            }
        }

        
      
    }

    public void getHit(int damage)
    {
        health -= damage;
        /*heartCanvas.GetComponent<HeartScript>().healthSet(health);
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
        }
        //Debug.Log("player took " + damage + "damage");*/
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Camera.main.GetComponent <camera_behavior>().startScreenShake(); 
            //heartCanvas.GetComponent<DarkScreen>().darken();
            for(int i = 0; i<Random.Range(1, maxDroppedHats+1); i++)
            {
                var randomOffset = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
                var position = randomOffset + this.transform.position; 
                GameObject droppedHat = Instantiate(hatPrefab, position, Quaternion.identity);
            }
        }
    }

    public void setCowboy(GameObject cowboy)
    {
        Debug.Log("changed");
        cowboyPrefab = cowboy;
    }

    //bullet behaviour detects the collision
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }*/
}
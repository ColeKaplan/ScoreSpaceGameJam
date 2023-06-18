using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_behavior : MonoBehaviour
{

    public int health = 6;
    private bool inPlay;
    private bool canThrow;
    private HeroState state;
    private float speed = 3.5f;
    private float throwCooldown = 0.2f;
    public int coins;
    public int hats;
    private float distanceToBank;
    private float distancex;
    private Vector2 previousPosition;

    public GameObject bullet;
    public GameObject bank;
    private GameObject bankInstance;

    //Add this if we want hearts on the screen
    //public Canvas heartCanvas;

    void Start()
    {
        transform.position = new Vector2(-6.0f, 0);
        inPlay = false;
        startGame();
    }

    void Update()
    {
        if (inPlay)
        {
            if (state == HeroState.WalkingToBank)
            {
                float distance = Vector2.Distance(bankInstance.transform.position, transform.position);
                if (distance <= 1.5f)
                {
                    state = HeroState.AtBank;
                }
            } else if (state == HeroState.AtBank)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = HeroState.Walking;
                }
            } else if (distancex >= distanceToBank)
            {
                state = HeroState.WalkingToBank;
                Vector3 position = transform.position + new Vector3(12, 2, 0);
                bankInstance = Instantiate(bank, position, Quaternion.identity);
                distancex = 0;
                distanceToBank = Random.Range(100f, 200f);
                previousPosition = transform.position;
            } else if (Input.GetKey(KeyCode.DownArrow))
            {
                state = HeroState.WalkingDown;
            } else if (Input.GetKey(KeyCode.UpArrow))
            {
                state = HeroState.WalkingUp;
            } else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (hats > 0 && canThrow)
                { 
                    canThrow = false;
                    ThrowHat();
                }
            } else
            {
                state = HeroState.Walking;
            }

            switch (state)
            {
                case HeroState.Walking:
                    transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                    break;
                case HeroState.WalkingDown:
                    transform.Translate(new Vector2(speed * Time.deltaTime, -speed * Time.deltaTime));
                    break;
                case HeroState.WalkingUp:
                    transform.Translate(new Vector2(speed * Time.deltaTime, speed * Time.deltaTime));
                    break;
                case HeroState.WalkingToBank:
                    Vector2 targetPosition = new Vector2(bankInstance.transform.position.x, bankInstance.transform.position.y - 1); 
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    break;
                case HeroState.AtBank:
                    break;
                default:
                    break;
                
            }
            distancex = transform.position.x - previousPosition.x;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            coins += 1;
            Destroy(other.gameObject);
        }
    }

    private void startGame()
    {
        inPlay = true;
        canThrow = true;
        state = HeroState.Walking;
        coins = 0;
        hats = 10;
        distanceToBank = Random.Range(100f, 200f);
        distancex = 0;
        previousPosition = transform.position;
    }

    private void ThrowHat()
    {
        Vector3 position = transform.position + new Vector3(1, 0, 0);
        GameObject instance = Instantiate(bullet, position, Quaternion.identity);
        hats -= 1;
        StartCoroutine(ThrowCooldown());
    }

    private enum HeroState
    {
        Walking,
        WalkingDown,
        WalkingUp,
        WalkingToBank,
        AtBank
    }

    IEnumerator ThrowCooldown()
    {
        float elapsed = 0f;
        while (elapsed < throwCooldown)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        canThrow = true;
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
            Debug.Log("dead");
            //heartCanvas.GetComponent<DarkScreen>().darken();
        }
    }
    //if we implement health

}

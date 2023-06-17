using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_behavior : MonoBehaviour
{

    private bool inPlay;
    private bool canThrow;
    private HeroState state;
    private float speed = 3.5f;
    private float throwCooldown = 0.2f;
    public int coins;
    public int hats;

    public GameObject bullet;

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
            if (Input.GetKey(KeyCode.DownArrow))
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
                
            }
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
        WalkingUp
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

}

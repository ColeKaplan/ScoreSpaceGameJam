using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_behavior : MonoBehaviour
{

    private bool inPlay;
    private HeroState state;
    private float speed = 5.0f;
    public int coins;

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
        Debug.Log("coin!");
        if (other.gameObject.CompareTag("coin"))
        {
            Debug.Log("coin!");
            coins += 1;
            Destroy(other.gameObject);
        }
    }

    private void startGame()
    {
        inPlay = true;
        state = HeroState.Walking;
        coins = 0;
    }

    private enum HeroState
    {
        Walking,
        WalkingDown,
        WalkingUp
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hero_behavior : MonoBehaviour
{

    public int health = 6;
    private bool inPlay;
    private bool canThrow;
    private HeroState state;
    private float verticalSpeed = 5f;
    private float horizontalSpeed = 4f;
    private float throwCooldown = 0.2f;
    public int coins;
    public int hats;
    public int hatsInBank;
    private float distanceToBank;
    private float distancex;
    private Vector2 previousPosition;

    public GameObject bullet;
    public GameObject bank;
    private GameObject bankInstance;
    public GameObject blackScreen;
    public GameObject bankScene;

    private Animator blackAnimator;
    private Animator bankAnimator;

    //Add this if we want hearts on the screen
    //public Canvas heartCanvas;

    void Start()
    {
        transform.position = new Vector2(-6.0f, 0);
        inPlay = false;
        blackAnimator = blackScreen.GetComponent<Animator>();
        bankAnimator = bankScene.GetComponent<Animator>();
        bankScene.GetComponent<Image>().enabled = false;
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
                    blackAnimator.SetTrigger("FadeToBlack");
                    StartCoroutine(ToggleBankScene());

                }
            } else if (state == HeroState.AtBank)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = HeroState.Walking;
                    blackAnimator.SetTrigger("FadeToBlack");
                    StartCoroutine(ToggleBankScene());
                }
            } else if (distancex >= distanceToBank)
            {
                state = HeroState.WalkingToBank;
                Vector3 position = transform.position + new Vector3(18, 3, 0);
                bankInstance = Instantiate(bank, position, Quaternion.identity);
                distancex = 0;
                distanceToBank = Random.Range(30f, 40f);
                previousPosition = transform.position;
            } else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (hats > 0 && canThrow)
                { 
                    canThrow = false;
                    ThrowHat();
                }
            } else if (Input.GetKey(KeyCode.DownArrow))
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
                    transform.Translate(new Vector2(horizontalSpeed * Time.deltaTime, 0));
                    GiveInterest();
                    break;
                case HeroState.WalkingDown:
                    transform.Translate(new Vector2(horizontalSpeed * Time.deltaTime, -verticalSpeed * Time.deltaTime));
                    GiveInterest();
                    break;
                case HeroState.WalkingUp:
                    GiveInterest();
                    transform.Translate(new Vector2(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime));
                    break;
                case HeroState.WalkingToBank:
                    Vector2 targetPosition = new Vector3(bankInstance.transform.position.x, bankInstance.transform.position.y - 1, 5); 
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, horizontalSpeed * Time.deltaTime);
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
        hatsInBank = 0;
        distanceToBank = Random.Range(30f, 40f);
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
    
    IEnumerator ToggleBankScene()
    {
        float elapsed = 0f;
        float duration = 0.35f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        bankScene.GetComponent<Image>().enabled = bankScene.GetComponent<Image>().enabled ? false : true;
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
            SceneManager.LoadScene("Leaderboard");
            //heartCanvas.GetComponent<DarkScreen>().darken();
        }
    }

    public void Deposit()
    {
        if (hats > 0)
        {
            hats--;
            hatsInBank++;
            Debug.Log("Hats left: " + hats);
        } else 
        {
            Debug.Log("No more hats!");
        }
    }

    public void Withdraw()
    {
        if (hatsInBank > 0)
        {
            hats++;
            hatsInBank--;
            Debug.Log("Hats In Bank: " + hatsInBank);
        } else 
        {
            Debug.Log("No more hats in bank!");
        }
    }

    private void GiveInterest()
    {
        hats += hats / 10;
    }

}

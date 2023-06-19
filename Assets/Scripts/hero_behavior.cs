using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hero_behavior : MonoBehaviour
{

    private int health = 6;
    private bool inPlay;
    private bool canThrow;
    private HeroState state;
    private float verticalSpeed = 5f;
    private float horizontalSpeed = 4f;
    private float throwCooldown = 0.1f;
    private int power;
    public int coins;
    public int hats;
    public int hatsInBank;
    private float secondPassed;
    private float distanceToBank;
    private float distancex;
    private int level = 0;
    private Vector2 previousPosition;

    private float enemySpawnDelay = 5f;
    private float enemyTimer = 2f;
    GameObject[][] sets = new GameObject[3][];

    public GameObject bullet;
    public GameObject bank;
    private GameObject bankInstance;
    public GameObject blackScreen;
    public GameObject bankScene;
    public GameObject heartCanvas;

    private Animator blackAnimator;
    private Animator bankAnimator;

    //Add this if we want hearts on the screen
    //public Canvas heartCanvas;

    void Start()
    {
        setupEnemy();
        
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
                level++;
                if(enemySpawnDelay > 3.5f)
                {
                    enemySpawnDelay -= .1f;
                }
                distanceToBank = Random.Range(100f, 200f);
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
            //Debug.Log("timer is: " + enemyTimer + " and spawn delay is: " + enemySpawnDelay);
            if ((state == HeroState.Walking || state == HeroState.WalkingDown || state == HeroState.WalkingUp) && enemyTimer >= enemySpawnDelay && distanceToBank - distancex >= 20) 
            {
                enemyTimer = 0;
                spawnEnemy();
            }

            distancex = transform.position.x - previousPosition.x;
            enemyTimer += Time.deltaTime;
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
        power = 1;
        coins = 0;
        hats = 10;
        hatsInBank = 0;
        secondPassed = 0;
        distanceToBank = Random.Range(100f, 200f);
        distancex = 0;
        previousPosition = transform.position;
    }

    private void ThrowHat()
    {
        Vector3 position = transform.position + new Vector3(1, 0, 0);
        GameObject instance = Instantiate(bullet, position, Quaternion.identity);
        instance.GetComponent<bullet_behavior>().setPower(power);
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
        heartCanvas.GetComponent<HeartScript>().healthSet(health);
        //Debug.Log("player took " + damage + "damage");
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

    public void Withdraw() {
        if (hatsInBank > 0) {
            hats++;
            hatsInBank--;
            Debug.Log("Hats In Bank: " + hatsInBank);
        } else {
            Debug.Log("No more hats in bank!");
        }
    }

    public void BuyHeart()
    {
        if (health < 6 && hats >= 10)
        {
            health++;
            hats -= 10;
            heartCanvas.GetComponent<HeartScript>().healthSet(health);
        }
    }

    public void BuyUpgrade()
    {
        if (hats >= 10)
        {
            power++;
            hats -= 10;
        }
    }

    private void GiveInterest()
    {
        if (secondPassed > 1)
        {
            if (hatsInBank < 10 || hatsInBank > 0)
            {
                hats++;
            }
            hatsInBank += hatsInBank / 10;
            secondPassed = 0;
        } else 
        {
            secondPassed += Time.deltaTime;
        }

    }

    public int getPower()
    {
        return power;
    }

    public int getHats()
    {
        return hats;
    }

    public int getHatsInBank()
    {
        return hatsInBank;
    }

    private void spawnEnemy()
    {

        int difficulty = (Random.Range(0, 3) + level) / 3;
        int random = Random.Range(0, 3);
        
        if(difficulty > 1)
        {
            difficulty = 1;
        }
        Debug.Log(level);

        //difficulty = 1;
        GameObject set = sets[difficulty][random];

        
        Vector3 position = this.transform.GetChild(0).position;
        position.y = 0;

        Instantiate(set, position, this.transform.GetChild(0).rotation);

        /*
        int children = set.transform.childCount;
        for(int i = 0; i < children; i++)
        {
            set.transform.GetChild(i).GetComponent<turret_behaviour>().setCowboy(this.gameObject);
        }
        */
        //set.gameObject.GetComponent<turret_behaviour>().setCowboy(this.gameObject);



        //sets = { { "Turrets_Easy_1", "Turrets_Easy_2", "Turrets_Easy_3" }, { "Turrets_Medium_1", "Turrets_Medium_2", "Turrets_Medium_3" } };
    }

    private void setupEnemy()
    {
        sets[0] = new GameObject[3];
        sets[1] = new GameObject[3];
        sets[2] = new GameObject[3];

        sets[0][0] = (Resources.Load<GameObject>("Turret_Patterns/Turrets_Easy_1"));
        sets[0][1] = (Resources.Load<GameObject>("Turret_Patterns/Turrets_Easy_2"));
        sets[0][2] = (Resources.Load<GameObject>("Turret_Patterns/Turrets_Easy_3"));
        sets[1][0] = (Resources.Load<GameObject>("Turret_Patterns/Turrets_Medium_1"));
        sets[1][1] = (Resources.Load<GameObject>("Turret_Patterns/Turrets_Medium_2"));
        sets[1][2] = (Resources.Load<GameObject>("Turret_Patterns/Turrets_Medium_3"));

        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                GameObject set = sets[i][j];
                int children = set.transform.childCount;
                for (int k = 0; k< children; k++)
                {
                    set.transform.GetChild(k).GetComponent<turret_behaviour>().setCowboy(this.gameObject);
                }
            }
        }


       // Debug.Log(sets[0][0]);
    }

}

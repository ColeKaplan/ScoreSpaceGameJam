using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour
{
    private GameObject canvas;
    public Texture2D heart;
    public Texture2D halfHeart;
    public Texture2D noHeart;

    private GameObject heartObject1;
    private GameObject heartObject2;
    private GameObject heartObject3;

    private RectTransform trans1;
    private RectTransform trans2;
    private RectTransform trans3;

    private Image image1;
    private Image image2;
    private Image image3;

    private GameObject[] hearts;
    private int health = 6;
    //public Sprite refSprite;

    // Use this for initialization
    void Start()
    {

        canvas = gameObject;

        heartObject1 = new GameObject("heart1");
        heartObject2 = new GameObject("heart2");
        heartObject3 = new GameObject("heart3");

        trans1 = heartObject1.AddComponent<RectTransform>();
        trans1.transform.SetParent(canvas.transform); // setting parent
        trans1.localScale = Vector3.one;
        trans1.anchoredPosition = new Vector2(-375, 150); // setting position, will be on center
        trans1.sizeDelta = new Vector2(30, 30); // custom size

        trans2 = heartObject2.AddComponent<RectTransform>();
        trans2.transform.SetParent(canvas.transform); // setting parent
        trans2.localScale = Vector3.one;
        trans2.anchoredPosition = new Vector2(-345, 150); // setting position, will be on center
        trans2.sizeDelta = new Vector2(30, 30); // custom size

        trans3 = heartObject3.AddComponent<RectTransform>();
        trans3.transform.SetParent(canvas.transform); // setting parent
        trans3.localScale = Vector3.one;
        trans3.anchoredPosition = new Vector2(-315, 150); // setting position, will be on center
        trans3.sizeDelta = new Vector2(30, 30); // custom size


        image1 = heartObject1.AddComponent<Image>();
        image1.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));


        image2 = heartObject2.AddComponent<Image>();
        image2.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));


        image3 = heartObject3.AddComponent<Image>();
        image3.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        //imgObject.transform.SetParent(canvas.transform);



        /*
        int count = 0;
        hearts = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            makeHeart(leftMost + spacing*i, height, heart, "heart"+i, i, hearts);
        }
        for(int i = 0; i< hearts.Length; i++)
        {
            print(hearts[i]);
        }
        */



    }
    // Update is called once per frame
    void Update()
    {
        //print(canvas.gameObject.transform.GetChild(0).gameObject);
        //print(health);

        if (health >= 6)
        {
            image1.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image2.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image3.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        }
        else if (health >= 5)
        {
            image1.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image2.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image3.sprite = Sprite.Create(halfHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        }
        else if (health >= 4)
        {
            image1.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image2.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image3.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        }
        else if (health >= 3)
        {
            image1.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image2.sprite = Sprite.Create(halfHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image3.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        }
        else if (health >= 2)
        {
            image1.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image2.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image3.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        }
        else if (health >= 1)
        {
            image1.sprite = Sprite.Create(halfHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image2.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image3.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        }
        else if (health >= 0) //dead
        {
            image1.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image2.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
            image3.sprite = Sprite.Create(noHeart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));
        }


    }

    /*
    void updateHearts(int health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            print("inside method:    " + hearts[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            print(hearts);
            if (hearts.Length > 0)
            {
                Destroy(hearts[i]);
            }
            
            //Destroy(canvas.gameObject.transform.GetChild(i).gameObject);


            if (health >= i * 2 + 2)
            {
                makeHeart(leftMost + spacing * i, height, heart, "heart" + i + "test", i, hearts);
            } else if (health >= i * 2 + 1)
            {
                makeHeart(leftMost + spacing * i, height, halfHeart, "heart" + i + "test", i, hearts);
            } else
            {
                makeHeart(leftMost + spacing * i, height, noHeart, "heart" + i + "test", i, hearts);
            }
        }
    }
    */

    public void healthSet(int health)
    {
        //print("test for child in method:  " + canvas.gameObject.transform.GetChild(0).gameObject);
        //print("health is:       " + health);

        //updateHearts(health);
        this.health = health;
    }
    /*
    void makeHeart(int x, int y, Texture2D tex, string name, int position, GameObject[] hearts)
    {
        canvas = gameObject;
        GameObject imgObject = new GameObject(name);

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(x, y); // setting position, will be on center
        trans.sizeDelta = new Vector2(30, 30); // custom size

        Image image = imgObject.AddComponent<Image>();
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imgObject.transform.SetParent(canvas.transform);
        hearts[position] = imgObject;
        print("here" + hearts[position] + "   " + hearts.Length);
        print("test for child:  " + canvas.gameObject.transform.GetChild(position).gameObject);
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankHatDisplayBehavior : MonoBehaviour
{
    
    public GameObject cowboy;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        text.text = "Bank " + cowboy.GetComponent<hero_behavior>().getHatsInBank();
    }
}

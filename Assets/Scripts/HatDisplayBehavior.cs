using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HatDisplayBehavior : MonoBehaviour
{
    
    public GameObject cowboy;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        text.text = "Hats " + cowboy.GetComponent<hero_behavior>().getHats();
    }
}

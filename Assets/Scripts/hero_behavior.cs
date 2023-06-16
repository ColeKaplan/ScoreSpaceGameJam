using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_behavior : MonoBehaviour
{

    private bool inPlay;
    private HeroState heroState;

    // Start is called before the first frame update
    void Start()
    {
        inPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlay)
        {
            switch (heroState)
            {
                case HeroState.Walking:
                    break;
                case HeroState.WalkingDown:
                    break;
                case HeroState.WalkingUp:
                    break;
                
            }
        }
    }

    private enum HeroState
    {
        Walking,
        WalkingDown,
        WalkingUp,
        

    }

}

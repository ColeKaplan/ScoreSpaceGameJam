using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class GuestLogin : MonoBehaviour
{

    public enum GameState { MenuIdle, LoggingIn, Error, LoggedIn, Play};
    public GameState gameState = GameState.MenuIdle;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress()
    {
        switch (gameState)
        {
            case GameState.MenuIdle:
                Login();
                break;
            case GameState.Error:
                Login();
                break;
            case GameState.LoggedIn:
                Play();
                break;
            case GameState.Play:
                gameState = GameState.MenuIdle;
                break;
            default:
                break;
        }
    }

    private void Login()
    {
        gameState = GameState.LoggingIn;
        StartCoroutine(GuestLoginRoutine());
    }

    private void Play()
    {
        gameState = GameState.Play;
    }

    private IEnumerator GuestLoginRoutine()
    {
        bool gotResponse = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                gotResponse = true;
                gameState = GameState.LoggedIn;
            } else
            {
                gameState = GameState.Error;
                gotResponse = true;
            }
        });

        yield return new WaitWhile(() => gotResponse == false);
    }
}

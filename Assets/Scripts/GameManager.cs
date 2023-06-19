using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEditor;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;

    public Leaderboard leaderboard;
    void Start()
    {
        score = hero_behavior.hatsInBank;
        Debug.Log(score);   
        StartCoroutine(SetupRoutine());
        
    }

    [System.Obsolete]
    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.SubmitScoreRoutine(score);
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                done = true;
            } else
            {
                Debug.Log("successfully started LootLocker session");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            
        });
        yield return new WaitWhile(() => done == false);
    }
}
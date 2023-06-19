using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEditor;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Leaderboard leaderboard;
    void Start()
    {
        int score = hero_behavior.hatsInBank;
        leaderboard.SubmitScoreRoutine(score);
        leaderboard.score.text = "" + score;
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
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
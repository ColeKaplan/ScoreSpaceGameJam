using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("Play button was clicked");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void debugquit()
    {
        Debug.Log("Back button was clicked");
    }
}

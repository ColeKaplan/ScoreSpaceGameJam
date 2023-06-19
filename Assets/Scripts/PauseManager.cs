using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Reference to the pause menu UI canvas
    public GameObject pauseCanvas;

    // Tracks if the game is paused
    public bool isPaused = false;

    void Start()
    { 
        Time.timeScale = 1f; // Resume the game time
        isPaused = false;
        //pauseCanvas.SetActive(false);
    }
    
    private void Update()
    {
        // Check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // Check if the game window is not focused
        if (!Application.isFocused && !isPaused)
        {
            PauseGame();
        }
    }

    // Pause the game
    private void PauseGame()
    {
        Time.timeScale = 0f; // Stop the game time
        isPaused = true;
        pauseCanvas.SetActive(true);
    }

    // Resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game time
        isPaused = false;
        pauseCanvas.SetActive(false);
    }

    // Load a scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Play button was clicked");

    }

    public bool getPauseStatus()
    {
        return isPaused;
    }

    public void setPauseStatus(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }
}
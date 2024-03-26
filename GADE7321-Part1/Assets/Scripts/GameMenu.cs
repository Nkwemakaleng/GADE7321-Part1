using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void RestartGame()
    {
        // Reload the current scene to restart the game
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f; // Ensure time scale is set to normal after restarting
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

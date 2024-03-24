using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public AudioSource backgroundMusic;
    public GameObject player; // Reference to the player object to adjust sensitivity

    private float originalSensitivity;
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        originalSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeLevel", 1f);
        sensitivitySlider.value = originalSensitivity;
        Time.timeScale = 1f; // Ensure time scale is set to normal when game starts
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("VolumeLevel", volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        // Adjust mouse sensitivity by modifying player's look sensitivity
        player.GetComponent<PlayerController>().mouseSensitivity = sensitivity;
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f; // Ensure time scale is set to normal after restarting
    }
}



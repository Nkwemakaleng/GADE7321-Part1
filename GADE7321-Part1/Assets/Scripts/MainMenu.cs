using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Ensure volume slider reflects current volume level
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeLevel", 1f);
        audioSource.volume = volumeSlider.value;
        optionsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("CTF");
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        // Adjust volume based on slider value
        audioSource.volume = volume;
        // Save volume level for future use
        PlayerPrefs.SetFloat("VolumeLevel", volume);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    

    public int scoreToWin = 5;
    public TextMeshPro scoreTextRed;
    public TextMeshPro scoreTextBlue;
    public TextMeshPro winText;
    public GameObject redFlag;
    public GameObject blueFlag;
    public Transform redBase;
    public Transform blueBase;
    public GameObject redPlayer;
    public GameObject bluePlayer;
    public GameObject gameOverPanel;
    public GameObject PlayerUiScore;

    private int redScore = 0;
    private int blueScore = 0;

    private bool redFlagTaken = false;
    private bool blueFlagTaken = false;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        gameOverPanel.SetActive(false);
        
    }

    private void Start()
    {
        RespawnFlags();
        UpdateScoreUI();
    }

    private void Update()
    {
        FlagsReturnedCorrectly();
    }

    /*
    public void FlagReturned(bool isRed)
    {
        if (isRed)
        {
            redScore++;
            redFlagTaken = false;
        }
        else
        {
            blueScore++;
            blueFlagTaken = false;
        }

        UpdateScoreUI();

        if (redScore >= scoreToWin)
            GameOver("Red");
        else if (blueScore >= scoreToWin)
            GameOver("Blue");
        else
            RespawnFlags();
    }
    */

    private void GameOver(string winner)
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        PlayerUiScore.SetActive(true);
        winText.text = winner + " wins!";
        
       
    }

    private void UpdateScoreUI()
    {
        scoreTextRed.text = String.Empty;
        scoreTextRed.text = redScore.ToString();
        
        scoreTextRed.text = String.Empty;
        scoreTextBlue.text = blueScore.ToString();
    }

    public void RespawnFlags()
    {
        if (redFlagTaken && redFlag.transform.position == redBase.position || redFlag.transform.position == bluePlayer.transform.position)
        {
            Debug.Log("Red Flag Spawned");
            redFlag.transform.position = blueBase.position;
            redFlag.SetActive(true);
        }
        if (blueFlagTaken && blueFlag.transform.position == blueBase.position || blueFlag.transform.position == redPlayer.transform.position)
        {
            Debug.Log("Red Flag Spawned");
            blueFlag.transform.position = redBase.position;
            blueFlag.SetActive(true);
        }
    }

    public void FlagPicked(bool isRed)
    {
        if (isRed)
            redFlagTaken = true;
        else
            blueFlagTaken = true;
    }

    public void FlagsReturnedCorrectly()
    {
        if (redFlagTaken && redFlag.transform.position == redBase.position)
        {
            redScore++;
            Debug.Log("red score up ");
            redFlagTaken = false;
            Destroy(redFlag);
            redFlag.SetActive(true);
            UpdateScoreUI();
            RespawnFlags();
        }
        if (blueFlagTaken && blueFlag.transform.position == blueBase.position)
        {
            blueScore++;
            Debug.Log("red score up ");
            blueFlagTaken = false;
            Destroy(blueFlag);
            blueFlag.SetActive(true);
            UpdateScoreUI();
            RespawnFlags();
        }
        if (redScore >= scoreToWin)
            GameOver("Red");
        else if (blueScore >= scoreToWin)
            GameOver("Blue");

    }

}

using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int scoreToWin = 5;
    public Text scoreText;
    public Text winText;
    public GameObject redFlag;
    public GameObject blueFlag;
    public Transform redBase;
    public Transform blueBase;
    public GameObject redPlayer;
    public GameObject bluePlayer;

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
    }

    private void Start()
    {
        RespawnFlags();
        UpdateScoreUI();
    }

    private void Update()
    {
       
        
    }

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

    private void GameOver(string winner)
    {
        winText.text = winner + " wins!";
       
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Red: " + redScore + " - Blue: " + blueScore;
    }

    public void RespawnFlags()
    {
        if (redFlagTaken)
        {
            redFlag.transform.position = blueBase.position;
            redFlag.SetActive(true);
        }
        if (blueFlagTaken)
        {
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
            redFlagTaken = false;
            Destroy(redFlag);
            redFlag.SetActive(true);
            UpdateScoreUI();
        }
        if (blueFlagTaken && blueFlag.transform.position == blueBase.position)
        {
            blueScore++;
            blueFlagTaken = false;
            Destroy(blueFlag);
            blueFlag.SetActive(true);
            UpdateScoreUI();
        }

    
    
    }
    
    
}
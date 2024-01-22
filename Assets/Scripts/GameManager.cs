using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI timerText;
    public GameObject endBG;
    public TextMeshProUGUI gameResultText;
    public TextMeshProUGUI gameWinText; 
    public TextMeshProUGUI gameOverText; 
    public GameObject startAudioObject; 
    public GameObject winAudioObject;   
    public GameObject loseAudioObject;  
    public GameObject pickupAudioObject; 

    private int score = 0;
    private bool isGamePaused = true;
    public float countdownDuration = 3f;
    public float gameDuration = 10f;

    void Start()
    {
        StartCoroutine(Countdown(countdownDuration));
    }

    IEnumerator Countdown(float seconds)
    {
        float count = seconds;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        StartGame();
    }

    void StartGame()
    {
        isGamePaused = false;
        countdownText.gameObject.SetActive(false);
        StartCoroutine(StartTimer(gameDuration));
        ActivateStartAudio(); 
    }

    void ActivateStartAudio()
    {
        if (startAudioObject != null)
        {
            startAudioObject.SetActive(true);
        }
    }

    void ActivateWinAudio()
    {
        if (winAudioObject != null)
        {
            winAudioObject.SetActive(true);
        }
    }

    void ActivateLoseAudio()
    {
        if (loseAudioObject != null)
        {
            loseAudioObject.SetActive(true);
        }
    }

    void ActivatePickupAudio()
    {
        if (pickupAudioObject != null)
        {
            pickupAudioObject.SetActive(true);
        }
    }

    IEnumerator StartTimer(float duration)
    {
        float timeRemaining = duration;

        while (timeRemaining > 0 && !isGamePaused)
        {
            timerText.text = Mathf.RoundToInt(timeRemaining).ToString();
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }

        if (!isGamePaused)
        {
            if (score >= 10)
            {
                EndGame(true);
            }
            else
            {
                EndGame(false);
            }
        }
    }

    void EndGame(bool isWin)
    {
        isGamePaused = true;

        endBG.SetActive(true);

        if (isWin)
        {
            gameWinText.gameObject.SetActive(true);
            ActivateWinAudio();
        }
        else
        {
            gameOverText.gameObject.SetActive(true);
            ActivateLoseAudio();
        }
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public void IncreaseScore()
    {
        if (!isGamePaused)
        {
            score++;
            UpdateScoreUI();
            ActivatePickupAudio();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

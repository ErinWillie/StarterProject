using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public TextMeshProUGUI timerText;
    private float currentTime = 10f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            currentTime = 0f;
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
    }

    public bool IsTimeUp()
    {
        return currentTime <= 0f;
    }
}
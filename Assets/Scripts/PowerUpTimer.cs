using UnityEngine;
using TMPro;

public class PowerUpTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float TimeLeft;
    public bool TimerOn = false;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    void Start() 
    {
        timerText.gameObject.SetActive(false);  // Ensure the timer is hidden at the start
    }

    private void Update()
    {
        if (TimerOn)
        {   
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is up");
                TimeLeft = 0;
                TimerOn = false;
                timerText.gameObject.SetActive(false);  // Hide the timer
            }
        }
    }

    public void StartTimer(float duration)
    {
        TimeLeft = duration;
        TimerOn = true;
        timerText.gameObject.SetActive(true); // Show the timer
    }

    void updateTimer(float currentTime)
    {
        currentTime = Mathf.Clamp(currentTime, 0, Mathf.Infinity);

        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}", seconds + "s");
    }
}
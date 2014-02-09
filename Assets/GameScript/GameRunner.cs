using UnityEngine;
using System;
using System.Collections;

public delegate void DayChangeHandler();

public class GameRunner : MonoBehaviour
{

    public UILabel DateLabel;
    public UIProgressBar DateProgressBar;

    public int CurrentDay = 0;
    public bool Running = false;

    public event DayChangeHandler DayChanged;

    public float DayProgress = 5f / 30f;    // new day in 5/30 secs
    public float DayProgressTimer = 0f;

    public int MaxDay = 365 * 4 + 1;

    // Use this for initialization
    void Start()
    {
        UpdateDateLabel();
        DayChanged += UpdateDateLabel;
        if (!GameObject.Find("Intro"))
        {
            StartGame();
        }
    }

    void UpdateDateLabel()
    {
        DateLabel.text = GameRunner.GetDayString(CurrentDay);
        DateProgressBar.value = Math.Max(0, Math.Min(1, CurrentDay * 1.0f / MaxDay));
    }

    public void StartGame()
    {
        Running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            DayProgressTimer += Time.fixedDeltaTime;
            while (DayProgressTimer >= DayProgress)
            {
                DayProgressTimer -= DayProgress;
                CurrentDay++;
                DayChanged();
            }
        }
    }

    public static string GetDayString(int CurrentDay)
    {
        DateTime d = new DateTime(2001, 2, 9);
        d = d.AddDays(CurrentDay);
        return d.ToString("MMMM dd, yyyy");
    }
}

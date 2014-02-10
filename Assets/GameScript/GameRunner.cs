﻿using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections;

public delegate void DayChangeHandler();

public class GameRunner : MonoBehaviour
{

    public bool ThisIsDebug = false;

    private Vector3 grav;

    public NotiScript notis;

    public UILabel DateLabel;
    public UIProgressBar DateProgressBar;

    public int CurrentDay = 0;
    public bool Running = false;
    private EndGameEnum EndGameType = EndGameEnum.FeelNotRight;

    private static DateTime BaseDate = new DateTime(2001, 2, 9);

    public event DayChangeHandler DayChanged;

    private float DayProgress = 0.205f;  // new day in 0.2 secs
    public float DayProgressTimer = 0f;

    public int MaxDay = 365 * 4;

    public UILabel LabelMoneySwiss;
    public UILabel LabelMoneyNation;

    public int MoneySwiss = -30;
    public int MoneyNation = 40929;

    private Color ColorPos = new Color(0.25f, 0.25f, 0.25f);
    private Color ColorNeg = new Color(0.75f, 0.1f, 0.1f);

    private Color c_good = new Color(0f / 255f, 166f / 255f, 124f / 255f);
    private Color c_info = new Color(17f / 255f, 66f / 255f, 170f / 255f);
    private Color c_warn = new Color(255f / 255f, 19f / 255f, 0f / 255f);

    public UILabel TheeText;
    private int TheeTextTimer = -1;

    public GameObject pawnPrefab;

    private Color h_armed = new Color(0 / 255f, 121 / 255f, 41 / 255f);
    private Color h_lower = new Color(255 / 255f, 205 / 255f, 0 / 255f);
    private Color h_middle = new Color(231 / 255f, 58 / 255f, 152 / 255f);
    private Color h_high = new Color(6 / 255f, 31 / 255f, 112 / 255f);
    private Color h_family = new Color(255 / 255f, 108 / 255f, 0 / 255f);

    private Color m_max = new Color(107 / 255f, 236 / 255f, 59 / 255f);
    private Color m_good = new Color(52 / 255f, 208 / 255f, 186 / 255f);
    private Color m_normal = new Color(101 / 255f, 165 / 255f, 209 / 255f);
    private Color m_bad = new Color(255 / 255f, 173 / 255f, 0 / 255f);
    private Color m_angry = new Color(255 / 255f, 19 / 255f, 0 / 255f);

    public UILabel LabelSatisLower;
    public UILabel LabelSatisMiddle;
    public UILabel LabelSatisHigher;
    public UILabel LabelSatisArmed;
    public UILabel LabelSatisFamily;

    private enum EndGameEnum
    {
        FeelNotRight,
        TimeUp,
        OverthrownLower,
        OverthrownMiddle,
        OverthrownHigher,
        OverthrownArmed,
        BrokenFamily,
    };

    // Use this for initialization
    void Start()
    {
        grav = Physics.gravity;
        Physics.gravity = Vector3.zero;
        GameObject.Find("Intro").transform.localPosition -= new Vector3(0, 530.6501f + 189.6017f, 0);

        if (ThisIsDebug)
        {
            Destroy(GameObject.Find("Intro"));
            DayProgress = 0.1f;
        }

        UpdateDateLabel();
        DayChanged += UpdateDateLabel;
        UpdateMoneyLabel();
        SpawnPawn();
        if (!GameObject.Find("Intro") || ThisIsDebug)
        {
            StartGame();
        }
    }

    void UpdateDateLabel()
    {
        DateLabel.text = GameRunner.GetDayString(CurrentDay);
        DateProgressBar.value = Math.Max(0, Math.Min(1, CurrentDay * 1.0f / MaxDay));
    }

    void UpdateMoneyLabel()
    {
        //40,929 M§
        UpdateMoneyLabellet(LabelMoneySwiss,MoneySwiss);
        UpdateMoneyLabellet(LabelMoneyNation,MoneyNation);
    }

    private string FormatMoney(int val)
    {
        return val.ToString("n0") + "  M§";
    }

    private void UpdateMoneyLabellet(UILabel label,int val)
    {
        label.text = FormatMoney(val);
        if (val < 0) label.color = ColorNeg;
        else label.color = ColorPos;
    }

    public void StartGame()
    {

        Physics.gravity = grav;

        Running = true;
        //Camera.main.GetComponent<AudioSource>().Play();

        notis.AddNotis("Welcome, P.M. Thee Sinatra. You now have full control of Sarkhan Country." + GetDayStringShort(CurrentDay), c_info);

        DayChanged += TaxRevenue;
        DayChanged += YearlyIncome;

        DayChanged += YearlyMoodDecay;

        DayChanged += UpdatePawnHat;
        DayChanged += OverthrowCheck;

        DayChanged += TheeComment;

        DayChanged += EndGameCheck;

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


            UpdateMoneyLabel();
        }
    }

    public static string GetDayString(int CurrentDay)
    {
        DateTime d = BaseDate;
        d = d.AddDays(CurrentDay);
        return d.ToString("MMMM dd, yyyy");
    }

    public static string GetDayStringShort(int CurrentDay)
    {
        DateTime d = BaseDate;
        d = d.AddDays(CurrentDay);
        return " ["+d.ToString("MMM dd")+"]";
    }

    private void TaxRevenue()
    {
        DateTime d = BaseDate;
        d = d.AddDays(CurrentDay);
        if (d.Day == 1)
        {
            if (d.Month == 3)
            {
                // yearly extra
                int toAdd = Random.Range(5000, 20000);
                //Debug.Log(toAdd);
                MoneyNation += toAdd;
                notis.AddNotis("Nation earns " + FormatMoney(toAdd) + " from yearly tax revenue." + GetDayStringShort(CurrentDay), c_good);
            }
            else
            {
                // monthly normal
                int toAdd = Random.Range(100, 3000);
                //Debug.Log(toAdd);
                MoneyNation += toAdd;
                notis.AddNotis("Nation earns " + FormatMoney(toAdd) + " from monthly tax revenue." + GetDayStringShort(CurrentDay), c_good);
            }
        }
    }

    private void YearlyIncome()
    {
        DateTime d = BaseDate;
        d = d.AddDays(CurrentDay);
        if (d.Month == 2 && d.Day == 1)
        {
            int toAdd = 5;
            MoneySwiss += toAdd;
            notis.AddNotis("You earns " + FormatMoney(toAdd) + " from yearly paycheck." + GetDayStringShort(CurrentDay), c_good);
        }
    }

    private void YearlyMoodDecay()
    {
        DateTime d = BaseDate;
        d = d.AddDays(CurrentDay);
        if ((d.Month == 6 || d.Month == 12) && d.Day == 1)
        {
            SatisArmed--;
            SatisFamily--;
            SatisHigh--;
            SatisLower--;
            SatisMiddle--;
        }
    }

    BetterList<PawnAIScript> pawnArmed;
    BetterList<PawnAIScript> pawnLower;
    BetterList<PawnAIScript> pawnMiddle;
    BetterList<PawnAIScript> pawnHigher;
    BetterList<PawnAIScript> pawnFamily;

    //  SpawnPawn
    private void SpawnPawn()
    {

        pawnArmed = new BetterList<PawnAIScript>();
        pawnLower = new BetterList<PawnAIScript>();
        pawnMiddle = new BetterList<PawnAIScript>();
        pawnHigher = new BetterList<PawnAIScript>();
        pawnFamily = new BetterList<PawnAIScript>();

        Vector3 midPoint = new Vector3(51.0728f, 0.6888365f, -2.616897f);

        int numArmed = 4;
        int numLower = 15;
        int numMiddle = 7;
        int numHigh = 4;
        int numFamily = 1;

        for (int i = 0; i < numArmed + numLower + numMiddle + numHigh + numFamily; i++)
        {
            float randRad = Random.Range(0, 2 * Mathf.PI);
            Vector3 randDir = new Vector3(Mathf.Sin(randRad), 0, Mathf.Cos(randRad));
            Vector3 randPos = midPoint + randDir * Random.Range(0.2f, 4f);
            GameObject go = Instantiate(pawnPrefab, randPos, Quaternion.identity) as GameObject;
            PawnAIScript pa = go.GetComponent<PawnAIScript>();
            pa.CenterPoint = midPoint;
            if (i < numArmed)
            {
                pa.SetHatColor(h_armed);
                pawnArmed.Add(pa);
            }
            else if (i < numArmed + numLower)
            {
                pa.SetHatColor(h_lower);
                pawnLower.Add(pa);
            }
            else if (i < numArmed + numLower + numMiddle)
            {
                pa.SetHatColor(h_middle);
                pawnMiddle.Add(pa);
            }
            else if (i < numArmed + numLower + numMiddle + numHigh)
            {
                pa.SetHatColor(h_high);
                pawnHigher.Add(pa);
            }
            else
            {
                pa.SetHatColor(h_family);
                pawnFamily.Add(pa);
            }
        }

        UpdatePawnHat();

    }

    private void UpdatePawnHat()
    {
        for (int i = 0; i < 5; i++)
        {
            int curMood = 10;
            BetterList<PawnAIScript> list = pawnArmed;
            switch (i)
            {
                case 0:
                    curMood = SatisArmed;
                    list = pawnArmed;
                    SetMoodText(LabelSatisArmed, "Armed Force", curMood);
                    break;
                case 1:
                    curMood = SatisLower;
                    list = pawnLower;
                    SetMoodText(LabelSatisLower, "Lower Class", curMood);
                    break;
                case 2:
                    curMood = SatisMiddle;
                    list = pawnMiddle;
                    SetMoodText(LabelSatisMiddle, "Middle Class", curMood);
                    break;
                case 3:
                    curMood = SatisHigh;
                    list = pawnHigher;
                    SetMoodText(LabelSatisHigher, "Higher Class", curMood);
                    break;
                case 4:
                    curMood = SatisFamily;
                    list = pawnFamily;
                    SetMoodText(LabelSatisFamily, "Family", curMood);
                    break;
            }
            foreach (PawnAIScript pa in list)
            {
                if (curMood >= 10) pa.SetMoodColor(m_max);
                else if (curMood >= 7) pa.SetMoodColor(m_good);
                else if (curMood >= 4) pa.SetMoodColor(m_normal);
                else if (curMood >= 2) pa.SetMoodColor(m_bad);
                else pa.SetMoodColor(m_angry);
            }
        }
    }

    private void SetMoodText(UILabel label,string type, int curMood)
    {
        if (curMood >= 10)
        {
            label.text = type + " : Very Happy";
            label.color = m_max;
        }
        else if (curMood >= 7)
        {
            label.text = type + " : Happy";
            label.color = m_good;
        }
        else if (curMood >= 4)
        {
            label.text = type + " : Normal";
            label.color = m_normal;
        }
        else if (curMood >= 2)
        {
            label.text = type + " : Annoyed";
            label.color = m_bad;
        }
        else
        {
            label.text = type + " : Angry";
            label.color = m_angry;
        }
    }

    // satisfaction variable
    private int SatisLower = 8;
    private int SatisMiddle = 5;
    private int SatisHigh = 6;
    private int SatisArmed = 6;
    private int SatisFamily = 9;

    private bool WarnSatisLower = false;
    private bool WarnSatisMiddle = false;
    private bool WarnSatisHigh = false;
    private bool WarnSatisArmed = false;
    private bool WarnSatisFamily = false;

    private void OverthrowCheck()
    {

        // lower class
        if (SatisLower > 3)
        {
            WarnSatisLower = false;
        } 
        else if (SatisLower <= 3 && !WarnSatisLower)
        {
            WarnSatisLower = true;
            notis.AddNotis("Lower Class isn't satisfied. You must do something for them or they may get you in trouble.", c_warn);
        }
        else if (SatisLower <= 0)
        {
            EndGameType = EndGameEnum.OverthrownLower;
            EndGame();
        }

        // middle class
        if (SatisMiddle > 3)
        {
            WarnSatisMiddle = false;
        }
        else if (SatisMiddle <= 3 && !WarnSatisMiddle)
        {
            WarnSatisMiddle = true;
            notis.AddNotis("Middle Class isn't satisfied. You must do something for them or they may get you in trouble.", c_warn);
        }
        else if (SatisMiddle <= 0)
        {
            EndGameType = EndGameEnum.OverthrownMiddle;
            EndGame();
        }

        // higher class
        if (SatisHigh > 3)
        {
            WarnSatisHigh = false;
        }
        else if (SatisHigh <= 3 && !WarnSatisHigh)
        {
            WarnSatisHigh = true;
            notis.AddNotis("Higher Class isn't satisfied. You must do something for them or they may get you in trouble.", c_warn);
        }
        else if (SatisHigh <= 0)
        {
            EndGameType = EndGameEnum.OverthrownHigher;
            EndGame();
        }

        // armed
        if (SatisArmed > 3)
        {
            WarnSatisArmed = false;
        }
        else if (SatisArmed <= 3 && !WarnSatisArmed)
        {
            WarnSatisArmed = true;
            notis.AddNotis("Armed Force isn't satisfied. You must do something for them or they may get you in trouble.", c_warn);
        }
        else if (SatisArmed <= 0)
        {
            EndGameType = EndGameEnum.OverthrownArmed;
            EndGame();
        }

        // family
        if (SatisFamily > 3)
        {
            WarnSatisFamily = false;
        }
        else if (SatisFamily <= 3 && !WarnSatisFamily)
        {
            WarnSatisFamily = true;
            notis.AddNotis("Your Family isn't satisfied. You must do something for them or they may get you in trouble.", c_warn);
        }
        else if (SatisFamily <= 0)
        {
            EndGameType = EndGameEnum.BrokenFamily;
            EndGame();
        }

    }


    private bool comment_tooMuchMoney = false;

    private void TheeComment()
    {

        if (TheeTextTimer >= 0)
        {
            TheeTextTimer--;
            if (TheeTextTimer < 0) TheeText.text = "";
        }

        if (MoneyNation > 100000 && !comment_tooMuchMoney)
        {
            comment_tooMuchMoney = true;
            DoComment("I think there's too much money in the treasury.. should use it somehow");
        }
        else if (comment_tooMuchMoney && MoneyNation < 100000)
        {
            comment_tooMuchMoney = false;
        }

    }

    private void DoComment(string txt)
    {
        TheeTextTimer = 60;
        TheeText.text = txt;
    }

    private void EndGameCheck()
    {
        if (CurrentDay == MaxDay)
        {
            EndGameType = EndGameEnum.TimeUp;
            EndGame();
        }
    }


    private void EndGame()
    {
        Running = false;
        switch (EndGameType)
        {
            case EndGameEnum.FeelNotRight:
                notis.AddNotis("You feel that this isn't the right thing and resigned. You still made " + FormatMoney(MoneySwiss) + " from your career.", c_info);
                break;
            case EndGameEnum.TimeUp:
                notis.AddNotis("Your time as P.M. has ended. You made " + FormatMoney(MoneySwiss) + " from your career.", c_info);
                break;
            case EndGameEnum.OverthrownLower:
                notis.AddNotis("Lower Class protest against you and forced you to resign. You made " + FormatMoney(MoneySwiss) + " from your career.", c_info);
                break;
            case EndGameEnum.OverthrownMiddle:
                notis.AddNotis("Middle Class signed a petition and remove you from power. You made " + FormatMoney(MoneySwiss) + " from your career.", c_info);
                break;
            case EndGameEnum.OverthrownHigher:
                notis.AddNotis("Higher Class blackmail you and forced you to resign. You made " + FormatMoney(MoneySwiss) + " from your career.", c_info);
                break;
            case EndGameEnum.OverthrownArmed:
                notis.AddNotis("Armed force seized the power and forced you to flee the country. Your " + FormatMoney(MoneySwiss) + " is sill safe in Swiss Bank.", c_info);
                break;
            case EndGameEnum.BrokenFamily:
                notis.AddNotis("Your discovered that your wife has an affair. Feeling despair, you left everything and become a monk. You donated all " + FormatMoney(MoneySwiss) + " you made to 3ʳᵈ world country.", c_info);
                break;
        }
    }

}

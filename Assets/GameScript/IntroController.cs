using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour
{

    public bool skipping = false;

    private bool finished = false;

    // Use this for initialization
    void Start()
    {
        EventDelegate.Add(GameObject.Find("Intro/Text7").GetComponent<TweenAlpha>().onFinished, IntroFadedFinished);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("IntroLabel");
        foreach (GameObject go in gos)
        {
            TweenAlpha alp = go.GetComponent<TweenAlpha>();
            if (alp) alp.enabled = false;
        }
        for (int i = 0; i <= 6; i++)
        {
            GameObject go = GameObject.Find("Intro/Text" + (i + 1));
            TweenPosition pos = go.AddComponent<TweenPosition>();
            pos.from = go.transform.localPosition + new Vector3(3.5f, 0, 0);
            pos.to = go.transform.localPosition;
            pos.duration = 2.0f;
            pos.animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
            //pos.animationCurve.SmoothTangents(2, 10);
            pos.delay = GetDelay(i);
            if (i == 6)
            {
                EventDelegate.Add(pos.onFinished, IntroFinished);
            }
        }

        if (skipping)
        {
            /* for (int i = 0; i <= 6; i++)
            {
                GameObject go = GameObject.Find("Text" + (i + 1));
                TweenPosition pos = go.GetComponent<TweenPosition>();
                //pos.enabled = false;
            } */
            IntroFinished();
        }

    }

    private float GetDelay(int i)
    {
        switch (i)
        {
            case 0: return 0f;
            case 1: return 1.3f;
            case 2: return 1.5f;
            case 3: return 3.1f;
            case 4: return 3.3f;
            case 5: return 3.5f;
            case 6: return 5.4f;
            default: return 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IntroFadedFinished()
    {
        Debug.Log("Intro faded");
        GameObject.Find("GameRunner").GetComponent<GameRunner>().StartGame();
    }

    void IntroFinished()
    {
        if (finished) return;

        Debug.Log("Intro finished");
        /* GameObject[] gos = GameObject.FindGameObjectsWithTag("IntroLabel");
        foreach (GameObject go in gos)
        {
            TweenAlpha alp = go.GetComponent<TweenAlpha>();
            alp.delay = 0;
            alp.from = 1.0f;
            alp.to = 0f;
            alp.ResetToBeginning();
            alp.enabled = true;
            alp.PlayForward();
        } */

        

        int i = 0;
        for (i = 0; i <= 6; i++)
        {
            GameObject go = GameObject.Find("Intro/Text" + (i + 1));
            TweenPosition pos = go.GetComponent<TweenPosition>();
            pos.from = go.transform.localPosition;
            pos.to = go.transform.localPosition + GetVector(i);
            pos.delay = 1f;
            if (i == 6)
            {
                EventDelegate.Remove(pos.onFinished, IntroFinished);
                EventDelegate.Add(pos.onFinished, IntroFadedFinished);
            }
            pos.ResetToBeginning();
            pos.PlayForward();
        }

        GameObject.Find("Intro/Hider").GetComponent<TweenColor>().enabled = true;

        finished = true;

    }

    private Vector3 GetVector(int i)
    {
        switch (i)
        {
            case 0: return new Vector3(0, 3.5f, 0);
            case 1: return new Vector3(-3.5f, 0, 0);
            case 2: return new Vector3(3.5f, 0, 0);
            case 3: return new Vector3(3.5f, 0, 0);
            case 4: return new Vector3(-3.5f, 0, 0);
            case 5: return new Vector3(3.5f, 0, 0);
            case 6: return new Vector3(0, -3.5f, 0);
            default: return new Vector3(3.5f, 0, 0);
        }
    }

}

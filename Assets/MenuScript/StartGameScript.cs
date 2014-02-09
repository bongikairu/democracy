using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour
{

    private bool started = false;
    private float rolloff = -1f;
    private float volFrom = 1f;

    // Use this for initialization
    void Start()
    {
        EventDelegate.Add(GameObject.Find("Hider").GetComponent<TweenColor>().onFinished, LoadLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (rolloff >= 0f)
        {
            rolloff += Time.fixedDeltaTime;
            if (rolloff > 3.25f) rolloff = 3.25f;
            Camera.main.GetComponent<AudioSource>().volume = Mathf.Lerp(volFrom, 0, rolloff / 3.25f);
        }
    }

    void OnClick()
    {
        if (started) return;
        // start game click
        GameObject.Find("Sarkhan").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("Underline").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("StartGame").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("Warning").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("SarkhanThai").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("Hider").GetComponent<TweenColor>().enabled = true;
        rolloff = 0f;
        volFrom = Camera.main.GetComponent<AudioSource>().volume;
    }

    void LoadLevel()
    {
        Debug.Log("Loading new level");
        Application.LoadLevel("MainGame");
    }

}

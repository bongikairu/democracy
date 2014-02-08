using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour
{

    private bool started = false;

    // Use this for initialization
    void Start()
    {
        EventDelegate.Add(GameObject.Find("Hider").GetComponent<TweenColor>().onFinished, LoadLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        if (started) return;
        // start game click
        GameObject.Find("Sarkhan").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("Underline").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("StartGame").GetComponent<TweenPosition>().enabled = true;
        GameObject.Find("Hider").GetComponent<TweenColor>().enabled = true;
    }

    void LoadLevel()
    {
        Debug.Log("Loading new level");
        Application.LoadLevel("MainGame");
    }

}

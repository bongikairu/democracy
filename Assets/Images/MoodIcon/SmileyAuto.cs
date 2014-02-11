using UnityEngine;
using System.Collections;

public class SmileyAuto : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}

    public void Animate(Vector3 startPos)
    {

        this.transform.localPosition = startPos;

        TweenPosition tp = gameObject.AddComponent<TweenPosition>();
        tp.from = transform.localPosition;
        tp.to = transform.localPosition + new Vector3(0, 15, 0);
        tp.duration = 2.0f;
        tp.delay = 0f;
        tp.PlayForward();
        tp.ignoreTimeScale = false;
        TweenAlpha ta = gameObject.AddComponent<TweenAlpha>();
        ta.from = 0.8f;
        ta.to = 0f;
        ta.duration = 2.0f;
        ta.delay = 0f;
        ta.PlayForward();
        ta.ignoreTimeScale = false;
        EventDelegate.Add(ta.onFinished, Hided);
    }

    void Hided()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class ForwardScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void OnPress(bool isOver)
    {
        GameRunner.me.IsForward = isOver;
    }

}

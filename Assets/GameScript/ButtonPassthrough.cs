using UnityEngine;
using System.Collections;

public class ButtonPassthrough : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        //Debug.Log("clicked");
        transform.parent.GetComponent<ActionButtonScript>().OnClick();
    }

    void OnHover(bool isOver)
    {
        transform.parent.GetComponent<ActionButtonScript>().OnHover(isOver);
    }

}

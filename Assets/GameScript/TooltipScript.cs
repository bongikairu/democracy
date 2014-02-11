using UnityEngine;
using System.Collections;

public class TooltipScript : MonoBehaviour
{

    public static TooltipScript me;
    private TweenAlpha ta;

    // Use this for initialization
    void Start()
    {
        me = this;
        //gameObject.SetActive(false);

        ta = gameObject.AddComponent<TweenAlpha>();

        ta.from = 0f;
        ta.to = 0f;
        ta.duration = 0.1f;
        ta.delay = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = Input.mousePosition / Camera.main.pixelWidth * 1280f - new Vector3(640f, 360f, 0);
    }

    internal void Show()
    {
        ta.to = 1f;
        ta.SetStartToCurrentValue();
        ta.ResetToBeginning();
        ta.PlayForward();
    }

    internal void Hide()
    {
        ta.to = 0f;
        ta.SetStartToCurrentValue();
        ta.ResetToBeginning();
        ta.PlayForward();
    }

}

using UnityEngine;
using System.Collections;

class NotiNode
{

    public enum State
    {
        Appearing,
        Displaying,
        Leaving,
        Dead
    };
    
    public string text;
    public State state;
    public float startTime;
    public UILabel label;

    public NotiNode prevNode;
    public NotiNode nextNode;

    private float Age = 10f;

    internal NotiNode Progress()
    {

        if (prevNode != null)
        {
            if (prevNode.state == State.Dead)
            {
                prevNode = null;
            }
        }

        if (state == State.Dead)
        {
            if (nextNode != null) return nextNode.Progress();
            else return null;
        }
        else if (state == State.Leaving)
        {
            if (nextNode != null) nextNode.Progress();
            return this;
        }
        else
        {
            // calculate position
            if (prevNode != null && prevNode.state!=State.Leaving)
            {
                label.transform.localPosition = prevNode.label.transform.localPosition + new Vector3(0, -30, 0);
            }
            else
            {
                //label.transform.localPosition = new Vector3(620, 320, 0);
                TweenPosition tp = label.gameObject.GetComponent<TweenPosition>();
                if (tp == null) tp = label.gameObject.AddComponent<TweenPosition>();
                tp.from = label.transform.localPosition;
                tp.to = new Vector3(620, 320, 0);
                tp.duration = 0.5f;
                tp.delay = 0f;
                tp.ResetToBeginning();
                tp.PlayForward();
            }
            // check for ages
            if (state == State.Displaying && Time.fixedTime - startTime > Age)
            {
                state = State.Leaving;
                TweenPosition tp = label.gameObject.GetComponent<TweenPosition>();
                if (tp)
                {
                    //tp.enabled = false;
                }
                TweenAlpha ta = label.gameObject.GetComponent<TweenAlpha>();
                ta.from = 1f;
                ta.to = 0f;
                ta.duration = 0.5f;
                ta.delay = 0f;
                ta.ResetToBeginning();
                ta.PlayForward();
                EventDelegate.Add(ta.onFinished, DisappearFinished);
            }
            // progress of next node
            if (nextNode!=null) nextNode.Progress();
            return this;
        }
    }

    private static int notiCount = 1;

    internal static NotiNode CreateNode(NotiNode rhead,string txt,Color c)
    {
        NotiNode head = rhead;
        NotiNode n = new NotiNode();
        n.state = NotiNode.State.Appearing;
        n.text = txt;
        n.startTime = Time.fixedTime;
        n.nextNode = null;
        n.prevNode = null;

        GameObject root = GameObject.Find("UI Root");

        GameObject go = new GameObject();
        go.name = "Noti#" + (notiCount++);
        go.transform.parent = root.transform;

        n.label = go.AddComponent<UILabel>();
        n.label.text = txt;
        n.label.trueTypeFont = GameObject.Find("SwissMoneyValue").GetComponent<UILabel>().trueTypeFont;
        n.label.fontSize = 22;
        n.label.color = c;
        n.label.pivot = UIWidget.Pivot.Right;
        n.label.transform.localPosition = new Vector3(620, 320, 0);

        TweenAlpha ta = n.label.gameObject.AddComponent<TweenAlpha>();
        ta.from = 0f;
        ta.to = 1f;
        ta.duration = 0.5f;
        ta.delay = 0f;
        EventDelegate.Add(ta.onFinished, n.AppearFinished);

        if (head == null) head = n;
        else
        {
            NotiNode cur = head;
            while (cur.nextNode != null)
            {
                cur = cur.nextNode;
            }
            cur.nextNode = n;
            n.prevNode = cur;
            n.label.transform.localPosition = cur.label.transform.localPosition + new Vector3(0, -30, 0);
        }
        return head;
    }

    void AppearFinished()
    {
        EventDelegate.Remove(this.label.GetComponent<TweenAlpha>().onFinished, AppearFinished);
        this.state = State.Displaying;
        startTime = Time.fixedTime;
    }

    void DisappearFinished()
    {
        this.state = State.Dead;
        GameObject.Destroy(label.gameObject);
    }
}

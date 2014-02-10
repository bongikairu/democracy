using UnityEngine;
using System.Collections;

public class NotiScript : MonoBehaviour
{

    private NotiNode head;

    private float test = 0f;

    // Use this for initialization
    void Start()
    {
        //AddNotis("test");
        //AddNotis("test2");
        //AddNotis("test3"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (head != null) head = head.Progress();

        //test += Time.fixedDeltaTime;
        if (test > Random.Range(0.2f, 3f))
        {
            test = 0f;
            //AddNotis("test " + Random.Range(0, 65535));
        }
    }

    private void ChangeState(NotiNode n, NotiNode.State state)
    {
        throw new System.NotImplementedException();
    }

    public void AddNotis(string txt,Color c)
    {

        head = NotiNode.CreateNode(head, txt,c);

    }

}

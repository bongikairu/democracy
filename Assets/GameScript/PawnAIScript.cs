using UnityEngine;
using System.Collections;

public class PawnAIScript : MonoBehaviour
{

    public Vector3 CenterPoint;

    private float NewDirTimer = 999f;
    private float RandDirTime = 3f;
    private Vector3 curDir = Vector3.zero;

    private float walkCycle;
    private float walkSpeed;

    // Use this for initialization
    void Start()
    {
        RandDirTime = Random.Range(1, 10);
        NewDirTimer = RandDirTime + 1;
        walkCycle = Random.Range(0.8f, 2f);
        walkSpeed = Random.Range(0.15f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {

        float velocity = walkSpeed;
        float rad = 2.5f;
        float ringRad = 0.5f;

        NewDirTimer += Time.fixedDeltaTime;

        if (NewDirTimer > 3f)
        {
            NewDirTimer = 0f;
            RandDirTime = Random.Range(1, 10);

            Vector3 cur = transform.position;
            Vector3 planar = Vector3.Project(cur - CenterPoint, Vector3.forward + Vector3.right);
            //Debug.Log(planar);
            float dist = planar.magnitude;
            Vector3 dir = planar.normalized;
            if (dist > rad) dir *= -1;

            float randRad = Random.Range(0, 2 * Mathf.PI);
            Vector3 randDir = new Vector3(Mathf.Sin(randRad), 0, Mathf.Cos(randRad));

            float forcepower = ((dist < rad) || Mathf.Abs(dist - rad) < ringRad) ? 0 : 1;
            if (dir != Vector3.zero && forcepower!=0) randDir = Vector3.Project(randDir, Vector3.Cross(dir, Vector3.up));

            curDir = (randDir + dir * forcepower).normalized;
        }

        Debug.DrawLine(transform.position, CenterPoint);

        transform.position += curDir * velocity * Time.fixedDeltaTime * (0.5f + 0.5f * Mathf.Abs(Mathf.Sin(Time.fixedTime / walkCycle * Mathf.PI)));

    }

    public void SetHatColor(Color c)
    {
        transform.FindChild("Hat").GetComponent<MeshRenderer>().material.color = c;
    }

    public void SetMoodColor(Color c)
    {
        transform.FindChild("Quad").GetComponent<MeshRenderer>().material.color = c;
    }

}

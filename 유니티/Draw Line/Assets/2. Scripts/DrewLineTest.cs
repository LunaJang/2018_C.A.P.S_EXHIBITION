using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrewLineTest : MonoBehaviour {
    private LineRenderer line;
    private Vector3 mousePos;
    private Vector3 startPos;
    private Vector3 endPos;
    private List<Vector3> mPos = new List<Vector3>();
    private Vector3[] points;


    private void Start()
    {
        points = null;
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            if (mPos != null)
            {
                mPos.Clear();
                points = null;
            }
            
        }
        else if(Input.GetMouseButtonUp(0))
        {

            points = MakeSmoothCurve(mPos, 3.0f);
            line = new GameObject("Line").AddComponent<LineRenderer>();
            line.SetVertexCount(points.Length);
            line.SetWidth(1f, 1f);
            line.SetColors(Color.black, Color.black);
            line.useWorldSpace = true;
            int counter = 0;
            foreach(var i in points)
            {
                line.SetPosition(counter, i);
                ++counter;
            }
        }
        else if(Input.GetMouseButton(0))
        {
            //mPos.Add(Input.mousePosition);
            //mPos.Add(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)));
            mPos.Add(Camera.current.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)));
        }
	}



    public static Vector3[] MakeSmoothCurve(List<Vector3> arrayToCurve, float smoothness)
    {
        List<Vector3> points;
        List<Vector3> curvedPoints;
        int pointsLength = 0;
        int curvedLength = 0;

        if (smoothness < 1.0f) smoothness = 1.0f;

        pointsLength = arrayToCurve.Count;

        curvedLength = (pointsLength * Mathf.RoundToInt(smoothness)) - 1;
        curvedPoints = new List<Vector3>(curvedLength);

        float t = 0.0f;
        for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
        {
            t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

            points = arrayToCurve;

            for (int j = pointsLength - 1; j > 0; j--)
            {
                for (int i = 0; i < j; i++)
                {
                    points[i] = (1 - t) * points[i] + t * points[i + 1];
                }
            }

            curvedPoints.Add(points[0]);
        }

        return (curvedPoints.ToArray());
    }
}


//private void createLine()
//{
//    line = new GameObject("Line").AddComponent<LineRenderer>();
//    line.material = new Material(Shader.Find("Diffuse"));
//    line.SetVertexCount(2);
//    line.SetWidth(0.1f, 0.1f);
//    line.SetColors(Color.black, Color.black);
//    line.useWorldSpace = true;
//}

//private void addColliderToLine()
//{
//    BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider>();
//    col.transform.parent = line.transform;
//    float lineLength = Vector3.Distance(startPos, endPos);
//    col.size = new Vector3(lineLength, 0.1f, 1f);
//    Vector3 midPoint = (startPos + endPos) / 2;
//    col.transform.position = midPoint;
//    float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
//    if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
//    {
//        angle *= -1;

//    }
//    if (angle != angle)
//    {
//        Destroy(line.gameObject);
//    }
//    else
//    {
//        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
//        col.transform.Rotate(0, 0, angle);
//    }
//}
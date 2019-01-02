using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DrawLineVR : MonoBehaviour {

    private Hand hand;

    private LineRenderer line;
    private Vector3 mousePos;
    private Vector3 startPos;
    private Vector3 endPos;
    private List<Vector3> mPos = new List<Vector3>();
    private Vector3[] points;
    private GameObject parent;

    // Use this for initialization
    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
        points = null;
        parent = GameObject.Find("paper");
    }

    public Vector2 getTrackPadPos()
    {
        SteamVR_Action_Vector2 trackpadPos = SteamVR_Input._default.inActions.touchPos;
        return trackpadPos.GetAxis(hand.handType);
    }

    public bool getPinch()
    {
        return SteamVR_Input._default.inActions.GrabPinch.GetState(hand.handType);
    }

    public bool getPinchDown()
    {
        return SteamVR_Input._default.inActions.GrabPinch.GetStateDown(hand.handType);
    }

    public bool getPinchUp()
    {
        return SteamVR_Input._default.inActions.GrabPinch.GetStateUp(hand.handType);
    }

    public bool getGrip()
    {
        return SteamVR_Input._default.inActions.GrabGrip.GetState(hand.handType);
    }

    public bool getGrip_Down()
    {
        return SteamVR_Input._default.inActions.GrabGrip.GetStateDown(hand.handType);
    }

    public bool getGrip_Up()
    {
        return SteamVR_Input._default.inActions.GrabGrip.GetStateUp(hand.handType);
    }

    public bool getMenu()
    {
        return SteamVR_Input._default.inActions.MenuButton.GetState(hand.handType);
    }

    public bool getMenu_Down()
    {
        return SteamVR_Input._default.inActions.MenuButton.GetStateDown(hand.handType);
    }

    public bool getMenu_Up()
    {
        return SteamVR_Input._default.inActions.MenuButton.GetStateUp(hand.handType);
    }

    public bool getTouchPad()
    {
        return SteamVR_Input._default.inActions.Teleport.GetState(hand.handType);
    }

    public bool getTouchPad_Down()
    {
        return SteamVR_Input._default.inActions.Teleport.GetStateDown(hand.handType);
    }

    public bool getTouchPad_Up()
    {
        return SteamVR_Input._default.inActions.Teleport.GetStateUp(hand.handType);
    }

    public Vector3 getControllerPosition()
    {
        SteamVR_Action_Pose[] poseActions = SteamVR_Input._default.poseActions;
        if (poseActions.Length > 0)
        {
            return poseActions[0].GetLocalPosition(hand.handType);
        }
        return new Vector3(0, 0, 0);
    }

    public Quaternion getControllerRotation()
    {
        SteamVR_Action_Pose[] poseActions = SteamVR_Input._default.poseActions;
        if (poseActions.Length > 0)
        {
            return poseActions[0].GetLocalRotation(hand.handType);
        }
        return Quaternion.identity;
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

    // Update is called once per frame
    void Update () {
        if (getPinchDown())
        {
            if (mPos != null)
            {
                mPos.Clear();
                points = null;
            }

            GameObject[] tmp = GameObject.FindGameObjectsWithTag("line");
            if (tmp != null)
                foreach (GameObject a in tmp)
                    Destroy(a);
            startPos = getControllerPosition();
            
        }
        else if (getPinchUp())
        {

            points = MakeSmoothCurve(mPos, 3.0f);
            line = new GameObject("Line").AddComponent<LineRenderer>();
            line.tag = "line";
            line.transform.parent = parent.transform;
            line.SetVertexCount(points.Length);
            line.SetWidth(0.01f, 0.01f);
            line.SetColors(Color.black, Color.black);
            line.useWorldSpace = true;
            int counter = 0;
            foreach (var i in points)
            {
                line.SetPosition(counter, i);
                ++counter;
            }
        }
        else if (getPinch())
        {
            //mPos.Add(Camera.current.ScreenToWorldPoint(new Vector3(getControllerPosition().x,getControllerPosition().y,-getControllerPosition().z)));
            //mPos.Add(new Vector3(getControllerPosition().x,getControllerPosition().y,startPos.z));
            mPos.Add(getControllerPosition());
        }
    }
}

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
    private SpriteRenderer spr;
    public int cameraRo;
    private GameObject viveCamera;


    public Transform objectImageCamera;
    public GameObject objectToSnapshot;
    ObjectImageSnapshot snapshot;
    Texture2D texture;


    // Use this for initialization
    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
        points = null;
        parent = GameObject.Find("paper");
        
        viveCamera = GameObject.Find("Camera");
        spr = parent.GetComponent<SpriteRenderer>();
        spr.enabled = false;

        snapshot = objectImageCamera.GetComponent<ObjectImageSnapshot>();
    }

    //컨프롤러의 각종 상태를 리턴해주는 함수
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

    //베지에 곡선 함수를 이용해서 매끄러운 곡선을 출력하는 함수(렉이 너무 심해서 안씀+생각보다 안 매끄러움)
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

            startPos = getControllerPosition();

            if (viveCamera.transform.eulerAngles.y < 135 && viveCamera.transform.eulerAngles.y >= 45)
                cameraRo = 2;
            else if (viveCamera.transform.eulerAngles.y < 225 && viveCamera.transform.eulerAngles.y >= 135)
                cameraRo = 1;
            else if (viveCamera.transform.eulerAngles.y < 315 && viveCamera.transform.eulerAngles.y >= 225)
                cameraRo = 2;
            else
                cameraRo = 1;

            parent.transform.position = new Vector3(startPos.x, startPos.y - 0.08f , startPos.z);
            spr.enabled = true;

            if (cameraRo == 1)
                parent.transform.rotation= Quaternion.Euler(new Vector3(0, 0, 0));
            else
                parent.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (getPinchUp())
        {
            spr.enabled = false;
            //사물 출력

            objectToSnapshot = GameObject.Find("Line");
            texture = snapshot.TakeObjectSnapshot(objectToSnapshot);
        }
        else if (getPinch())
        {
            //mPos.Add(Camera.current.ScreenToWorldPoint(new Vector3(getControllerPosition().x,getControllerPosition().y,-getControllerPosition().z)));
            //mPos.Add(getControllerPosition());

            GameObject[] tmp = GameObject.FindGameObjectsWithTag("line");
            if (tmp != null)
                foreach (GameObject a in tmp)
                    Destroy(a);

            if (cameraRo == 1)
                mPos.Add(new Vector3(getControllerPosition().x, getControllerPosition().y, startPos.z));
            else
                mPos.Add(new Vector3(startPos.x, getControllerPosition().y, getControllerPosition().z));

            points = mPos.ToArray();
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
    }
}

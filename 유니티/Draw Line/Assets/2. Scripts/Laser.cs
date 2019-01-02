using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Laser : MonoBehaviour {

    private Hand hand;

    private SteamVR_TrackedObject trackedObj;
    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

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

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, 5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }

    // Use this for initialization
    void Start () {
        hand = gameObject.GetComponent<Hand>();
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }
	
	// Update is called once per frame
	void Update () {
		if(getPinch())
        {
            RaycastHit hit;
            if(Physics.Raycast(trackedObj.transform.position,transform.forward,out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
            }
        }
        else
        {
            laser.SetActive(false);
        }
	}
}

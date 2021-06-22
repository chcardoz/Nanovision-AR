using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using UnityEngine.XR.ARSubsystems;

public class FollowPlayer : MonoBehaviour
{
        
	[SerializeField] List<GameObject> followingObjects = new List<GameObject>();
    [SerializeField] GameObject placementIndicator;
    [SerializeField] float offset;


    //Raycast stuff
    private ARRaycastManager aRRaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private bool objectPlacedOnce = false;
    private bool placementPoseIsValid = false;
    private Pose placementPose;

	void Start()
	{
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (!objectPlacedOnce)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
                objectPlacedOnce = true;
                placementIndicator.SetActive(false);
            }
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        aRRaycastManager.Raycast(screenCenter, s_Hits, TrackableType.Planes);

        placementPoseIsValid = s_Hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = s_Hits[0].pose;
            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void PlaceObject()
    {
        int numObjects = followingObjects.Count;
        for(int i =0; i<numObjects; i++)
        {
            Vector3 position = new Vector3(placementPose.position.x + offset * i, placementPose.position.y, placementPose.position.z);
            Instantiate(followingObjects[i], position, placementPose.rotation);
        }
    }
}

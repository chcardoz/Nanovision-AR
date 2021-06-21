using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;


public class TapToPlaceObject : MonoBehaviour

{

	

	int counter = 0;
	private ARRaycastManager aRRaycastManager;  //handles raycasts
	private Pose placementPose;
	private bool placementPoseIsValid = false;
	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

	[SerializeField]
	GameObject placementIndicator;

	GameObject placedObjects;

	[SerializeField]
	GameObject objectToPlace;
	void Start()
	{
		placedObjects = null;
		aRRaycastManager = FindObjectOfType<ARRaycastManager>();
	}

	void Update()
	{
	//	Boolean keepGoing = true;
		//int counter = 0;
		UpdatePlacementPose();
		UpdatePlacementIndicator();
	//	while(keepGoing) {
			if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
					
				PlaceObject();
			//	keepGoing = false;	
				//counter++;
				
				//}
		}

	}
	public void PlaceObject() {
		//int counter = 0;
		if(isPlaced()) {
			Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
			counter++;
		}	
		//}
		
	}
	private bool isPlaced() {
		if (counter == 0) {
			return true;
		} else {
			return false;
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
		var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
		aRRaycastManager.Raycast(screenCenter, s_Hits, TrackableType.Planes);

		placementPoseIsValid = s_Hits.Count > 0;
		if (placementPoseIsValid)
		{
			placementPose = s_Hits[0].pose;
			var cameraForward = Camera.current.transform.forward;
			var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
			placementPose.rotation = Quaternion.LookRotation(cameraBearing);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class CutPaper : MonoBehaviour
{
	[SerializeField] GameObject placementIndicator;
	[SerializeField] GameObject objectToPlace;
	[SerializeField] float shrinkFactor;
	private GameObject spawnedObject;

	private ARRaycastManager aRRaycastManager;  
	private Pose placementPose;
	private bool placementPoseIsValid = false;
	public static bool objectIsPlaced = false;
	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
	public static int numShrinks;

	void Start()
	{
		numShrinks = 0;
		aRRaycastManager = FindObjectOfType<ARRaycastManager>();
	}

	void Update()
	{
        if (!objectIsPlaced)
        {
			UpdatePlacementPose();
			UpdatePlacementIndicator();
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


	public void Shrink()
    {
		if (placementPoseIsValid)
		{
			spawnedObject.transform.localScale *= shrinkFactor;
			numShrinks += 1;
		}
	}

	public void SpawnObject()
    {
		if(placementPoseIsValid)
        {
			spawnedObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
			placementIndicator.SetActive(false);
			objectIsPlaced = true;
        }
    }

}
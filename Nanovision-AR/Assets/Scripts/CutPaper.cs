using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class CutPaper : MonoBehaviour
{
	// To handles raycasts
	private ARRaycastManager aRRaycastManager;  
	private Pose placementPose;
	private bool placementPoseIsValid = false;
	public static bool objectIsPlaced = false;
	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

	[SerializeField] GameObject placementIndicator;
	[SerializeField]  List<GameObject> objectsToPlace = new List<GameObject>();
	private int rangeObjectList;
	private GameObject spawnedObject;

	public static int numCuts;
	private int item_idx;
	public int Item_idx
    {
		get { return item_idx;  }
		set
        {
			item_idx = value;
			if(item_idx < rangeObjectList)
            {
				ReplaceObject();
			}
        }
    }

	void Start()
	{
		Item_idx = 0;
		numCuts = 0;
		rangeObjectList = objectsToPlace.Count;
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


	public void Cut()
    {
		if (placementPoseIsValid)
		{
			placementIndicator.SetActive(false);
			Item_idx += 1;
			numCuts += 1;
		}
	}

	public void ReplaceObject()
    {
		if (Item_idx > 1)
		{
			Destroy(spawnedObject);
		}
		spawnedObject = Instantiate(objectsToPlace[Item_idx-1], placementPose.position, placementPose.rotation);

	}
}
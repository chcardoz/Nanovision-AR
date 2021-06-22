using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class CutPaper : MonoBehaviour
{

	private ARRaycastManager aRRaycastManager;  //handles raycasts
	private Pose placementPose;
	private bool placementPoseIsValid = false;
	private bool objectIsPlaced = false;
	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

	[SerializeField] GameObject placementIndicator;
	[SerializeField]  List<GameObject> objectsToPlace = new List<GameObject>();

	private int item_idx = 0;
	public int Item_idx
    {
		get { return item_idx;  }
		set
        {
			item_idx = value;
			ReplaceObject();
        }
    }

	void Start()
	{
		aRRaycastManager = FindObjectOfType<ARRaycastManager>();
	}

	void Update()
	{
        if (!objectIsPlaced)
        {
			UpdatePlacementPose();
			UpdatePlacementIndicator();
		}

		if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			objectIsPlaced = true;
			placementIndicator.SetActive(false);
			Item_idx += 1;
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

	public void ReplaceObject()
    {
		if(Item_idx > 0)
        {
			Destroy(objectsToPlace[Item_idx-1]);
			Instantiate(objectsToPlace[Item_idx], placementPose.position, placementPose.rotation);
        }
    }
}
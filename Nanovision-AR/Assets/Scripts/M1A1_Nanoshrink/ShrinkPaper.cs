using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ShrinkPaper : MonoBehaviour
{
	[SerializeField] GameObject placementIndicator;
	[SerializeField] GameObject objectToPlace;
	[SerializeField] float shrinkFactor;
	private GameObject spawnedObject;

	/// <summary>
    /// Variables required to make AR work
    /// </summary>
	private ARRaycastManager aRRaycastManager;  
	private Pose placementPose;
	private bool placementPoseIsValid;
	public static bool objectIsPlaced;
	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
	public static float numShrinks;

	void Start()
	{
		numShrinks = 0;
		objectIsPlaced = false;
		placementPoseIsValid = false; 
		aRRaycastManager = FindObjectOfType<ARRaycastManager>();
		Destroy(spawnedObject);
	}

	/// <summary>
    /// If the object is not spawned, updates the placement pose and indicator every frame.
    /// </summary>
	void Update()
	{
        if (!objectIsPlaced)
        {
			UpdatePlacementPose();
			UpdatePlacementIndicator();
		}
	}

	/// <summary>
    /// Updates the placement indicators's position and rotation if the a placement pose is valid
    /// </summary>
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

	/// <summary>
    /// Updates the placement pose by casting rays from the center of screen. When raycasts hit a valid plane in AR, sets placement pose to true and sets placement position and rotation
    /// to the position and rotation of the plane. 
    /// </summary>
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

	/// <summary>
    /// If the placement pose is valid, reduces the scale of the spawned object by the scale factor
    /// </summary>
	public void Shrink()
    {
        if (placementPoseIsValid)
        {
            spawnedObject.transform.localScale *= shrinkFactor;
            numShrinks += 1f;
        }
    }

	/// <summary>
    /// If the placement pose is valid, spawns the object in the placement position and rotation. 
    /// </summary>
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
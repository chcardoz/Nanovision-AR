using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FollowPlayer : MonoBehaviour
{
        
	[SerializeField] List<GameObject> followingObjects = new List<GameObject>();
    [SerializeField] GameObject placementIndicator;
    [SerializeField] float offset;
    public LeanTweenType easeType;

    private List<GameObject> spawnedObjects = new List<GameObject>();
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

    public void PlaceObject()
    {
        int numObjects = followingObjects.Count;
        if (placementPoseIsValid)
        {
            objectPlacedOnce = true;
            placementIndicator.SetActive(false);
            for (int i = 0; i < numObjects; i++)
            {
                Vector3 position = new Vector3(placementPose.position.x + offset * i, placementPose.position.y, placementPose.position.z);
                GameObject newGO = (GameObject)Instantiate(followingObjects[i], position, placementPose.rotation);
                spawnedObjects.Add(newGO);
            }
        }
    }


    public void ScaleUpObjects()
    {
        int numObjects = spawnedObjects.Count;
        for(int i=0; i<numObjects; i++)
        {
            if(i < 3)
            {
                Destroy(spawnedObjects[i]);
            }
            else
            {
                Transform objTransform = spawnedObjects[i].transform;
                Vector3 newScale = objTransform.localScale * 1000f;
                LeanTween.scale(spawnedObjects[i], newScale, 3f).setEase(easeType);
            }
        }
    }
   
}


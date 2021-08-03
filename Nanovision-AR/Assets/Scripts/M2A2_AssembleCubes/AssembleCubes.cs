using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AssembleCubes : MonoBehaviour
{
    [SerializeField] GameObject placementIndicator;
    [SerializeField] GameObject largeCubePrefab;
    [SerializeField] GameObject smallCubePrebab;
    [SerializeField] GameObject smallestCubePrefab;

    private ARRaycastManager aRRaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private bool objectPlacedOnce = false;
    private bool placementPoseIsValid = false;
    private Pose placementPose;

    private GameObject largerCube;
    private List<GameObject> smallerCubes = new List<GameObject>();
    private List<GameObject> smallestCubes = new List<GameObject>();

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

    public void PlaceLargeCube()
    {
        if (placementPoseIsValid)
        {
            largerCube = Instantiate(largeCubePrefab, placementPose.position, placementPose.rotation);
        }
    }

    public void Place8Cubes()
    {
        float offset = 2;
        int index;
        int gridWidth = 4;
        int gridLength = 2;
        if (placementPoseIsValid)
        {
            for (int i = 0; i < gridLength; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    index = j + i * gridWidth;
                    Color randomColor = Random.ColorHSV(0f, .25f, .4f, 1f);
                    Vector3 placementPosition = new Vector3(placementPose.position.x + offset * i + 4f, placementPose.position.y, placementPose.position.z + offset * j);
                    smallerCubes.Add(Instantiate(smallCubePrebab, placementPosition, placementPose.rotation));
                    MeshRenderer cubeMeshRenderer = smallerCubes[index].GetComponent<MeshRenderer>();
                    cubeMeshRenderer.material.color = randomColor;
                }

            }
        }
    }

    public void Place64Cubes()
    {
        float offset = 2;
        int index;
        int gridWidth = 8;
        int gridLength = 8;
        if (placementPoseIsValid)
        {
            for (int i = 0; i < gridLength; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    index = j + i * gridWidth;
                    Color randomColor = Random.ColorHSV(0f, .25f, .4f, 1f);
                    Vector3 placementPosition = new Vector3(placementPose.position.x + offset * i + 3f, placementPose.position.y, placementPose.position.z + offset * j);
                    smallestCubes.Add(Instantiate(smallestCubePrefab, placementPosition, placementPose.rotation));
                    MeshRenderer cubeMeshRenderer = smallestCubes[index].GetComponent<MeshRenderer>();
                    cubeMeshRenderer.material.color = randomColor;
                }

            }
        }
    }

    public void Assemble8Cubes()
    {
        int index;
        float offset = 1.5f;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    index = k + j * 2 + i * 4;
                    Vector3 newposition = new Vector3(placementPose.position.x + offset * i + 4f, placementPose.position.y + offset * j, placementPose.position.z + offset * k);
                    LeanTween.move(smallerCubes[index], newposition, 3f).setEase(LeanTweenType.easeOutQuint);
                }
            }
        }
    }

    public void Assemble64Cubes()
    {
        int index;
        float offset = .75f;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    index = k + j * 4 + i * 16;
                    Vector3 newposition = new Vector3(placementPose.position.x + offset * i + 3f, placementPose.position.y + offset * j, placementPose.position.z + offset * k);
                    LeanTween.move(smallestCubes[index], newposition, 3f).setEase(LeanTweenType.easeOutQuint);
                }
            }
        }
    }

    public void Destroy8Cubes()
    {
        for(int i=0; i<8; i++)
        {
            Destroy(smallerCubes[i]);
        }
    }

    public void Destroy64Cubes()
    {
        for(int i=0; i<64; i++)
        {
            Destroy(smallestCubes[i]);
        }
    }
}

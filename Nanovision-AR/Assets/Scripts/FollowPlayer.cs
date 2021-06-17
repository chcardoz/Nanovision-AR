using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class FollowPlayer : MonoBehaviour
{
	[SerializeField] List<GameObject> followingObjects;
	[SerializeField] float circleRadius;
	// Start is called before the first frame update
	void Start()
	{
		int numObjects = followingObjects.Count;
		Vector3 cameraPosition = Camera.main.transform.position;
		float angle = 360f / (float)numObjects;
		for (int i = 0; i < numObjects; i++)
		{
			Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
			Vector3 direction = rotation * Vector3.forward;
			Vector3 position = cameraPosition + (direction * circleRadius);
			Instantiate(followingObjects[i], position, rotation);

		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}

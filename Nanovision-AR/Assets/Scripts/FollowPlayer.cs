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

	private List<Vector3> outvectors;
	private int numObjects;
	private Vector3 cameraPosition;
	void Start()
	{
		numObjects = followingObjects.Count;
		cameraPosition = Camera.main.transform.position;
		float angle = 360f / (float)numObjects;
		for (int i = 0; i < numObjects; i++)
		{
			Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
			Vector3 startOrientation = Vector3.forward;
			Vector3 direction = rotation * startOrientation;
			outvectors[i] = direction * circleRadius;
			Vector3 position = cameraPosition + outvectors[i];
			Instantiate(followingObjects[i], position, rotation);
		}
	}

	void Update()
	{
		cameraPosition = Camera.main.transform.position;
		for (int i = 0; i < numObjects; i++)
		{
			followingObjects[i].transform.position = cameraPosition + outvectors[i];
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToTarget : MonoBehaviour {

	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	public CameraGestureRotate cameraGestureRotate;
	public CameraGestureScale cameraGestureScale;
	private Transform target;

	// Update is called once per frame
	void LateUpdate () {
		if (target != null) {
			SmoothFollow ();
		}
	}

	private void SmoothFollow(){
			// Early out if we don't have a target
			if (!target) return;

			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;

			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);

			// Always look at the target
			transform.LookAt(target);
	}

	public void SetTarget(Transform target){
		this.target = target;
		cameraGestureRotate.enabled = false;
		cameraGestureScale.enabled = false;
	}
	public void RemoveTarget(){
		this.target = null;
		cameraGestureRotate.enabled = true;
		cameraGestureScale.enabled = true;
	}
}

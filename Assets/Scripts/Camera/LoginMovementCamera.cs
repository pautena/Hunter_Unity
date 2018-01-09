using UnityEngine;
using System.Collections;

public class LoginMovementCamera : MonoBehaviour {

	public float startCameraXVelocity = 10.0f;
	public float minCameraXMovement = -550.0f;
	public float maxCameraXMovement = 550.0f;

	private float cameraXVelocity;
	private Transform cameraTransform;

	// Use this for initialization
	void Start () {
		cameraXVelocity = startCameraXVelocity;
		cameraTransform = GetComponent<Transform> ();

	}

	// Update is called once per frame
	void Update () {
		cameraTransform.Translate (new Vector3(cameraXVelocity,0.0f,0.0f) * Time.deltaTime);

		float x = cameraTransform.position.x;
		if (x > maxCameraXMovement || x <minCameraXMovement)
			cameraXVelocity *= -1;
	}
}
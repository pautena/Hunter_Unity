using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class CameraGestures : MonoBehaviour {
	
	private PanGestureRecognizer panGesture;
	private Vector3 cameraAnimationTargetPosition;

	public Transform target;
	public float rotateVelocity=10f;
	public float upVelocity=10f;
	public float minAngle = 10f;
	public float maxAngle = 80f;

	private float deltaX;
	private float deltaY;


	private IEnumerator AnimationCoRoutine()
	{
		Vector3 start = Camera.main.transform.position;

		// animate over 1/2 second
		for (float accumTime = Time.deltaTime; accumTime <= 0.5f; accumTime += Time.deltaTime)
		{
			Camera.main.transform.position = Vector3.Lerp(start, cameraAnimationTargetPosition, accumTime / 0.5f);
			yield return null;
		}
	}

	private void Start()
	{
		deltaX = 0.0f;
		deltaY = 0.0f;
		panGesture = new PanGestureRecognizer();
		panGesture.StateUpdated += PanGesture_Updated;
		FingersScript.Instance.AddGesture(panGesture);

	}

	private void Update(){
		if (deltaX != 0f || deltaY != 0f) {
			transform.LookAt (target);
			Rotate ();
			Up ();
			Zoom ();
		}
	}
		
	private void Rotate(){
		transform.RotateAround (target.position,Vector3.up,deltaX * Time.deltaTime * rotateVelocity);
	}

	private void Up(){
		Quaternion originalRotation = transform.rotation;
		Vector3 originalPosition = transform.position;

		transform.RotateAround (target.position,Vector3.right,deltaY * Time.deltaTime * upVelocity);

		Debug.Log (transform.rotation.x);

		if (transform.rotation.x < Mathf.Deg2Rad * minAngle) {
			transform.position = originalPosition;
			transform.rotation = originalRotation;
		} else if (transform.rotation.x > Mathf.Deg2Rad * maxAngle) {
			transform.position = originalPosition;
			transform.rotation = originalRotation;
		}
	}

	private void Zoom(){
	}

	private void PanGesture_Updated(GestureRecognizer gesture)
	{
		if (panGesture.State == GestureRecognizerState.Executing)
		{
			StopAllCoroutines();

			deltaX=panGesture.DeltaX;
			deltaY = panGesture.DeltaY;
			RotateCamera (panGesture.DeltaX);
			ZoomCamera (panGesture.DeltaY);
		}
		else if (panGesture.State == GestureRecognizerState.Ended){
			deltaX = 0f;
			deltaY = 0f;
		}
	}

	private void RotateCamera(float delta){

	}

	private void ZoomCamera(float delta){

	}

	public void OrthographicCameraOptionChanged(bool orthographic)
	{
		Camera.main.orthographic = orthographic;
	}
}

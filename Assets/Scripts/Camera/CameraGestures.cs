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

	public float minimumY = -60F;
	public float maximumY = 60F;

	private float rotationX = 0F;
	private float rotationY = 0F;
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
		float angle = deltaX * Time.deltaTime * rotateVelocity;
		float newRotation = rotationX + angle;
		transform.RotateAround (target.position,Vector3.up,angle);
		rotationX = newRotation;
	}

	//Complete example with max and min angles
	private void Up(){
		float delta = deltaY * Time.deltaTime * rotateVelocity;
		float newAngle = Camera.main.transform.eulerAngles.x - delta;

		if(Mathf.Ceil(newAngle) < maximumY  && Mathf.Floor(newAngle) > minimumY){
			Camera.main.transform.RotateAround(target.position, Vector3.right, -delta );
		}else{
			float clampedVal = 0.0f;

			if(Mathf.Ceil(newAngle) > maximumY){
				clampedVal =(maximumY - 0.001f) - Camera.main.transform.eulerAngles.x;
				Camera.main.transform.RotateAround(target.position, Vector3.right, clampedVal * Mathf.Deg2Rad );

			}
			else if(Mathf.Floor(newAngle) < minimumY){
				clampedVal = (minimumY+0.001f) - Camera.main.transform.eulerAngles.x;
				Camera.main.transform.RotateAround(target.position, Vector3.right, clampedVal * Mathf.Deg2Rad  );
			}
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

	public static float ClampAngle (float angle, float min, float max){
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}

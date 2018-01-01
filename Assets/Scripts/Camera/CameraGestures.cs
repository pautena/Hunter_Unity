using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class CameraGestures : MonoBehaviour {
	
	public Transform Target;
	public float distance = 10.0f;
	public float minDistance=10.0f;
	public float maxDistance=30.0f;

	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float scaleSpeed=0.7f;

	public float yMinLimit = -20.0f;
	public float yMaxLimit = 80.0f;


	private float x;
	private float y;
	private float scale;
	private PanGestureRecognizer panGesture;
	private ScaleGestureRecognizer scaleGesture;

	void Awake(){
		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;
		scale=1.0f;
	}

	void Start(){

		scaleGesture = new ScaleGestureRecognizer ();
		scaleGesture.StateUpdated += Gesture_Updated;
		FingersScript.Instance.AddGesture(scaleGesture);

		panGesture = new PanGestureRecognizer();
		panGesture.StateUpdated += PanGesture_Updated;
		FingersScript.Instance.AddGesture(panGesture);

		// the scale and pan can happen together
		scaleGesture.AllowSimultaneousExecution(panGesture);
	}

	void LateUpdate(){
		if(Target != null){
			Zoom ();
			Rotation ();
		}
	}


	private void Rotation(){
		y = ClampAngle(y, yMinLimit, yMaxLimit);

		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -distance)) + Target.position;
		transform.rotation = rotation;
		transform.position = position;
	}

	private void Zoom(){
		if (scale != 1.0f) {
			float delta = (distance * scale) - distance;
			distance += delta * scaleSpeed;

			if (distance < minDistance) {
				distance = minDistance;
			} else if (distance > maxDistance) {
				distance = maxDistance;
			}
		}
	}

	private float ClampAngle(float angle, float min, float max){
		if(angle < -360){
			angle += 360;
		}else if(angle > 360){
			angle -= 360;
		}
		return Mathf.Clamp (angle, min, max);
	}

	private void PanGesture_Updated(GestureRecognizer gesture){
		if (panGesture.State == GestureRecognizerState.Executing){
			StopAllCoroutines();

			x+=panGesture.DeltaX;
			y+= panGesture.DeltaY;
		}
	}

	private void Gesture_Updated(GestureRecognizer gesture){

		if (scaleGesture.State == GestureRecognizerState.Executing) {
			scale = 1.0f + (1.0f - scaleGesture.ScaleMultiplier);
		} else {
			scale = 1.0f;
		}

	}

}

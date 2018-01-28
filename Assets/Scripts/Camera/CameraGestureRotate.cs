using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGestureRotate : MonoBehaviour {

	public Transform Target;
	public float distance = 10.0f;
	public float minDistance=10.0f;
	public float maxDistance=30.0f;

	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20.0f;
	public float yMaxLimit = 80.0f;


	private float xAngle;
	private float yAngle;
	private float xAngTemp = 0.0f;
	private float yAngTemp = 0.0f;
	private Vector3 firstpoint;
	private Vector3 secondpoint;

	private float scale;

	void Awake(){
		Vector3 angles = transform.rotation.eulerAngles;
		xAngle = angles.y;
		yAngle = angles.x;
	}

	void Update() {
		//Check count touches
		if(Input.touchCount ==1) {
			UpdateRotate ();
		}
	}

	private void UpdateRotate(){
		//Touch began, save position
		if(Input.GetTouch(0).phase == TouchPhase.Began) {
			firstpoint = Input.GetTouch(0).position;
			xAngTemp = xAngle;
			yAngTemp = yAngle;
		}
		//Move finger by screen
		if(Input.GetTouch(0).phase==TouchPhase.Moved) {
			secondpoint = Input.GetTouch(0).position;
			//Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
			xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
			yAngle = yAngTemp + (secondpoint.y - firstpoint.y) *90.0f / Screen.height;
			//Rotate camera
			//this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
		}
	}

	void LateUpdate(){
		if(Target != null){
			Rotation ();
		}
	}
		
	private void Rotation(){
		yAngle = ClampAngle(yAngle, yMinLimit, yMaxLimit);

		Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0);
		Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -distance)) + Target.position;
		transform.rotation = rotation;
		transform.position = position;
	}

	private float ClampAngle(float angle, float min, float max){
		if(angle < -360){
			angle += 360;
		}else if(angle > 360){
			angle -= 360;
		}
		return Mathf.Clamp (angle, min, max);
	}

	void Updte(){


	}
}

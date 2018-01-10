using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCircleMovement : MonoBehaviour {

	public GroundCircle groundCircle;
	public float minRadius = 0f;
	public float maxRadius = 2f;
	public float velolcity = 0.2f;


	private float radius;

	// Use this for initialization
	void Start () {
		radius = minRadius;
		
	}
	
	// Update is called once per frame
	void Update () {
		groundCircle.xradius = radius;
		groundCircle.yradius = radius;
		groundCircle.CreatePoints ();
		IncRadius ();
	}

	private void IncRadius(){
		radius += velolcity * Time.deltaTime;
		if (radius > maxRadius) {
			radius = minRadius;
		}
	}
}

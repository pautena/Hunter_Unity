using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
	public float speed =50f;
	public Vector3 direction = Vector3.left;


	void FixedUpdate ()
	{
		//transform.RotateAround(direction, speed * Time.deltaTime);
		transform.RotateAround(transform.position, direction, Time.deltaTime * speed);
	}
}
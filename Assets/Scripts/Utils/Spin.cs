using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
	public float speed =50f;
	public Vector3 direction = Vector3.left;


	void Update ()
	{
		transform.Rotate(direction, speed * Time.deltaTime);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Poi{
	public class TouchPoi : MonoBehaviour {

		void Update () {
			#if UNITY_EDITOR
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			#else
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
			#endif
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					if(hit.collider.tag=="Poi"){
						PoiHelper poi = hit.collider.GetComponent<PoiHelper>();
						poi.OnClick();
					}
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mapbox.Unity.MeshGeneration.Interfaces;
using NemApi.Models;

namespace Poi{
	public class PoiHelper : MonoBehaviour,IFeaturePropertySettable {

		public bool poiEnabled=true;
		public GameObject enabledGameObject;

		private Collider poiCollider;
		public string id = "-1";
		public Canvas poiUI;

		// Use this for initialization
		void Start () {
			poiCollider = GetComponent<Collider> ();		
		}

		// Update is called once per frame
		void Update () {

			if (poiEnabled) {
				Enable ();
			} else {
				Disable ();
			}
		}

		public void Enable(Mosaic mosaic){
			//TODO: Check if can enable with this mosaic;
			SetEnabled(true);
		}


		public void Enable(){
			enabledGameObject.SetActive (true);
		}

		public void Disable(){
			enabledGameObject.SetActive (false);
		}

		private void SetEnabled(bool poiEnabled){
			this.poiEnabled = poiEnabled;
			enabledGameObject.SetActive (poiEnabled);
		}

		public void OnClick(){
			if (/*PlayerInsideCollider () &&*/ poiEnabled) {
				ShowPrize ();
			}
		}

		private void ShowPrize(){
			Camera mainCamera = Camera.main;
			CameraToTarget cameraToTarget = mainCamera.GetComponent<CameraToTarget> ();
			if (cameraToTarget != null) {
				ShowPoiUI ();
				cameraToTarget.SetTarget (transform);
			}
		}

		public void ShowPoiUI(){
			poiUI.enabled = true;
		}

		public void HidePoiUI(){
			poiUI.enabled = false;
		}

		private bool PlayerInsideCollider(){
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			return player != null && poiCollider.bounds.Contains (player.transform.position);
		}

		public void Set(Dictionary<string, object> props){
			string id =  props ["id"].ToString();

			this.SetId (id);

		}

		private void SetId(string id){
			this.id = id;
			new PoiManager ().SetPoiName (gameObject,id);
		}

	}

}
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
			Debug.Log ("try to enable " + id);
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
			if (PlayerInsideCollider () && enabled) {
				print("player is inside collider");
				GameObject prizePanel = GameObject.FindGameObjectWithTag ("PanelPrize");

				if (prizePanel != null) {
					PrizePanelManager prizePanelManager = prizePanel.GetComponent<PrizePanelManager> ();
					prizePanelManager.OnCanWinPrize ();
				}
			}
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
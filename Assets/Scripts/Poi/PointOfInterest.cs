using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mapbox.Unity.MeshGeneration.Interfaces;

public class PointOfInterest : MonoBehaviour,IFeaturePropertySettable {

	public bool enabled=true;
	public GameObject enabledGameObject;

	private Collider collider;
	public string id = "-1";

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider> ();		
	}
	
	// Update is called once per frame
	void Update () {

		//TODO: Remove this line when connected to NEM
		if (enabled) {
			Enable ();
		} else {
			Disable ();
		}		
	}


	public void Enable(){
		SetEnabled (true);
	}

	public void Disable(){
		SetEnabled (false);
	}

	private void SetEnabled(bool enabled){
		this.enabled = enabled;
		enabledGameObject.SetActive (enabled);
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
		return player != null && collider.bounds.Contains (player.transform.position);
	}

	public void Set(Dictionary<string, object> props){
		this.id =  props ["id"] as string;
	}
}

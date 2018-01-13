using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NemApi{
	public class LoadPois : MonoBehaviour {

		public NemApi nemApi;

		// Use this for initialization
		void Start () {
			RequestPois ();

		}

		// Update is called once per frame
		void Update () {

		}

		private void RequestPois(){

			nemApi.GetMosaicDefinition ();

		}
	}
}

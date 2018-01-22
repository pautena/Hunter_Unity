using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using NemApi.Utils;
using System.Security;
using NemApi.Models;
using NemApi.CryptoFunctions;

namespace NemApi{
	public class TestEncription : MonoBehaviour {

		public string initialPrivateKey="59e309f7db4cc17fcb1d0ddd85f331bf18be179cbb656eed8e2d69b98b5fb6ba";
		public string expectedPublicKey="40d8ed4aa306c94b6ec6c7036a7b5dca2d1b7fa5cfeec222dd0dac44ccbabfcf";
		public string expectedAddress="TBWHKDPRWQYD5JFVATPOOBJZQDFXZ3LDHYYBJHF3";

		// Use this for initialization
		void Start () {
			//Good

			Debug.Log ("initialPrivateKey: " + initialPrivateKey);
			PrivateKey privateKey = new PrivateKey (initialPrivateKey);
			PublicKey publicKey = CalculatePublicKey (privateKey);
			string address = CalculateAddress (publicKey);

		}

		// Update is called once per frame
		void Update () {

		}


		private PublicKey CalculatePublicKey(PrivateKey privateKey){

			PublicKey publicKey = new PublicKey (privateKey);

			if (publicKey.Raw == expectedPublicKey) {
				Debug.Log ("public key generated successfully");
			} else {
				Debug.LogError ("error generating public key. generatedPublicKey: "+publicKey.Raw);
			}

			return publicKey;

		}

		private string CalculateAddress(PublicKey publicKey){

			string generatedAddress = AddressEncoding.ToAddress (NetworkVersion.TEST_NET, publicKey);

			if (generatedAddress == expectedAddress) {
				Debug.Log ("address generated successfully");
			} else {
				Debug.LogError ("error generating address. generatedAddress: "+generatedAddress);
			}

			return generatedAddress;

		}
	}

}
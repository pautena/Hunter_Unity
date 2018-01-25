using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using NemApi.Utils;
using System.Security;
using NemApi.Models;
using NemApi.CryptoFunctions;
using Chaos.NaCl;
using System;



namespace NemApi{
	public class TestEncription : MonoBehaviour {

		private string initialPrivateKey="0bb0672d0c9cf25ee0569ba300261fd48d1c0c84b54f7c7b52c3b05c951c9227";
		private string expectedPublicKey="e79acab4b4155fdcbe7d05f09ae9200458f4558cb3fc34b368cffd3dbf0d8ed3";

		private string initialPublicKey = "e79acab4b4155fdcbe7d05f09ae9200458f4558cb3fc34b368cffd3dbf0d8ed3";
		private string expectedAddress="TAUYBFWNP3D26H3UEG2ED6T6DI6YMN3EGEJ3LKFE";

		// Use this for initialization
		void Start () {
			//Good

			Debug.Log ("initialPrivateKey: " + initialPrivateKey);
			PrivateKey privateKey = new PrivateKey (initialPrivateKey);
			TestSign ();
			CalculatePublicKey (privateKey);

			PublicKey publicKey = new PublicKey (initialPublicKey);
			CalculateAddress (publicKey);


		}

		// Update is called once per frame
		void Update () {

		}

		private void TestSign(){

			string myPrivateKey = "593ed16a4c58152d6f2989dfc3acc98460bc1ae16d031153bb994a47bd47c3a4";
			string myPublicKey = "8b242d83b05e64ed6fa367141c0dd30b0c9a49cca486bdb8528270f5096b7d08";
			byte[] privateKeySeedArray = CryptoBytes.FromHexString(initialPrivateKey);
			Array.Reverse(privateKeySeedArray);

			byte[] publicKey = Ed25519.PublicKeyFromSeed (privateKeySeedArray);
			Array.Reverse(publicKey);

			string hexPublicKey = CryptoBytes.ToHexStringLower (publicKey);
			Debug.Log("hexPublicKey " + hexPublicKey);
			Debug.Log ("assertTrueEqual " + myPublicKey == hexPublicKey);

			//byte[] message = CryptoBytes.FromHexString("02534234");
			//byte[] expandedPrivateKey;
			//byte[] publicKey;

			//Ed25519.KeyPairFromSeed (out publicKey, out expandedPrivateKey, privateKeySeedArray);
			//byte[] signature = Ed25519.Sign (message, expandedPrivateKey);


			//bool verified = Ed25519.Verify (signature, message, publicKey);
			//Debug.Log ("verified: " + verified);
			//PublicKeyConversion.PrintByteArray (publicKey);
			//Debug.Log("public key TestSign: "+CryptoBytes.ToHexStringLower(publicKey));
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
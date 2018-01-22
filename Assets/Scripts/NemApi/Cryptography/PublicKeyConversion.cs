using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security;
using NemApi.Utils;
using System;
using Chaos.NaCl;
using NemApi.Models;
using Org.BouncyCastle.Crypto.Digests;
using System.Text;

namespace NemApi.CryptoFunctions{
	public class PublicKeyConversion{
		public static string ToPublicKey(PrivateKey privateKey)
		{
			if (!StringUtils.OnlyHexInString(privateKey.Raw) ||
				privateKey.Raw.Length == 64 && privateKey.Raw.Length == 66)
				throw new ArgumentException("invalid private key");

			string unsecurePrivateKey = StringUtils.ConvertToUnsecureString (privateKey.Raw);
			Debug.Log ("unsecurePrivateKey: " + unsecurePrivateKey);
			byte[] privateKeyArray = CryptoBytes.FromHexString(unsecurePrivateKey);

			Array.Reverse(privateKeyArray);
			PrintByteArray (privateKeyArray);

			byte[] publicKeyArray = Ed25519.PublicKeyFromSeed (privateKeyArray);
			PrintByteArray (publicKeyArray);

			return CryptoBytes.ToHexStringLower(publicKeyArray);
		}

		private static void PrintByteArray(byte[] bytes)
		{
			var sb = new StringBuilder("new byte[] { ");
			foreach (var b in bytes)
			{
				sb.Append(b + ", ");
			}
			sb.Append("}");
			Debug.Log (sb.ToString());
		}
	}
}


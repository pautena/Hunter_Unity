using System;
using NemApi.Models;
using NemApi.CryptoFunctions;

namespace Models
{
	public class User{
		public string secretKey;
		public string publicKey;

		public User (string secretKey,string publicKey){
			this.secretKey = secretKey;
			this.publicKey = publicKey;
		}

		public string GetAddress(byte network){
			PublicKey nemPublicKey = new PublicKey (this.publicKey);
			return AddressEncoding.ToAddress(network,nemPublicKey);

		}
	}
}

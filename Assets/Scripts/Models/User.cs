using System;

namespace Models
{
	public class User{
		public string secretKey;
		public string publicKey;

		public User (string secretKey,string publicKey){
			this.secretKey = secretKey;
			this.publicKey = publicKey;
		}
	}
}

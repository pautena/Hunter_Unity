using System;

namespace Models
{
	public class User{
		public string secretToken;

		public User (string secretToken){
			this.secretToken = secretToken;
		}
	}
}

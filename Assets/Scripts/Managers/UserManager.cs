using System;
using UnityEngine;
using System.IO;

namespace Models.Managers{
	public class UserManager{
		private static UserManager instance;
		public static UserManager GetInstance(){
			if (instance == null){
				instance = new UserManager();
			}
			return instance;
		}


		private User user=null;
		private string userFilePath;
		private string relativeUserPath="userData.json";
		private UserManager (){
			userFilePath = Path.Combine(Application.persistentDataPath, relativeUserPath);

			Debug.Log ("userFilePath: " + userFilePath);
			if (!File.Exists (userFilePath)) {
				File.Create (userFilePath).Dispose();
			}

			Load ();
		}


		public bool HaveUser(){
			return user != null;
		}

		public User GetUser(){
			return user;
		}


		public void Login(string email,string password){
			Debug.Log ("start Login");
			//TODO: Login into nem	
			user = new User(email,password);
			Save ();
		}

		public void Logout(){
			user = null;
			File.Delete (userFilePath);
		}

		private void Save(){
			string json = JsonUtility.ToJson(user);
			Debug.Log ("save user -> json: " + json);

			File.WriteAllText (userFilePath, json);
		}

		private void Load(){
			if (File.Exists (userFilePath)) {
				string json = File.ReadAllText (userFilePath);
				bool haveUser = (json!=null && json.Length > 0);
				Debug.Log ("load user("+haveUser+") -> json: " + json);
				if (haveUser) {
					user = JsonUtility.FromJson<User> (json);
				}
			}
		}
	}
}

using System.Security;
using NemApi.Utils;
using System;

namespace NemApi.Models
{
	/// <summary>
	/// PrivateKey class contains stores private keys. Converts string plain text keys to SecureString for storage.
	/// </summary>
	public class PrivateKey
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PrivateKey"/> class.
		/// </summary>
		/// <remarks>
		/// The <see cref="StringUtils"/> class contains methods to convert to or from secure string.
		/// </remarks>
		/// <param name="key">The SecureString private key.</param>
		public PrivateKey(string key){
			if (!StringUtils.OnlyHexInString(key) || key.Length != 64 && key.Length != 66)
				throw new ArgumentException("invalid private key");

			Raw = StringUtils.ToSecureString(key);
		}

		/// <summary>
		/// Gets the private key SecureString.
		/// </summary>
		/// <remarks>
		/// The <see cref="StringUtils"/> class contains methods to convert to or from secure string.
		/// </remarks>
		/// <value>
		/// The private key secure string.
		/// </value>
		public SecureString Raw { get; private set; }
	}
}
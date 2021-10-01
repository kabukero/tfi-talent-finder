using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.Service.Seguridad
{
	public class Encriptador
	{
		private const int SaltByteSize = 32;
		private const int HashByteSize = 32;
		private const int Iterations = 4096;

		public static string GetSalt()
		{
			var cryptoProvider = new RNGCryptoServiceProvider();
			byte[] b_salt = new byte[SaltByteSize];
			cryptoProvider.GetBytes(b_salt);
			return Convert.ToBase64String(b_salt);
		}

		public static string GetPasswordHash(string password)
		{
			string salt = GetSalt();

			byte[] saltBytes = Convert.FromBase64String(salt);
			byte[] derived = null;

			using(Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
			{
				derived = pbkdf2.GetBytes(HashByteSize);
			}

			return string.Format("{0}:{1}:{2}", Iterations, Convert.ToBase64String(derived), Convert.ToBase64String(saltBytes));
		}

		public static bool VerifyPasswordHash(string password, string hash)
		{
			try
			{
				string[] parts = hash.Split(':');
				byte[] saltBytes = Convert.FromBase64String(parts[2]);
				byte[] derived;
				int iterations = Convert.ToInt32(parts[0]);

				using(Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations))
				{
					derived = pbkdf2.GetBytes(HashByteSize);
				}

				string new_hash = string.Format("{0}:{1}:{2}", Iterations, Convert.ToBase64String(derived), Convert.ToBase64String(saltBytes));

				return hash == new_hash;
			}
			catch
			{
				return false;
			}
		}
	}
}

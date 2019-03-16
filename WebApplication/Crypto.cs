using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace WebApplication
{
	public static class Crypto
	{
		public static byte[] CalculateArgon2Hash(string text, string salt)
		{
			var textBytes = Encoding.Unicode.GetBytes(text);
			var saltBytes = Encoding.Unicode.GetBytes(salt);
			var argon2 = new Argon2id(textBytes)
			{
				DegreeOfParallelism = 12,
				MemorySize = 512,
				Iterations = 20,
				Salt = saltBytes
			};

			return argon2.GetBytes(512);
		}

		public static bool SecureCompareByteArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}

			var result = true;
			for (var i = 0; i < first.Length; i++)
			{
				result = result & (first[i] == second[i]);
			}
			return result;
		}

		public static string GenerateRandomString(int length)
		{
			const int byteSize = 0x100;
			const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&";

			if (length < 0)
				throw new ArgumentOutOfRangeException(nameof(length), "length cannot be less than zero.");

			using (var randomNumberGenerator = RandomNumberGenerator.Create())
			{
				var result = new StringBuilder();
				var buffer = new byte[128];
				while (result.Length < length)
				{
					randomNumberGenerator.GetBytes(buffer);
					for (var i = 0; i < buffer.Length && result.Length < length; ++i)
					{
						var outOfRangeStart = byteSize - (byteSize % allowedChars.Length);
						if (outOfRangeStart <= buffer[i]) continue;
						result.Append(allowedChars[buffer[i] % allowedChars.Length]);
					}
				}
				return result.ToString();
			}
		}
	}
}

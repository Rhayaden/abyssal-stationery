using System.Text;

namespace Blazor.Shared.Utilities
{
	public static class PasswordCrypter
	{
		public static string Encrypt(string password)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(password);
			return Convert.ToBase64String(plainTextBytes);
		}
	}
}

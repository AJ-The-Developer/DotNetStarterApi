using System.Text;

namespace Application;

public class AuthService : IAuthService
{
	public string GeneratePassword()
	{
		const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
		const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		const string digitChars = "0123456789";
		const string specialChars = "!@#$%^&*()_-+=<>?";

		var passwordBuilder = new StringBuilder();

		Random random = new Random();

		passwordBuilder.Append(lowerChars[random.Next(lowerChars.Length)]);
		passwordBuilder.Append(upperChars[random.Next(upperChars.Length)]);
		passwordBuilder.Append(specialChars[random.Next(specialChars.Length)]);
		passwordBuilder.Append(digitChars[random.Next(digitChars.Length)]);

		for (int i = 4; i < 10; i++)
		{
			string chars = lowerChars + upperChars + digitChars + specialChars;
			passwordBuilder.Append(chars[random.Next(chars.Length)]);
		}

		return passwordBuilder.ToString();
	}
}

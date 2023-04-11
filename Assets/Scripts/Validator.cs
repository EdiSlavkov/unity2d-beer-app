using System.Linq;

namespace Scripts
{
    static class Validator
    {
        public static bool HasUpperCase(string text)
        {
            return text.Any(c => char.IsUpper(c));
        }

        public static bool HasLowerCase(string text)
        {
            return text.Any(c => char.IsLower(c));
        }

        public static bool HasNumber(string text)
        {
            return text.Any(c => char.IsNumber(c));
        }

        public static bool IsPasswordValid(string password)
        {
            return password.Length >= Utils.PasswordRequirementLength && HasUpperCase(password) && HasLowerCase(password) && HasNumber(password);
        }

        public static bool IsUserameValid(string username)
        {
            return username.Length >= Utils.UsernameRequirementLength && HasUpperCase(username) && HasLowerCase(username);
        }

        public static bool HasMatched(string password, string repeatPassword)
        {
            return password == repeatPassword;
        }
    }
}

namespace Scripts
{
    static class Utils
    {
        public const int PasswordRequirementLength = 6;
        public const int UsernameRequirementLength = 6;
        public const int MaxFavoriteBeerCount = 15;

        public static string RoundToOneDecimalPlace(float value)
        {
            return value.ToString("0.#");
        }
    }
}
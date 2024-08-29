namespace kzRip.AttackTypes.BruteForce
{
    using System.Text;

    using StatusClasses;

    using static Attempt;
    internal static class BruteForce
    {
        private static bool shouldStop = false;
        public static PasswordResult Result = new PasswordResult();
        internal static void GeneratePassword(StringBuilder password, int currentIndex, int length)
        {
            if (currentIndex == length)
            {
                PasswordResult result = TryPassword("", password.ToString());

                if (result.IsPasswordCorrect)
                {
                    shouldStop = true;
                    Result = result;
                }

                return;
            }

            for (int i = 32; i <= 126; i++)
            {
                if (shouldStop)
                    return;

                password.Append((char)i);
                GeneratePassword(password, currentIndex + 1, length);
                if (password.Length != 0)
                    password.Remove(currentIndex, 1);
            }
        }
    }
}

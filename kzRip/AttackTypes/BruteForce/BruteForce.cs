namespace kzRip.AttackTypes.BruteForce
{
    using System.Text;

    using StatusClasses;

    using static Attempt;
    internal static class BruteForce
    {
        private static bool ShouldStop = false;
        private static string Path = string.Empty;
        private static PasswordResult Result = new PasswordResult();
        internal static void GeneratePassword(StringBuilder password, int currentIndex, int length)
        {
            if (currentIndex == length)
            {
                PasswordResult result = TryPassword(Path, password.ToString());

                if (result.IsPasswordCorrect)
                {
                    ShouldStop = true;
                    Result = result;
                }

                return;
            }

            for (int i = 32; i <= 126; i++)
            {
                if (ShouldStop)
                    return;

                password.Append((char)i);
                GeneratePassword(password, currentIndex + 1, length);
                if (password.Length != 0)
                    password.Remove(currentIndex, 1);
            }
        }
        internal static PasswordResult BruteRip(string path, int minPasswordLength = 0, int maxPasswordLength = 50)
        {
            if (!ArchiveExists(path))
            {
                return new PasswordResult(false, string.Empty, "Archive doesn't exist!");
            }

            Path = path;
            for (int i = minPasswordLength; i <= maxPasswordLength; i++)
            {
                GeneratePassword(new StringBuilder(), 0, i);

                if (Result.IsPasswordCorrect)
                    break;
            }

            return Result;
        }
    }
}

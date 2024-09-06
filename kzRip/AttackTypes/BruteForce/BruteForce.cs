namespace kzRip.AttackTypes.BruteForce
{
    using Statuses;

    using Characters;

    using static Characters.Characters;
    using static Attempt;

    internal static class BruteForce
    {
        private static volatile bool ShouldStop = false;
        private static string Path = string.Empty;
        private static volatile PasswordResult Result = new PasswordResult(false, string.Empty, "Password not found!");
        private static void GeneratePassword(string password, string[] characters, int length)
        {
            if (password.Length == length || password.Length > length)
            {
                PasswordResult result = TryPassword(Path, password);

                if (result.IsPasswordCorrect)
                {
                    ShouldStop = true;
                    Result = result;
                }

                return;
            }

            Parallel.For(0, characters.Length, (i, state) =>
            {
                if (ShouldStop)
                {
                    state.Break();
                    return;
                }

                GeneratePassword(string.Concat(password, characters[i]), characters, length);
            });
        }
        internal static PasswordResult BruteRip(string path, CharactersType flags, int maxPasswordLength = 50, int minPasswordLength = 0)
        {
            if (!File.Exists(path))
            {
                return new PasswordResult(false, string.Empty, $"Archive {path} doesn't exist!");
            }

            Path = path;
            string[] characters = GetCharacters(flags);

            for (int i = minPasswordLength; i <= maxPasswordLength; i++)
            {
                GeneratePassword(string.Empty, characters, i);

                if (Result.IsPasswordCorrect)
                    break;
            }

            return Result;
        }
    }
}

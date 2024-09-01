namespace kzRip.AttackTypes.Dictionary
{
    using StatusClasses;

    using static Attempt;
    internal static class DictionaryAttack
    {
        private static async Task<string[]> GetTxtPasswords(string pathDictionary)
        {
            IAsyncEnumerable<string> content = File.ReadLinesAsync(pathDictionary);

            List<string> passwords = new List<string>();

            await foreach (string line in content)
            {
                passwords.Add(line);
            }

            return passwords.ToArray();
        }
        internal static PasswordResult DictionaryRip(string pathRar, string pathDictionary)
        {
            if (!File.Exists(pathRar))
            {
                return new PasswordResult(false, string.Empty, "Archive doesn't exist!");
            }

            if (!File.Exists(pathDictionary))
            {
                return new PasswordResult(false, string.Empty, "Dictionary file doesn't exist!");
            }

            string fileFormat = Path.GetExtension(pathDictionary);

            if (fileFormat != ".txt")
            {
                return new PasswordResult(false, string.Empty, "Unsupported file format!");
            }

            string[] passwords = Task.Run(() => GetTxtPasswords(pathDictionary)).Result;

            if (passwords.Length == 0)
            {
                return new PasswordResult(false, string.Empty, "Empty file!");
            }

            for(int i = 0; i < passwords.Length; i++)
            {
                PasswordResult result = TryPassword(pathRar, passwords[i]);

                if (result.IsPasswordCorrect)
                {
                    return result;
                }
            }

            return new PasswordResult(false, string.Empty, "Password not found!");
        }
    }
}

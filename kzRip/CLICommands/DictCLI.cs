namespace kzRip.CLICommands
{
    using Statuses;
    using AttackTypes.Dictionary;
    internal static class DictCLI
    {
        internal static void DictionaryAttackHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  kzrip drip <File> <Dictionary>");
            Console.WriteLine();
        }
        internal static void RunDictionary(string[] args)
        {
            if (args.Length == 0 || args[0] == "-h" || args[0] == "--help")
            {
                DictionaryAttackHelp();
                return;
            }

            if (args.Length < 2)
            {
                Console.WriteLine($"kzrip drip {args[0]}");
                Console.WriteLine("Dictionary not provided!");
                return;
            }

            if (args.Length > 2)
            {
                Console.WriteLine($"kzrip drip {string.Join(' ', args)}");
                Console.WriteLine("Something additional was applied to the command!");
                return;
            }

            if (args[0][0] == '\\' || args[0][0] == '/')
            {
                Console.WriteLine($"kzrip drip {string.Join(' ', args)}");
                Console.WriteLine($"Invalid file path: {args[0]}");
                return;
            }

            if (args[1][0] == '\\' || args[1][0] == '/')
            {
                Console.WriteLine($"kzrip drip {string.Join(' ', args)}");
                Console.WriteLine($"Invalid file path: {args[1]}");
                return;
            }

            string pathArchive = Path.IsPathRooted(args[0]) ? args[0] : $@"{Directory.GetCurrentDirectory()}\{args[0]}";
            string pathDictionary = Path.IsPathRooted(args[1]) ? args[1] : $@"{Directory.GetCurrentDirectory()}\{args[1]}";

            PasswordResult result = DictionaryAttack.DictionaryRip(pathArchive, pathDictionary);

            if (result.IsPasswordCorrect)
            {
                Console.WriteLine(result.Message);
                if (result.Password != string.Empty)
                    Console.WriteLine($"Password: {result.Password}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}

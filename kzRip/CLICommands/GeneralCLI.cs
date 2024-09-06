namespace kzRip.CLICommands
{
    internal static class GeneralCLI
    {
        internal static void PrintAlignedCLI(Dictionary<string, string> dict)
        {
            int maxLength = dict.Keys.Max(key => key.Length) + 2;

            foreach (var item in dict)
            {
                Console.WriteLine($"  {item.Key.PadRight(maxLength)}{item.Value}");
            }
        }

        internal static void Help()
        {
            var commands = new Dictionary<string, string>
            {
                {"brip","Brute force attack" },
                {"drip","Dictionary attack" },
            };

            var options = new Dictionary<string, string>
            {
                { "-h, --help", "Print command usage" }
            };


            Console.WriteLine("Usage:");
            Console.WriteLine("  kzrip [command] [options]");
            Console.WriteLine();
            Console.WriteLine("Commands:");

            PrintAlignedCLI(commands);

            Console.WriteLine("Options");

            PrintAlignedCLI(options);
        }
    }
}

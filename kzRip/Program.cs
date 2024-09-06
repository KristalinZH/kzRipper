namespace kzRip
{
    using static CLICommands.DictCLI;
    using static CLICommands.BruteCLI;
    using static CLICommands.GeneralCLI;
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("\nTerminating process...");
                Environment.Exit(0);
            };

            if (args.Length == 0 || args[0] == "-h" || args[0] == "--help")
            {
                Help();
                return;
            }

            switch (args[0])
            {
                case "brip":
                    RunBruteForce(args.Skip(1).ToArray());
                    return;
                case "drip":
                    RunDictionary(args.Skip(1).ToArray());
                    return;
                default:
                    Console.WriteLine("Invalid command!");
                    return;
            }
        }
    }
}

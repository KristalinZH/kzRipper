namespace kzRip.CLICommands
{
    using Statuses;
    using AttackTypes.BruteForce;
    using AttackTypes.BruteForce.Characters;

    using static GeneralCLI;

    internal static class BruteCLI
    {
        private static void BruteForceHelp()
        {
            Dictionary<string, string> options = new Dictionary<string, string>()
            {
                {"-d, --digits","Use digits in passwords" },
                {"-l, --lower", "Use lowercase letters in passwords"},
                {"-u, --upper", "Use uppercase letters in passwords "},
                {"-s, --special", "Use special symbols in passwords" },
                {"-r, --range", "Set passwords length. First argument sets the minimal length, second" },
                {"           ", "sets maximal length. If there is one, it sets the maximal length." },
            };

            Console.WriteLine("Usage:");
            Console.WriteLine("  kzrip brip <File> [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");

            PrintAlignedCLI(options);
        }
        private static BruteFroceOptionsResult GetOptions(string[] options)
        {
            bool isRangeSet = false;
            int? maxLength = null;
            int? minLength = null;

            Dictionary<string, CharactersType> optValues = new()
            {
                {"d", CharactersType.Digits},
                {"l", CharactersType.LowercaseLetters},
                {"u", CharactersType.UppercaseLetters},
                {"s", CharactersType.SpecialSymbols}
            };

            Dictionary<string, CharactersType?> opts = new()
            {
                {"d", null},
                {"l", null},
                {"u", null},
                {"s", null}
            };

            for(int i=0;i< options.Length; i++)
            {
                if (options[i] == "-d" || options[i] == "--digits")
                {
                    opts["d"] = CharactersType.Digits;
                    isRangeSet = false;
                }
                else if (options[i] == "-l" || options[i] == "--lower")
                {
                    opts["l"] = CharactersType.LowercaseLetters;
                    isRangeSet = false;
                }
                else if (options[i] == "-u" || options[i] == "--upper")
                {
                    opts["u"] = CharactersType.UppercaseLetters;
                    isRangeSet = false;
                }
                else if (options[i] == "-s" || options[i] == "--special")
                {
                    opts["s"] = CharactersType.SpecialSymbols;
                    isRangeSet = false;
                }
                else if (options[i] == "-r" || options[i] == "--range")
                    isRangeSet = true;
                else
                {
                    if (options[i][0] == '-')
                    {
                        for (int j = 1; j < options[i].Length; j++)
                        {
                            if (options[i][j] == 'r')
                            {
                                isRangeSet = true;
                                continue;
                            }

                            if (!opts.ContainsKey(options[i][j].ToString()))
                                return new BruteFroceOptionsResult(options[i]);

                            opts[options[i][j].ToString()] = optValues[options[i][j].ToString()];
                        }
                    }
                    else
                    {
                        if (isRangeSet)
                        {
                            bool isParsed = int.TryParse(options[i], out int parsedValue);

                            if (!isParsed)
                                return new BruteFroceOptionsResult(options[i]);

                            if (minLength.HasValue)
                                return new BruteFroceOptionsResult(options[i], "Additional range value!");

                            if (maxLength.HasValue)
                            {
                                minLength = parsedValue;
                                isRangeSet = false;
                            }
                            else
                                maxLength = parsedValue;
                        }
                        else
                            return new BruteFroceOptionsResult(options[i], "Invalid argument order!");
                    }
                }
            }

            CharactersType? flags = null;

            foreach (KeyValuePair<string, CharactersType?> opt in opts)
            {
                if (opt.Value == null)
                    continue;

                if (flags == null)
                    flags = opt.Value;
                else
                    flags |= opt.Value;
            }

            if (maxLength.HasValue && minLength.HasValue)
            {
                if(maxLength.Value<minLength.Value)
                {
                    int? c=maxLength;
                    maxLength = minLength;
                    minLength = c;
                }

                return new BruteFroceOptionsResult(flags!.Value, maxLength.Value, minLength.Value);
            }

            if (maxLength.HasValue)
                return new BruteFroceOptionsResult(flags!.Value, maxLength.Value);

            return new BruteFroceOptionsResult(flags!.Value);
        }
        internal static void RunBruteForce(string[] args)
        {
            if (args.Length == 0 || args[0]=="-h" || args[0] =="--help")
            {
                BruteForceHelp();
                return;
            }

            if (args[0][0]=='\\' || args[0][0] == '/')
            {
                Console.WriteLine($"kzrip brip {string.Join(' ', args)}");
                Console.WriteLine($"Invalid file path: {args[0]}");
                return;
            }

            string path = Path.IsPathRooted(args[0]) ? args[0] : $@"{Directory.GetCurrentDirectory()}\{args[0]}";

            PasswordResult result = null!;

            if (args.Length == 1)
            {
                result = BruteForce.BruteRip(path, CharactersType.Default);
            }
            else
            {
                BruteFroceOptionsResult options = GetOptions(args.Skip(1).ToArray());

                if(!options.AreOptionsValid)
                {
                    Console.WriteLine($"kzrip brip {string.Join(' ', args)}");
                    Console.WriteLine($"{options.Message} {options.InvalidOption}");
                    return;
                }

                if (options.MinimalLength.HasValue)
                    result = BruteForce.BruteRip(path, options.Flags!.Value, options.MaximumLength!.Value, options.MinimalLength!.Value);
                else if (options.MaximumLength.HasValue)
                    result = BruteForce.BruteRip(path, options.Flags!.Value, options.MaximumLength!.Value);
                else
                    result = BruteForce.BruteRip(path, options.Flags!.Value);
            }

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

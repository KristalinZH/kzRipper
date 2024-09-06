namespace kzRip.Statuses
{
    using AttackTypes.BruteForce.Characters;

    internal class BruteFroceOptionsResult
    {
        public BruteFroceOptionsResult()
        {
            AreOptionsValid = false;
            InvalidOption = "Unspecified!";
            Message = "Invalid option:";
            Flags = null;
            MaximumLength = null;
            MinimalLength = null;
        }
        public BruteFroceOptionsResult(string invalidOption):this()
        {
            InvalidOption = invalidOption;
        }
        public BruteFroceOptionsResult(string invalidOption, string message):this(invalidOption)
        {
            Message = message;
        }
        public BruteFroceOptionsResult(CharactersType flags)
        {
            AreOptionsValid = true;
            InvalidOption = null;
            Message = "Valid options!";
            Flags = flags;
        }
        public BruteFroceOptionsResult(CharactersType flags, int maxLength) : this(flags)
        {
            MaximumLength = maxLength;
        }
        public BruteFroceOptionsResult(CharactersType flags, int maxLength, int minLength):this(flags,maxLength)
        {
            MinimalLength = minLength;
        }
        public bool AreOptionsValid { get; private set; }
        public string? InvalidOption { get; private set; }
        public string? Message { get; private set; }
        public CharactersType? Flags { get; private set; }
        public int? MaximumLength { get; private set; }
        public int? MinimalLength { get; private set; }
    }
}

namespace kzRip.AttackTypes.BruteForce.Characters
{
    internal enum CharactersType
    {
        Digits = 1,
        LowercaseLetters = 2,
        UppercaseLetters = 4,
        SpecialSymbols = 8,
        Default = Digits | LowercaseLetters | UppercaseLetters,
        All = Default | SpecialSymbols
    }
}

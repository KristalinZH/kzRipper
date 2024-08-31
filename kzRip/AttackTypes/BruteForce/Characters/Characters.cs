namespace kzRip.AttackTypes.BruteForce.Characters
{
    internal static class Characters
    {
        internal static string[] GetCharacters(CharactersType flags)
        {
            if ((flags & CharactersType.All) == CharactersType.All)
            {
                return Enumerable.Range(32, 95)
                    .Select(x => ((char)x).ToString())
                    .ToArray();
            }

            if ((flags & CharactersType.Default) == CharactersType.Default)
            {
                return Enumerable.Range(48, 10)
                    .Concat(Enumerable.Range(65, 26))
                    .Concat(Enumerable.Range(97, 26))
                    .Select(x => ((char)x).ToString())
                    .ToArray();
            }

            IEnumerable<int> characters = new List<int>();

            if ((flags & CharactersType.Digits) == CharactersType.Digits)
            {
                characters = characters.Concat(Enumerable.Range(48, 10));
            }

            if ((flags & CharactersType.LowercaseLetters) == CharactersType.LowercaseLetters)
            {
                characters = characters.Concat(Enumerable.Range(97, 26));
            }

            if ((flags & CharactersType.UppercaseLetters) == CharactersType.UppercaseLetters)
            {
                characters = characters.Concat(Enumerable.Range(65, 26));
            }

            if ((flags & CharactersType.SpecialSymbols) == CharactersType.SpecialSymbols)
            {
                characters = characters.Concat(Enumerable.Range(32, 16))
                    .Concat(Enumerable.Range(58, 7))
                    .Concat(Enumerable.Range(91, 6))
                    .Concat(Enumerable.Range(123, 4));
            }

            return characters
                .Select(x => ((char)x).ToString())
                .ToArray();
        }
    }
}

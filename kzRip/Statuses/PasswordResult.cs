namespace kzRip.StatusClasses
{
    internal class PasswordResult
    {
        public PasswordResult()
        {
            IsPasswordCorrect = false;
            Password = string.Empty;
        }
        public PasswordResult(bool result, string password)
        {
            IsPasswordCorrect = result;
            Password = password;
        }
        public bool IsPasswordCorrect { get; }
        public string Password { get; }
    }
}

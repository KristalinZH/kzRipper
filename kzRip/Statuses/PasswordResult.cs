namespace kzRip.StatusClasses
{
    internal class PasswordResult
    {
        public PasswordResult()
        {
            IsPasswordCorrect = false;
            Password = string.Empty;
            Message = string.Empty;
        }
        public PasswordResult(bool result, string password, string message)
        {
            IsPasswordCorrect = result;
            Password = password;
            Message = message;
        }
        public bool IsPasswordCorrect { get; }
        public string Password { get; }
        public string Message { get; }
    }
}

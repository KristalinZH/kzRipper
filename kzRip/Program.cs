namespace kzRip
{
    using SharpCompress.Readers;
    using SharpCompress.Archives;
    using SharpCompress.Archives.Rar;

    internal class Program
    {
        static PasswordResult TryPassword(string path, string password)
        {
            using (RarArchive archive = RarArchive.Open(path, new ReaderOptions() { Password = password }))
            {
                RarArchiveEntry? entry = archive.Entries.FirstOrDefault();

                if (entry == null)
                {
                    return new PasswordResult(true, string.Empty, "The archive is empty!");
                }

                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        entry.WriteTo(stream);
                    }
                }
                catch (Exception)
                {
                    return new PasswordResult(false, password, "Invalid password");
                }
            }

            return new PasswordResult(true, password, "Password found!");
        }
        static void Main(string[] args)
        {

        }
    }
}

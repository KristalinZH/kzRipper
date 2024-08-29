namespace kzRip.AttackTypes
{
    using SharpCompress.Readers;
    using SharpCompress.Archives;
    using SharpCompress.Archives.Rar;

    using StatusClasses;
    internal static class Attempt
    {
        internal static PasswordResult TryPassword(string path, string password)
        {
            using (RarArchive archive = RarArchive.Open(path, new ReaderOptions() { Password = password }))
            {
                RarArchiveEntry? entry = archive.Entries.FirstOrDefault();

                if (entry == null)
                {
                    return new PasswordResult(true, string.Empty);
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
                    return new PasswordResult(false, password);
                }
            }

            return new PasswordResult(true, password);
        }
    }
}

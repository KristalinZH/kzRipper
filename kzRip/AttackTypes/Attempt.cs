﻿namespace kzRip.AttackTypes
{
    using SharpCompress.Readers;
    using SharpCompress.Archives;
    using SharpCompress.Archives.Rar;

    using Statuses;
    internal static class Attempt
    {
        internal static PasswordResult TryPassword(string path, string password)
        {
            using (RarArchive archive = RarArchive.Open(path, new ReaderOptions() { Password = password }))
            {
                try
                {
                    RarArchiveEntry? entry = archive.Entries.FirstOrDefault();

                    if (entry == null)
                    {
                        return new PasswordResult(true, string.Empty, "The archive is empty!");
                    }

                    if (!entry.IsEncrypted)
                    {
                        return new PasswordResult(true, string.Empty, "There is no password!");
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        entry.WriteTo(stream);
                    }
                }
                catch (Exception)
                {
                    return new PasswordResult(false, password, "Invalid password!");
                }
            }

            return new PasswordResult(true, password, "Password found!");
        }
    }
}

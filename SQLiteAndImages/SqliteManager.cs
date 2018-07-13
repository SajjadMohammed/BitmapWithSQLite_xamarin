using Android.App;

using SQLite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SQLiteAndImages
{
    internal class SqliteManager
    {
        const string DB_NAME = "ProfileDB.db3";
        readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DB_NAME);
        SQLiteAsyncConnection asyncConnection;

        public SqliteManager()
        {
            CopyDB();
            asyncConnection = new SQLiteAsyncConnection(dbPath);
        }

        public async Task<Profile> GetProfileAsyncById(int id)
        {
            return await asyncConnection.GetAsync<Profile>(id);
        }

        public async Task<List<Profile>> GetProfilesAsync()
        {
            return await asyncConnection.Table<Profile>().ToListAsync();
        }

        public async void InsertProfile(Profile profile)
        {
            await asyncConnection.InsertAsync(profile);
        }

        // copying data base from assets to android device
        private void CopyDB()
        {
            if (File.Exists(dbPath))
            {
                return;
            }

            using (var br = new BinaryReader(Application.Context.Assets.Open(DB_NAME)))
            {
                using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int length = 0;
                    while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, length);
                    }
                }
            }
        }
    }
}
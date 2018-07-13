using SQLite;

namespace SQLiteAndImages
{
    [Table("ProfileTable")]
    internal class Profile
    {
        [Column("Id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("FullName")]
        public string FullName { get; set; }
        [Column("ProfileImage")]
        public byte[] ProfileImage { get; set; }
    }
}
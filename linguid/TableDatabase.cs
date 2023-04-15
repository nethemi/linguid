using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace linguid
{
    [Table("Language")]
    public class Language
    {
        [PrimaryKey, AutoIncrement, Column("LanguageID")]
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Dictionary> dictionary { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<User> user { get; set; }
    }

    [Table("Role")]
    public class Role
    {
        [PrimaryKey, AutoIncrement, Column("RoleID")]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<UserByRole> user { get; set; }
    }

    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("UserID")]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        [ForeignKey(typeof(Language))]
        public int fkLanguage { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Language language { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<UserByRole> role { get; set; }

    }

    [Table("UserByRole")]
    public class UserByRole
    {
        [PrimaryKey, AutoIncrement, Column("UbR")]
        public int UbR { get; set; }

        [ForeignKey(typeof(User))]
        public int fkUser { get; set; }

        [ForeignKey(typeof(Role))]
        public int fkRole { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public User user { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Role role { get; set; }
    }

    [Table("Dictionary")]
    public class Dictionary
    {
        [PrimaryKey, AutoIncrement, Column("ItemID")]
        public int ItemID { get; set; }

        public string Item { get; set; }

        [ForeignKey(typeof(Language))]
        public int fkLanguage { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Language language { get; set; }

        [OneToMany (CascadeOperations = CascadeOperation.All)]
        public List<Meaning> meaning { get; set; }
    }

    [Table("DictionaryRu")]
    public class DictionaryRU
    {
        [PrimaryKey, AutoIncrement, Column("ItemRuID")]
        public int ItemRuID { get; set; }

        public string ItemRu { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Meaning> meaning { get; set; }
    }

    [Table("Transcription")]
    public class Transcription
    {
        [PrimaryKey, AutoIncrement, Column("TranscriptionID")]
        public int TranscriptionID { get; set; }

        public string TranscriptionItem { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Meaning> meaning { get; set; }
    }

    [Table("Meaning")]
    public class Meaning
    {
        [PrimaryKey, AutoIncrement, Column("MeaningID")]
        public int MeaningID { get; set; }

        [ForeignKey(typeof(Dictionary))]
        public int fkItem { get; set; }

        [ForeignKey(typeof(Transcription))]
        public int fkTransctription { get; set; }

        [ForeignKey(typeof(DictionaryRU))]
        public int fkItemRu { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Dictionary dictionary { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Transcription transcription { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public DictionaryRU dictionaryRu { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MeaningByCategory> byCategory { get; set; }

    }

    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement, Column("CategoryID")]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MeaningByCategory> byCategory { get; set; }

    }

    [Table("MeaningByCategory")]
    public class MeaningByCategory
    {
        [PrimaryKey, AutoIncrement, Column("MbCID")]
        public int MbCID { get; set; }

        [ForeignKey(typeof(Category))]
        public int fkCategory { get; set; }

        [ForeignKey(typeof(Meaning))]
        public int fkMeaning { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Category category { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Meaning meaning { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<HistoryLesson> lessons { get; set; }

    }

    [Table("Favorite")]
    public class Favorite
    {
        [PrimaryKey, AutoIncrement, Column("FavoriteID")]
        public int FavoriteID { get; set; }

        [ForeignKey(typeof(User))]
        public int fkUser { get; set; }

        [ForeignKey(typeof(Meaning))]
        public int fkMeaning { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public User user { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Meaning meaning { get; set; }

    }

    [Table("HistoryLesson")]
    public class HistoryLesson
    {
        [PrimaryKey, AutoIncrement, Column("LessonID")]
        public int LessonID { get; set; }

        [ForeignKey(typeof(User))]
        public int fkUser { get; set; }

        [ForeignKey(typeof(MeaningByCategory))]
        public int fkMbC { get; set; }

        public DateTime Date { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public User user { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public MeaningByCategory byCategory{ get; set; }
    }

    [Table("HistoryMeaning")]
    public class HistoryMeaning
    {
        [PrimaryKey, AutoIncrement, Column("HistoryID")]
        public int HistoryID { get; set; }

        [ForeignKey(typeof(User))]
        public int fkUser { get; set; }

        [ForeignKey(typeof(Meaning))]
        public int fkMeaning { get; set; }

        public DateTime Date { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Meaning meaning { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public User user { get; set; }
    }


}

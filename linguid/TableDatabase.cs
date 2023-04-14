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
    }

    [Table("Role")]
    public class Role
    {
        [PrimaryKey, AutoIncrement, Column("RoleID")]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("UserID")]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        public int fkLanguage { get; set; }

    }

    [Table("UserByRole")]
    public class UserByRole
    {
        [PrimaryKey, AutoIncrement, Column("UbR")]
        public int UbR { get; set; }

        public int fkUser { get; set; }

        public int fkRole { get; set; }
    }

    [Table("Dictionary")]
    public class Dictionary
    {
        [PrimaryKey, AutoIncrement, Column("ItemID")]
        public int ItemID { get; set; }

        public string Item { get; set; }

        public int fkLanguage { get; set; }
    }

    [Table("DictionaryRu")]
    public class DictionaryRU
    {
        [PrimaryKey, AutoIncrement, Column("ItemRuID")]
        public int ItemRuID { get; set; }

        public string ItemRu { get; set; }
    }

    [Table("Transcription")]
    public class Transcription
    {
        [PrimaryKey, AutoIncrement, Column("TranscriptionID")]
        public int TranscriptionID { get; set; }

        public string TranscriptionItem { get; set; }
    }

    [Table("Meaning")]
    public class Meaning
    {
        [PrimaryKey, AutoIncrement, Column("MeaningID")]
        public int MeaningID { get; set; }

        public int fkItem { get; set; }

        public int fkTranscription { get; set; }

        public int fkItemRu { get; set; }
    }

    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement, Column("CategoryID")]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
    }

    [Table("MeaningByCategory")]
    public class MeaningByCategory
    {
        [PrimaryKey, AutoIncrement, Column("MbCID")]
        public int MbCID { get; set; }

        public int fkCategory { get; set; }

        public int fkMeaningFk { get; set; }

    }

    [Table("Favorite")]
    public class Favorite
    {
        [PrimaryKey, AutoIncrement, Column("FavoriteID")]
        public int FavoriteID { get; set; }

        public int fkUser { get; set; }

        public int fkMeaning { get; set; }
    }

    [Table("HistoryLesson")]
    public class HistoryLesson
    {
        [PrimaryKey, AutoIncrement, Column("LessonID")]
        public int LessonID { get; set; }

        public int fkUser { get; set; }

        public int fkMbC { get; set; }

        public DateTime Date { get; set; }
    }

    [Table("HistoryMeaning")]
    public class HistoryMeaning
    {
        [PrimaryKey, AutoIncrement, Column("HistoryID")]
        public int HistoryID { get; set; }

        public int fkUser { get; set; }

        public int fkMeaning { get; set; }

        public DateTime Date { get; set; }
    }


}

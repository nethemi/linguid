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

        [ForeignKey(typeof(User)), Column("fkUser"), NotNull]
        public int fkUser { get; set; }

        [ForeignKey(typeof(Role)), Column("fkRole"), NotNull]
        public int fkRole { get; set; }
    }

    [Table("Dictionary")]
    public class Dictionary
    {
        [PrimaryKey, AutoIncrement, Column("ItemID")]
        public int ItemID { get; set; }

        [Column("Item"), NotNull]
        public string Item { get; set; }

        [ForeignKey(typeof(Language)), Column("fkLanguage"), NotNull]
        public int fkLanguage { get; set; }
    }

    [Table("DictionaryRu")]
    public class DictionaryRU
    {
        [PrimaryKey, AutoIncrement, Column("ItemRuID")]
        public int ItemRuID { get; set; }

        [Column("ItemRu"), NotNull]
        public string ItemRu { get; set; }
    }

    [Table("Transcription")]
    public class Transcription
    {
        [PrimaryKey, AutoIncrement, Column("TranscriptionID")]
        public int TranscriptionID { get; set; }

        [Column("Transcription"), NotNull]
        public string TranscriptionItem { get; set; }
    }

    [Table("Meaning")]
    public class Meaning
    {
        [PrimaryKey, AutoIncrement, Column("MeaningID")]
        public int MeaningID { get; set; }

        [ForeignKey(typeof(Dictionary)), Column("fkItem"), NotNull]
        public int fkItem { get; set; }

        [ForeignKey(typeof(Transcription)), Column("fkTranscription"), NotNull]
        public int fkTranscription { get; set; }

        [ForeignKey(typeof(DictionaryRU)), Column("fkItemRu"), NotNull]
        public int fkItemRu { get; set; }
    }

    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement, Column("CategoryID")]
        public int CategoryID { get; set; }

        [Column("Category"), NotNull]
        public string CategoryName { get; set; }
    }

    [Table("MeaningByCategory")]
    public class MeaningByCategory
    {
        [PrimaryKey, AutoIncrement, Column("MbCID")]
        public int MbCID { get; set; }

        [ForeignKey(typeof(Category)), Column("fkCategory"), NotNull]
        public int fkCategory { get; set; }

        [ForeignKey(typeof(Meaning)), Column("fkMeaning"), NotNull]
        public int fkMeaningFk { get; set; }

    }

    [Table("Favorite")]
    public class Favorite
    {
        [PrimaryKey, AutoIncrement, Column("FavoriteID")]
        public int FavoriteID { get; set; }

        [ForeignKey(typeof(User)), Column("fkUser"), NotNull]
        public int fkUser { get; set; }

        [ForeignKey(typeof(Meaning)), Column("fkMeaning"), NotNull]
        public int fkMeaning { get; set; }
    }

    [Table("HistoryLesson")]
    public class HistoryLesson
    {
        [PrimaryKey, AutoIncrement, Column("LessonID")]
        public int LessonID { get; set; }

        [ForeignKey(typeof(User)), Column("fkUser"), NotNull]
        public int fkUser { get; set; }

        [ForeignKey(typeof(MeaningByCategory)), Column("fkMbC"), NotNull]
        public int fkMbC { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }
    }

    [Table("HistoryMeaning")]
    public class HistoryMeaning
    {
        [PrimaryKey, AutoIncrement, Column("HistoryID")]
        public int HistoryID { get; set; }

        [ForeignKey(typeof(User)), Column("fkUser"), NotNull]
        public int fkUser { get; set; }

        [ForeignKey(typeof(Meaning)), Column("fkMeaning"), NotNull]
        public int fkMeaning { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }
    }


}

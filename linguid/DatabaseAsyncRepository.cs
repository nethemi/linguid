using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DevExpress.Mvvm.Native;
using DevExpress.XamarinForms.Core.Themes;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace linguid
{
    public class DatabaseAsyncRepository
    {
        SQLiteAsyncConnection database;

        public DatabaseAsyncRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        #region language
        public async Task CreateLanguage()
        {
            await database.CreateTableAsync<Language>();
        }

        public async Task<List<Language>> GetLanguageAsync()
        {
            return await database.Table<Language>().ToListAsync();

        }

        public async Task<Language> GetLanguageAsync(int id)
        {
            return await database.GetAsync<Language>(id);
        }

        public async Task<int> DeleteLanguageAsync(Language item)
        {
            return await database.DeleteAsync(item);
        }

        public async Task<int> SaveLanguageAsync(Language item)
        {
            if (item.LanguageID != 0)
            {
                await database.UpdateAsync(item);
                return item.LanguageID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        #endregion

        #region role

        public async Task CreateRole()
        {
            await database.CreateTableAsync<Role>();
        }

        public async Task<List<Role>> GetRoleAsync()
        {
            return await database.Table<Role>().ToListAsync();
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await database.GetAsync<Role>(id);
        }
        public async Task<int> DeleteRoleAsync(Role item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveRoleAsync(Role item)
        {
            if (item.RoleID != 0)
            {
                await database.UpdateAsync(item);
                return item.RoleID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region user

        public async Task CreateUser()
        {
            await database.CreateTableAsync<User>();
        }

        public async Task<List<User>> GetUserAsync()
        {
            return await database.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserAsync(string login)
        {
            return await database.GetAsync<User>(login);
        }
        public async Task<int> DeleteUserAsync(User item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveUserAsync(User item)
        {
            if (item.UserID != 0)
            {
                await database.UpdateAsync(item);
                return item.UserID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region user_by_role
        public async Task CreateUbR()
        {
            await database.CreateTableAsync<UserByRole>();
        }

        public async Task<List<UserByRole>> GetUbRAsync()
        {
            return await database.Table<UserByRole>().ToListAsync();
        }

        public async Task<UserByRole> GetUbRAsync(int id)
        {
            return await database.GetAsync<UserByRole>(id);
        }
        public async Task<int> DeleteUbRAsync(UserByRole item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveUbRAsync(UserByRole item)
        {
            if (item.UbR != 0)
            {
                await database.UpdateAsync(item);
                return item.UbR;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region dictionary
        public async Task CreateDictionary()
        {
            await database.CreateTableAsync<Dictionary>();
        }
        public async Task<List<Dictionary>> GetItemsAsync()
        {
            return await database.Table<Dictionary>().ToListAsync();
        }
        public async Task<Dictionary> GetItemAsync(int id)
        {
            return await database.GetAsync<Dictionary>(id);
        }
        public async Task<int> DeleteItemAsync(Dictionary item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(Dictionary item)
        {
            if (item.ItemID != 0)
            {
                await database.UpdateAsync(item);
                return item.ItemID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        //public Task<List<Dictionary>> SearchDictionary(int i)
        //{
        //    return database.Table<Dictionary>().Where(p => p.fkLanguage.Equals(i)).ToListAsync();
        //}
        #endregion

        #region dictionaryRu
        public async Task CreateDictionaryRu()
        {
            await database.CreateTableAsync<DictionaryRU>();
        }
        public async Task<List<DictionaryRU>> GetItemRuAsync()
        {
            return await database.Table<DictionaryRU>().ToListAsync();
        }
        public async Task<DictionaryRU> GetItemRuAsync(int id)
        {
            return await database.GetAsync<DictionaryRU>(id);
        }
        public async Task<int> DeleteItemRuAsync(DictionaryRU item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemRuAsync(DictionaryRU item)
        {
            if (item.ItemRuID != 0)
            {
                await database.UpdateAsync(item);
                return item.ItemRuID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region transcription
        public async Task CreateTranscription()
        {
            await database.CreateTableAsync<Transcription>();
        }
        public async Task<List<Transcription>> GetTranscriptionAsync()
        {
            return await database.Table<Transcription>().ToListAsync();
        }
        public async Task<Transcription> GetTranscriptionAsync(int id)
        {
            return await database.GetAsync<Transcription>(id);
        }
        public async Task<int> DeleteTranscriprionAsync(Transcription item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveTranscriptionAsync(Transcription item)
        {
            if (item.TranscriptionID != 0)
            {
                await database.UpdateAsync(item);
                return item.TranscriptionID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region meaning
        public async Task CreateMeaning()
        {
            await database.CreateTableAsync<Meaning>();
        }
        public async Task<List<Meaning>> GetMeaningAsync()
        {
            return await database.Table<Meaning>().ToListAsync();
        }

        public async Task<Meaning> GetMeaningAsync(int id)
        {
            return await database.GetAsync<Meaning>(id);
        }
        public async Task<int> DeleteMeaningAsync(Meaning item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveMeaningAsync(Meaning item)
        {
            if (item.MeaningID != 0)
            {
                await database.UpdateAsync(item);
                return item.MeaningID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<List<Meaning>> GetMeaningWithChildren()
        {
            return await database.GetAllWithChildrenAsync<Meaning>();
        }

        public async Task<int> SaveMeaningWithChildren(Meaning item)
        {
            if (item.MeaningID != 0)
            {
                await database.UpdateWithChildrenAsync(item);
                return item.MeaningID;
            }
            else
            {
               await database.InsertWithChildrenAsync(item);
                //await database.InsertAllWithChildrenAsync((System.Collections.IEnumerable)item);
               return item.MeaningID;
            }
        }

        #endregion

        #region category
        public async Task CreateCategory()
        {
            await database.CreateTableAsync<Category>();
        }
        public async Task<List<Category>> GetCategoryAsync()
        {
            return await database.Table<Category>().ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await database.GetAsync<Category>(id);
        }
        public async Task<int> DeleteCategoryAsync(Category item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveCategoryAsync(Category item)
        {
            if (item.CategoryID != 0)
            {
                await database.UpdateAsync(item);
                return item.CategoryID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        #endregion

        #region meaningByCategory
        public async Task CreateMbC()
        {
            await database.CreateTableAsync<MeaningByCategory>();
        }
        public async Task<List<MeaningByCategory>> GetMbCAsync()
        {
            return await database.Table<MeaningByCategory>().ToListAsync();
        }
        public async Task<MeaningByCategory> GetMbCAsync(int id)
        {
            return await database.GetAsync<MeaningByCategory>(id);
        }
        public async Task<int> DeleteMbCAsync(MeaningByCategory item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveMbCAsync(MeaningByCategory item)
        {
            if (item.MbCID != 0)
            {
                await database.UpdateAsync(item);
                return item.MbCID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<List<MeaningByCategory>> GetMbCWithChildren()
        {
            return await database.GetAllWithChildrenAsync<MeaningByCategory>();
        }
        #endregion

        #region history_meaning
        public async Task CreateHistory()
        {
            await database.CreateTableAsync<HistoryMeaning>();
        }
        public async Task<List<HistoryMeaning>> GetHistoryAsync()
        {
            return await database.Table<HistoryMeaning>().ToListAsync();
        }
        public async Task<HistoryMeaning> GetHistoryAsync(int id)
        {
            return await database.GetAsync<HistoryMeaning>(id);
        }
        public async Task<int> DeleteHistoryAsync(HistoryMeaning item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveHistoryAsync(HistoryMeaning item)
        {
            if (item.HistoryID != 0)
            {
                await database.UpdateAsync(item);
                return item.HistoryID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<List<HistoryMeaning>> GetHistoryWithChildren()
        {
            return await database.GetAllWithChildrenAsync<HistoryMeaning>();
        }
        #endregion

        #region history_lessons
        public async Task CreateLesson()
        {
            await database.CreateTableAsync<HistoryLesson>();
        }
        public async Task<List<HistoryLesson>> GetLessonAsync()
        {
            return await database.Table<HistoryLesson>().ToListAsync();
        }

        public async Task<HistoryLesson> GetLessonAsync(int id)
        {
            return await database.GetAsync<HistoryLesson>(id);
        }
        public async Task<int> DeleteLessonAsync(HistoryLesson item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveLessonAsync(HistoryLesson item)
        {
            if (item.LessonID != 0)
            {
                await database.UpdateAsync(item);
                return item.LessonID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region favorite
        public async Task CreateFavorite()
        {
            await database.CreateTableAsync<Favorite>();
        }
        public async Task<List<Favorite>> GetFavoriteAsync()
        {
            return await database.Table<Favorite>().ToListAsync();
        }
        public async Task<Favorite> GetFavoriteAsync(int id)
        {
            return await database.GetAsync<Favorite>(id);
        }
        public async Task<int> DeleteFavoriteAsync(Favorite item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveFavoriteAsync(Favorite item)
        {
            if (item.FavoriteID != 0)
            {
                await database.UpdateAsync(item);
                return item.FavoriteID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<List<Favorite>> GetFavoriteWithChildren()
        {
            return await database.GetAllWithChildrenAsync<Favorite>();
        }
        #endregion
    }
}
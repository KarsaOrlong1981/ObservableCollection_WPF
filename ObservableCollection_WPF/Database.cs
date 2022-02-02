using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace ObservableCollection_WPF

{
   
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }
        public Task<List<Person>> GetAllItemsAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }
        public Task<int> AddToDBAsync(Person person)
        {
            return _database.InsertAsync(person);
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _database.Table<Person>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                await _database.DeleteAsync(item);
            }
        }
        public Task<Person> GetItemAsync(int id)
        {
            return _database.Table<Person>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> DeleteAllItems<T>()
        {
            return _database.DeleteAllAsync<Person>();
        }
        public Task<int> GetDBCount()
        {
            return _database.Table<Person>().CountAsync();
        }
    }
}

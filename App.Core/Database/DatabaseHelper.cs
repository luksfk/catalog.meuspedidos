using System.Threading.Tasks;
using SQLite;
using App.Core.ViewModels;
using App.Core.Database;

namespace App.Core.DataBase
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private static string _dbPath;

        public static void CreateDatabase(string dbPath)
        {
            _dbPath = dbPath;

            using (var connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite))
            {
                connection.CreateTable<Favorite>();
            }
        }        

        public async Task AddRemoveFavorite(ProductViewModel product)
        {
            var connection = new SQLiteAsyncConnection(_dbPath);
            product.Favorite = !product.Favorite;
            if (product.Favorite)
            {
                await connection.InsertAsync(new Favorite { ItemId = product.Id });
            }
            else
            {
                string query = string.Format("Delete From {0} Where ItemId = {1}", typeof(Favorite).Name, product.Id);
                await connection.QueryAsync<Favorite>(query);
            }
        }

        public bool IsFavorite(ProductViewModel product)
        {
            var connection = new SQLiteConnection(_dbPath);
            return connection.Table<Favorite>().Where(c => c.ItemId == product.Id).Count() > 0;
        }
    }
}

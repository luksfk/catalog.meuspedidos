using App.Core.ViewModels;
using System.Threading.Tasks;

namespace App.Core.DataBase
{
    public interface IDatabaseHelper
    {
        Task AddRemoveFavorite(ProductViewModel product);
        bool IsFavorite(ProductViewModel product);        
    }
}

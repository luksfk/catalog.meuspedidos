using App.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace App.Core.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly Category _category;        

        public CategoryViewModel(Category category)
        {
            _category = category;
        }

        public string Name => _category.Name;
        public int Id => _category.Id;
    }
}

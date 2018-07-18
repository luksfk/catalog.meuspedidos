using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.Core.Models;
using App.Core.RestService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;

namespace App.Core.ViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        private readonly IRequestService<Category> _service;        

        private readonly ObservableCollection<CategoryViewModel> _categories = new ObservableCollection<CategoryViewModel>();
        public ReadOnlyObservableCollection<CategoryViewModel> Categories { get; private set; }

        public CategoriesViewModel()
        {            
            _service = new RequestService<Category>();
            Categories = new ReadOnlyObservableCollection<CategoryViewModel>(_categories);
        }

        public async Task LoadCountersAsync()
        {
            foreach (var category in await _service.GetAll("http://pastebin.com/raw/YNR2rsWe"))
            {
                _categories.Add(new CategoryViewModel(category));
            }
        }
    }
}

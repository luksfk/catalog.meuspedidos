using App.Core.Models;
using App.Core.RestService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Core.ViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        private readonly IRequestService<Sale> _service;
        private readonly ObservableCollection<SaleViewModel> _sales = new ObservableCollection<SaleViewModel>();
        public ReadOnlyObservableCollection<SaleViewModel> Sales { get; private set; }

        public SalesViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _service = new RequestService<Sale>();
            Sales = new ReadOnlyObservableCollection<SaleViewModel>(_sales);
        }

        public async Task LoadCountersAsync()
        {
            foreach (var sale in await _service.GetAll("https://pastebin.com/raw/R9cJFBtG"))
            {
                _sales.Add(new SaleViewModel(sale));
            }
        }

        public decimal GetDiscount(int quantity, int? categoryId)
        {
            if (!categoryId.HasValue || !Sales.Any()) return 0;

            var sale = Sales.Where(t => t.CategoryId == categoryId).FirstOrDefault();
            if (sale == null) return 0;

            var policy = sale.SalesPolicies.Where(x => quantity >= x.Min).LastOrDefault();
            if (policy == null) return 0;

            return policy.Discount;
        }
    }
}
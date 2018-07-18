using App.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace App.Core.ViewModels
{
    public class SaleViewModel : ViewModelBase
    {
        private readonly Sale _sale;
        private readonly ObservableCollection<SalesPolicyViewModel> _salesPolicies = new ObservableCollection<SalesPolicyViewModel>();
        public ReadOnlyObservableCollection<SalesPolicyViewModel> SalesPolicies { get; private set; }

        public SaleViewModel(Sale sale)
        {
            _sale = sale;
            SalesPolicies = new ReadOnlyObservableCollection<SalesPolicyViewModel>(_salesPolicies);
            foreach (var policy in sale.Policies)
            {
                _salesPolicies.Add(new SalesPolicyViewModel(policy));
            }
        }

        public string Name => _sale.Name;
        public int CategoryId => _sale.Category_Id;
    }

    public class SalesPolicyViewModel
    {
        private readonly SalesPolicy _salePolicy;
        public SalesPolicyViewModel(SalesPolicy salesPolicy)
        {
            _salePolicy = salesPolicy;
        }

        public int Min => _salePolicy.Min;
        public decimal Discount => _salePolicy.Discount;
    }
}
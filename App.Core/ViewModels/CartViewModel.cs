using System.Collections.ObjectModel;
using System.Linq;
using App.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace App.Core.ViewModels
{
    public class CartViewModel : ViewModelBase
    {        
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private readonly ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ReadOnlyObservableCollection<ProductViewModel> Products { get; private set; }

        public CartViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            Products = new ReadOnlyObservableCollection<ProductViewModel>(_products);
        }

        public decimal CartPrice => Products.Sum(t => t.TotalPrice);

        private RelayCommand<ProductViewModel> _addProductToChart;
        public RelayCommand<ProductViewModel> AddProductToChart => _addProductToChart ?? (_addProductToChart = new RelayCommand<ProductViewModel>(AddProduct));

        private void AddProduct(ProductViewModel productViewModel)
        {
            _products.Remove(productViewModel);
            productViewModel.IncrementCommand.Execute(null);
            _products.Add(productViewModel);
        }

        private RelayCommand<ProductViewModel> _removeProductFromChart;
        public RelayCommand<ProductViewModel> RemoveProductFromChart => _removeProductFromChart ?? (_removeProductFromChart = new RelayCommand<ProductViewModel>(RemoveProduct));

        private void RemoveProduct(ProductViewModel productViewModel)
        {
            _products.Remove(productViewModel);
            productViewModel.DecrementCommand.Execute(null);
            if (productViewModel.Quantity > 0)
            {
                _products.Add(productViewModel);
            }
        }

        private RelayCommand _closeCartCommand;
        public RelayCommand CloseCartCommand => _closeCartCommand ?? (_closeCartCommand = new RelayCommand(CloseCart));

        private void CloseCart()
        {
            _navigationService.NavigateTo(ViewModelLocator.CloseCartKey);
        }

        public ProductViewModel ProductInChart(Product product)
        {
            return Products.Where(t => t.Id == product.Id).FirstOrDefault();
        }
    }
}

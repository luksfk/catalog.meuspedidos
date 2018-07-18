using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DataBase;
using App.Core.Models;
using App.Core.RestService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace App.Core.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly IRequestService<Product> _service;
        private string _url;

        private readonly ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ReadOnlyObservableCollection<ProductViewModel> Products { get; private set; }

        public ProductsViewModel(IDatabaseHelper databaseHelper, INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _databaseHelper = databaseHelper;
            _service = new RequestService<Product>();
            Products = new ReadOnlyObservableCollection<ProductViewModel>(_products);
            _url = "https://pastebin.com/raw/eVqp7pfX";
        }

        public async Task LoadProductsAsync()
        {
            var listProducts = await _service.GetAll(_url);

            foreach (var sale in ViewModelLocator.Sales.Sales)
            {
                _products.Add(new ProductViewModel(new Product { Name = sale.Name }, ProductViewModel.TYPE_HEADER, _databaseHelper, _dialogService, _navigationService));
                foreach (var product in listProducts.Where(t => t.Category_Id == sale.CategoryId))
                {
                    _products.Add(new ProductViewModel(product, ProductViewModel.TYPE_ITEM, _databaseHelper, _dialogService, _navigationService));
                    listProducts = listProducts.Except(listProducts.Where(t => t.Category_Id == sale.CategoryId)).ToList();
                }
            }

            _products.Add(new ProductViewModel(new Product { Name = "" }, ProductViewModel.TYPE_HEADER, _databaseHelper, _dialogService, _navigationService));
            foreach (var item in listProducts)
            {
                _products.Add(new ProductViewModel(item, ProductViewModel.TYPE_ITEM, _databaseHelper, _dialogService, _navigationService));
            }
        }

        public async Task LoadByCategory(int categoryId)
        {
            _products.Clear();
            var listProducts = await _service.GetAll(_url);

            if (categoryId != 0)
            {
                listProducts = listProducts.Where(t => t.Category_Id == categoryId).ToList();
            }

            foreach (var sale in ViewModelLocator.Sales.Sales)
            {
                var products = listProducts.Where(t => t.Category_Id == sale.CategoryId);
                if (products.Any())
                {
                    _products.Add(new ProductViewModel(new Product { Name = sale.Name }, ProductViewModel.TYPE_HEADER, _databaseHelper, _dialogService, _navigationService));

                    foreach (var product in products)
                    {
                        var productInCart = ViewModelLocator.Cart.ProductInChart(product);

                        if (productInCart != null)
                        {
                            _products.Add(productInCart);
                        }
                        else
                        {
                            _products.Add(new ProductViewModel(product, ProductViewModel.TYPE_ITEM, _databaseHelper, _dialogService, _navigationService));
                        }

                        listProducts = listProducts.Except(listProducts.Where(t => t.Category_Id == sale.CategoryId)).ToList();
                    }
                }
            }

            if (listProducts.Any())
            {
                _products.Add(new ProductViewModel(new Product { Name = "" }, ProductViewModel.TYPE_HEADER, _databaseHelper, _dialogService, _navigationService));
                foreach (var product in listProducts)
                {
                    var productInCart = ViewModelLocator.Cart.ProductInChart(product);

                    var productToAdd = new ProductViewModel(product, ProductViewModel.TYPE_ITEM, _databaseHelper, _dialogService, _navigationService);

                    if (productInCart != null)
                    {
                        _products.Add(productInCart);
                    }
                    else
                    {
                        _products.Add(new ProductViewModel(product, ProductViewModel.TYPE_ITEM, _databaseHelper, _dialogService, _navigationService));
                    }
                }
            }
        }
    }
}

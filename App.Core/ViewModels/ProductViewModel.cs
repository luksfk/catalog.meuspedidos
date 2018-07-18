using System.Threading.Tasks;
using App.Core.DataBase;
using App.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace App.Core.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        public const int TYPE_ITEM = 1;
        public const int TYPE_HEADER = 2;

        private readonly Product _product;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ProductViewModel(Product product, int typeItem, IDatabaseHelper databaseHelper, IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _product = product;
            _databaseHelper = databaseHelper;

            Name = product.Name;
            TypeItem = typeItem;
            if (TypeItem == TYPE_ITEM)
            {
                Price = product.Price;
                Photo = product.Photo;
                Description = product.Description;
                Favorite = _databaseHelper.IsFavorite(this);
                Discount = 0;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private bool _favorite;
        public bool Favorite
        {
            get { return _favorite; }
            set { Set(ref _favorite, value); }
        }

        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set { Set(ref _photo, value); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { Set(ref _price, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { Set(ref _quantity, value); }
        }

        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set { Set(ref _discount, value); }
        }

        public int Id => _product.Id;
        
        private int _typeItem;
        public int TypeItem
        {
            get { return _typeItem; }
            set { Set(ref _typeItem, value); }
        }

        public int? CategoryId => _product.Category_Id;

        public decimal TotalPrice => (Price * Quantity) * ((100 - Discount) / 100);


        private RelayCommand _incrementCommand;
        public RelayCommand IncrementCommand => _incrementCommand ?? (_incrementCommand = new RelayCommand(() => IncrementAsync()));

        private RelayCommand _decrementCommand;
        public RelayCommand DecrementCommand => _decrementCommand ?? (_decrementCommand = new RelayCommand(() => DecrementAsync()));

        private void IncrementAsync()
        {
            Quantity++;
            Discount = ViewModelLocator.Sales.GetDiscount(Quantity, CategoryId);
            RaisePropertyChanged(() => Quantity);
        }

        private void DecrementAsync()
        {
            if (Quantity > 0)
            {
                Quantity--;
                Discount = ViewModelLocator.Sales.GetDiscount(Quantity, CategoryId);
                RaisePropertyChanged(() => Quantity);
            }
        }



        private RelayCommand _addToFavoriteCommand;
        public RelayCommand AddToFavoriteCommand => _addToFavoriteCommand ?? (_addToFavoriteCommand = new RelayCommand(async () => await AddToFavoriteAsync()));

        private async Task AddToFavoriteAsync()
        {
            await _databaseHelper.AddRemoveFavorite(this);
        }

        private RelayCommand _goBackCommand;
        public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(() => _navigationService.GoBack()));
    }
}

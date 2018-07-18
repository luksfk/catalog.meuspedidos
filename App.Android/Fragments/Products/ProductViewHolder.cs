using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App.Core.ViewModels;

namespace App.Fragments.Products
{
    public class ProductViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView _name;
        private readonly TextView _price;
        private readonly TextView _quantity;
        private readonly TextView _discount;
        private readonly ImageView _photo;
        private readonly ImageView _favorite;

        private ProductViewModel _productViewModel;

        public ProductViewHolder(View itemView) : base(itemView)
        {
            _photo = itemView.FindViewById<ImageView>(Resource.Id.photo);
            _quantity = itemView.FindViewById<TextView>(Resource.Id.quantity);
            _name = itemView.FindViewById<TextView>(Resource.Id.name);
            _price = itemView.FindViewById<TextView>(Resource.Id.price);
            _discount = itemView.FindViewById<TextView>(Resource.Id.discount);
            _favorite = itemView.FindViewById<ImageView>(Resource.Id.favoriteImage);

            var increment = itemView.FindViewById<Button>(Resource.Id.plus);
            increment.Click += IncrementOnClick;

            var decrement = itemView.FindViewById<Button>(Resource.Id.minus);
            decrement.Click += DecrementOnClick;

            var favorite = itemView.FindViewById<Button>(Resource.Id.addFavorite);
            favorite.Click += FavoriteOnClick;
        }

        private void IncrementOnClick(object sender, EventArgs e)
        {
            ViewModelLocator.Cart.AddProductToChart.Execute(_productViewModel);
        }

        private void FavoriteOnClick(object sender, EventArgs eventArgs)
        {
            _productViewModel.AddToFavoriteCommand.Execute(null);
        }

        private void DecrementOnClick(object sender, EventArgs eventArgs)
        {
            ViewModelLocator.Cart.RemoveProductFromChart.Execute(_productViewModel);
        }

        public void BindCounterViewModel(ProductViewModel productViewModel)
        {
            if (_productViewModel != null) _productViewModel.PropertyChanged -= CounterViewModelOnPropertyChanged;

            _productViewModel = productViewModel;
            _productViewModel.PropertyChanged += CounterViewModelOnPropertyChanged;

            Bitmap image = Utils.Utils.DownloadImage(productViewModel.Photo);

            SetFavoriteImage();

            _name.Text = productViewModel.Name;
            if (image != null) _photo.SetImageBitmap(image);
            _quantity.Text = string.Format("{0} UN", productViewModel.Quantity);
            _price.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", productViewModel.Price);
            ShowDiscount();
        }

        private void CounterViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(ProductViewModel.Quantity))
                _quantity.Text = string.Format("{0} UN", _productViewModel.Quantity);

            if (args.PropertyName == nameof(ProductViewModel.Favorite))
                SetFavoriteImage();

            if (args.PropertyName == nameof(ProductViewModel.Discount))
                ShowDiscount();
        }

        private void ShowDiscount()
        {
            if (_productViewModel.Discount > 0)
            {
                _discount.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P02}", _productViewModel.Discount / 100);
                _discount.Visibility = ViewStates.Visible;
            }
            else
            {
                _discount.Text = "";
                _discount.Visibility = ViewStates.Invisible;
            }
        }

        private void SetFavoriteImage()
        {
            if (_productViewModel.Favorite)
            {
                _favorite.SetImageResource(Resource.Drawable.ic_star_full);
            }
            else
            {
                _favorite.SetImageResource(Resource.Drawable.ic_star);
            }
        }
    }
}
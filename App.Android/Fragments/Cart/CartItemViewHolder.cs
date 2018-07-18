using System.Globalization;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App.Core.ViewModels;

namespace App.Fragments.Products
{
    public class CartItemViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView _itemCartName;
        private readonly TextView _itemCartPrice;
        private readonly TextView _itemCartQuantity;
        private readonly TextView _itemCartDiscount;
        private readonly ImageView _itemCartPhoto;

        private ProductViewModel _productViewModel;

        public CartItemViewHolder(View itemView) : base(itemView)
        {
            _itemCartPhoto = itemView.FindViewById<ImageView>(Resource.Id.item_cart_photo);
            _itemCartQuantity = itemView.FindViewById<TextView>(Resource.Id.item_cart_quantity);
            _itemCartName = itemView.FindViewById<TextView>(Resource.Id.item_cart_name);
            _itemCartPrice = itemView.FindViewById<TextView>(Resource.Id.item_cart_price);
            _itemCartDiscount = itemView.FindViewById<TextView>(Resource.Id.item_cart_discount);
        }        

        public void BindCounterViewModel(ProductViewModel productViewModel)
        {            

            _productViewModel = productViewModel;            

            Bitmap image = Utils.Utils.DownloadImage(productViewModel.Photo);

            _itemCartName.Text = productViewModel.Name;
            if (image != null) _itemCartPhoto.SetImageBitmap(image);
            _itemCartQuantity.Text = string.Format("{0} UN", productViewModel.Quantity);
            _itemCartPrice.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", productViewModel.Price);
            ShowDiscount();
        }        

        private void ShowDiscount()
        {
            if (_productViewModel.Discount > 0)
            {
                _itemCartDiscount.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P02}", _productViewModel.Discount / 100);
                _itemCartDiscount.Visibility = ViewStates.Visible;
            }
            else
            {
                _itemCartDiscount.Text = "";
                _itemCartDiscount.Visibility = ViewStates.Invisible;
            }
        }        
    }
}
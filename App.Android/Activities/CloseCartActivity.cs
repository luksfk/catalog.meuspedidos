using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App.Core.ViewModels;
using App.Fragments.Products;
using GalaSoft.MvvmLight.Helpers;

namespace App.Activities
{
    [Activity(Label = "Carrinho")]
    public class CloseCartActivity : BaseActivity
    {
        private RecyclerView _recyclerView;
        private TextView _totalQuantity;
        private TextView _totalPrice;
        private Button _closeCart;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.closeCartRecyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            _recyclerView.SetAdapter(new CartAdapter());

            _totalQuantity = FindViewById<TextView>(Resource.Id.cart_total_quantity);
            _totalPrice = FindViewById<TextView>(Resource.Id.cart_total_price);

            _totalQuantity.Text = string.Format("{0} UN", ViewModelLocator.Cart.CartQuantity.ToString());
            _totalPrice.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", ViewModelLocator.Cart.CartPrice);

            _closeCart = FindViewById<Button>(Resource.Id.cart_close_cart_button);

            _closeCart.SetCommand(nameof(Button.Click), ViewModelLocator.Cart.CloseCartCommand);
        }

        protected override int LayoutResource => Resource.Layout.new_counter;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ViewModelLocator.Cart.GoBackCommand.Execute(null);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App.Core.ViewModels;

namespace App.Fragments.Products
{
    public class CartAdapter : RecyclerView.Adapter
    {
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = ViewModelLocator.Cart.Products[position];
            ((CartItemViewHolder)holder).BindCounterViewModel(item);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cart_item_view, parent, false);
            return new CartItemViewHolder(itemView);
        }

        public override int ItemCount => ViewModelLocator.Cart.Products.Count;
    }
}
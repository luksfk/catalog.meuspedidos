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
    public class ProductsAdapter : RecyclerView.Adapter
    {
        public ProductsAdapter()
        {
            ((INotifyCollectionChanged)ViewModelLocator.Products.Products).CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            NotifyDataSetChanged();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int type = GetItemViewType(position);
            if (type == ProductViewModel.TYPE_HEADER)
            {
                var item = ViewModelLocator.Products.Products[position];
                ((HeaderViewHolder)holder).BindCounterViewModel(item);
            }
            else
            {
                var item = ViewModelLocator.Products.Products[position];
                ((ProductViewHolder)holder).BindCounterViewModel(item);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == ProductViewModel.TYPE_HEADER)
            {
                var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.header_view, parent, false);
                return new HeaderViewHolder(itemView);
            }
            else
            {
                var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.product_view, parent, false);
                return new ProductViewHolder(itemView);
            }
        }

        public override int GetItemViewType(int position)
        {
            return ViewModelLocator.Products.Products[position].TypeItem;
        }

        public override int ItemCount => ViewModelLocator.Products.Products.Count;
    }
}
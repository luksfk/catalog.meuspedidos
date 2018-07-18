using Android;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App.Core.ViewModels;

namespace App.Fragments
{
    public class HeaderViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView _name;

        private ProductViewModel _productViewModel;

        public HeaderViewHolder(View itemView) : base(itemView)
        {
            _name = itemView.FindViewById<TextView>(Resource.Id.headerTextView);
        }

        public void BindCounterViewModel(ProductViewModel productViewModel)
        {
            _productViewModel = productViewModel;
            _name.Text = productViewModel.Name;
        }
    }
}
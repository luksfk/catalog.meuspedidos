using System;
using System.Collections.Generic;
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

namespace App.Activities
{
    [Activity(Label = "Carrinho")]
    public class CloseCartActivity : BaseActivity
    {
        private RecyclerView _recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.closeCartRecyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            _recyclerView.SetAdapter(new CartAdapter());
        }        

        protected override int LayoutResource => Resource.Layout.new_counter;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            Toolbar.InflateMenu(Resource.Menu.new_counter_menu);

            return true;
        }

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
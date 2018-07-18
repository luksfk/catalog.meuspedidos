using System.Collections.Specialized;
using System.Globalization;
using Android;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App.Core.ViewModels;
using GalaSoft.MvvmLight.Helpers;

namespace App.Fragments.Products
{
    public class ProductsFragment : Android.Support.V4.App.Fragment
    {
        public static ProductsFragment NewInstance()
        {
            return new ProductsFragment { Arguments = new Bundle() };
        }

        private RecyclerView _recyclerView;
        private LinearLayout _footer;
        private Button _closeCart;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.counters_fragment, null);

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.countersRecyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            _recyclerView.SetAdapter(new ProductsAdapter());


            _footer = view.FindViewById<LinearLayout>(Resource.Id.footer);
            _closeCart = view.FindViewById<Button>(Resource.Id.closeCart);

            _closeCart.SetCommand(nameof(Button.Click), ViewModelLocator.Cart.CloseCartCommand);


            ((INotifyCollectionChanged)ViewModelLocator.Cart.Products).CollectionChanged += OnCollectionChanged;
            OnCollectionChanged(null, null);

            return view;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ViewModelLocator.Cart.CartPrice > 0)
            {
                _closeCart.Text = string.Format("{0}   {1}   {2}", "COMPRAR", "►", string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", ViewModelLocator.Cart.CartPrice));
                _footer.Visibility = ViewStates.Visible;
                _closeCart.Visibility = ViewStates.Visible;
            }
            else
            {
                _closeCart.Text = "";
                _footer.Visibility = ViewStates.Gone;
                _closeCart.Visibility = ViewStates.Gone;
            }
        }
    }
}
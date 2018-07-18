using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using App.Core.DataBase;
using App.Core.ViewModels;
using JimBobBennett.MvvmLight.AppCompat;

namespace App.Activities
{
    [Activity(Label = "Catálogo", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    public class MainActivity : BaseActivity
    {
        public MainActivity()
        {
            var navigationService = new AppCompatNavigationService();
            //navigationService.Configure(ViewModelLocator.NewCounterPageKey, typeof(NewCounterActivity));
            //navigationService.Configure(ViewModelLocator.EditCounterPageKey, typeof(EditCounterActivity));
            ViewModelLocator.RegisterNavigationService(navigationService);
            ViewModelLocator.RegisterDialogService(new AppCompatDialogService());
        }

        DrawerLayout _drawerLayout;
        NavigationView _navigationView;

        protected override int LayoutResource => Resource.Layout.main;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            //await ViewModelLocator.Counters.LoadCountersAsync();            

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //setup navigation view
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            _navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_counters:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_about:
                        ListItemClicked(1);
                        break;
                }

                _drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                ListItemClicked(0);

                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var dbPath = Path.Combine(path, "counters.db3");

                DatabaseHelper.CreateDatabase(dbPath);                

                await ViewModelLocator.Sales.LoadCountersAsync();
                await ViewModelLocator.Products.LoadCountersAsync();
                await ViewModelLocator.Categories.LoadCountersAsync();

                InvalidateOptionsMenu();                
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (ViewModelLocator.Categories.Categories.Any())
            {
                menu.Clear();
                menu.Add(0, Menu.None, Menu.None, "Todas as categorias");

                foreach (var categoria in ViewModelLocator.Categories.Categories.OrderBy(t => t.Name))
                {
                    menu.Add(0, categoria.Id, Menu.None, categoria.Name);
                }

                menu.SetGroupCheckable(0, true, true);
                menu.GetItem(0).SetChecked(true);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        //int _oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            //if (position == _oldPosition)
            //    return;

            //_oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    //fragment = CountersFragment.NewInstance();
                    break;
                case 1:
                    //fragment = AboutFragment.NewInstance();
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
                default:
                    item.SetChecked(true);
                    ViewModelLocator.Products.LoadByCategory(item.ItemId);
                    return true;
            }
        }
    }
}
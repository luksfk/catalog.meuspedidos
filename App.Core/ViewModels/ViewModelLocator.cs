using App.Core.DataBase;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace App.Core.ViewModels
{
    public static class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IDatabaseHelper>(() => new DatabaseHelper());            
            SimpleIoc.Default.Register<ProductsViewModel>();
            SimpleIoc.Default.Register<SalesViewModel>();
            SimpleIoc.Default.Register<CategoriesViewModel>();
            SimpleIoc.Default.Register<CartViewModel>();
        }

        public static void RegisterNavigationService(INavigationService navigationService)
        {
            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                SimpleIoc.Default.Register(() => navigationService);
            }            
        }

        public static void RegisterDialogService(IDialogService dialogService)
        {
            if (!SimpleIoc.Default.IsRegistered<IDialogService>())
            {
                SimpleIoc.Default.Register(() => dialogService);
            }            
        }

        public const string NewCounterPageKey = "NewCounterPage";
        public const string EditCounterPageKey = "EditCounterPage";

        public static SalesViewModel Sales => ServiceLocator.Current.GetInstance<SalesViewModel>();        
        public static ProductsViewModel Products => ServiceLocator.Current.GetInstance<ProductsViewModel>();
        public static CartViewModel Cart => ServiceLocator.Current.GetInstance<CartViewModel>();
        public static CategoriesViewModel Categories => ServiceLocator.Current.GetInstance<CategoriesViewModel>();
        public static INavigationService NavigationService => ServiceLocator.Current.GetInstance<INavigationService>();
        public static IDatabaseHelper DatabaseHelper => ServiceLocator.Current.GetInstance<IDatabaseHelper>();
        public static IDialogService DialogService => ServiceLocator.Current.GetInstance<IDialogService>();
    }
}
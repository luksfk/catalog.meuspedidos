using App.Core.DataBase;
using App.Core.Models;
using App.Core.ViewModels;
using FluentAssertions;
using GalaSoft.MvvmLight.Views;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Tests.ViewModels
{
    [TestFixture]
    public class ProductViewModelTests
    {
        private Mock<IDatabaseHelper> _mockDatabaseHelper;
        private Mock<IDialogService> _mockDialogService;
        private Mock<INavigationService> _mockNavigationService;

        private ProductViewModel CreateProductViewModel(int id = 10, decimal price = 100, int categoriaId = 2, string description = "Bar", string name = "Foo")
        {
            var procut = new Product { Name = name, Description = description, Id = id, Price = price, Category_Id = categoriaId };
            return new ProductViewModel(procut, ProductViewModel.TYPE_ITEM, _mockDatabaseHelper.Object, _mockDialogService.Object, _mockNavigationService.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _mockDatabaseHelper = new Mock<IDatabaseHelper>();
            _mockDialogService = new Mock<IDialogService>();
            _mockNavigationService = new Mock<INavigationService>();
        }

        [Test]
        public void NameShouldComeFromTheProduct()
        {
            CreateProductViewModel().Name.Should().Be("Foo");
        }

        [Test]
        public void DescriptionShouldComeFromTheProduct()
        {
            CreateProductViewModel().Description.Should().Be("Bar");
        }

        [Test]
        public void IdShouldComeFromTheProduct()
        {
            CreateProductViewModel().Id.Should().Be(10);
        }

        [Test]
        public void ExecutingTheIncrementCommandShouldRaiseAPropertyChangeForValue()
        {
            var vm = CreateProductViewModel();
            vm.MonitorEvents();
            vm.IncrementCommand.Execute(null);
            vm.ShouldRaisePropertyChangeFor(v => vm.Quantity);
        }

        [Test]
        public void SettingTheNameRaisesAPropertyChangeEvent()
        {
            var vm = CreateProductViewModel();
            vm.MonitorEvents();
            vm.Name = "New Name";
            vm.ShouldRaisePropertyChangeFor(v => vm.Name);
        }

        [Test]
        public void SettingTheDescriptionRaisesAPropertyChangeEvent()
        {
            var vm = CreateProductViewModel();
            vm.MonitorEvents();
            vm.Description = "New Description";
            vm.ShouldRaisePropertyChangeFor(v => vm.Description);
        }

        [Test]
        public void ExecutingTheSaveCommandUpdatesTheChangesOnTheProduct()
        {
            var favorite = true;
            var product = new Product { Id = 10, Name = "Name", Description = "Description" };
            var vm = new ProductViewModel(product, ProductViewModel.TYPE_ITEM, _mockDatabaseHelper.Object, _mockDialogService.Object, _mockNavigationService.Object);
            vm.Favorite = favorite;
            vm.AddToFavoriteCommand.Execute(null);
            vm.Favorite.Should().Be(!favorite);
        }
    }
}

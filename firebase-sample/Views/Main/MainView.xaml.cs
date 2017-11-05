using System;
using System.Collections.Generic;
using firebasesample.ViewModels.Main;
using Xamarin.Forms;

namespace firebasesample.Views.Main
{
    public partial class MainView : ContentPage
    {
        MainViewModel viewModel;
        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            viewModel = BindingContext as MainViewModel;

            if (viewModel != null) viewModel.OnAppearing(null);
        }
    }
}

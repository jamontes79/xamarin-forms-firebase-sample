using System;
using System.Collections.Generic;
using firebasesample.ViewModels.Login;
using Xamarin.Forms;

namespace firebasesample.Views.Login
{
    public partial class LoginView : ContentPage
    {
        LoginViewModel viewModel;
        public LoginView()
        {
            try
            {
                InitializeComponent();
            }catch(Exception e){
                String a = "";
            }
        }

        protected override void OnAppearing()
        {
            viewModel = BindingContext as LoginViewModel;

            if (viewModel != null) viewModel.OnAppearing(null);
        }
    }
}

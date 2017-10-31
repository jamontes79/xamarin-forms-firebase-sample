using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using firebasesample.ViewModels.Base;
using firebasesample.ViewModels.Main;

namespace firebasesample.ViewModels.Login
{
    public class LoginViewModel: ViewModelBase
    {
        private ICommand _signUpCommand;
        private ICommand _loginCommand;
        private ICommand _loginGoogleCommand;

  
        private String _username;
        private String _password;
        private IUserDialogs _userDialogService;

        public LoginViewModel(IUserDialogs userDialogsService)
        {
            _userDialogService = userDialogsService;
           
        }

        public ICommand SignUpCommand
        {
            get { return _signUpCommand = _signUpCommand ?? new DelegateCommandAsync(SignUpCommandExecute); }
        }
         
        public ICommand LoginGoogleCommand
        {
            get { return _loginGoogleCommand = _loginGoogleCommand ?? new DelegateCommandAsync(LoginGoogleCommandExecute); }
        }


        public ICommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new DelegateCommandAsync(LoginCommandExecute); }
        }
        public String Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                RaisePropertyChanged();
            }
        }

        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private async Task LoginCommandExecute()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
          
        }

        private async Task SignUpCommandExecute()
        {
            await NavigationService.NavigateToAsync<SignUpViewModel>();
        }


        private async Task LoginGoogleCommandExecute()
        {
            _userDialogService.Toast("Opción no implementada");

        }

    }
}

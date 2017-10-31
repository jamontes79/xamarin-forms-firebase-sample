using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using firebasesample.ViewModels.Base;

namespace firebasesample.ViewModels.Login
{
    public class SignUpViewModel: ViewModelBase
    {
        private ICommand _loginCommand;
        private ICommand _signupCommand;
        private String _username;
        private String _password;
        private IUserDialogs _userDialogService;

        public SignUpViewModel(IUserDialogs userDialogsService)
        {
            _userDialogService = userDialogsService;

        }

        public ICommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new DelegateCommandAsync(LoginCommandExecute); }
        }
        public ICommand SignUpCommand
        {
            get { return _signupCommand = _signupCommand ?? new DelegateCommandAsync(SignUpCommandExecute); }
        }
        private async Task LoginCommandExecute()
        {
            await NavigationService.NavigateToAsync<LoginViewModel>();
        }
        private async Task SignUpCommandExecute()
        {
            _userDialogService.Toast("Opción no implementada");

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
    }
}

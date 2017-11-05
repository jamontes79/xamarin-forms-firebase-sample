using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using firebasesample.Services.FirebaseAuth;
using firebasesample.ViewModels.Base;
using firebasesample.ViewModels.Main;
using Xamarin.Forms;

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

        private IFirebaseAuthService _firebaseService;

        public LoginViewModel(IUserDialogs userDialogsService)
        {
            _userDialogService = userDialogsService;
            _firebaseService = DependencyService.Get<IFirebaseAuthService>();
            MessagingCenter.Subscribe<String, String>(this, _firebaseService.getAuthKey(), (sender, args) =>
            {
                LoginGoogle(args);

            });
           
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
            if (await _firebaseService.SignIn(Username, Password))
            {
                await NavigationService.NavigateToAsync<MainViewModel>();
            }
            else
            {
                _userDialogService.Toast("Usuario o contraseña incorrectos");
            }
          
        }

        private async Task SignUpCommandExecute()
        {
            await NavigationService.NavigateToAsync<SignUpViewModel>();
        }


        private async Task LoginGoogleCommandExecute()
        {
             _firebaseService.SignInWithGoogle();

        }

        private async Task LoginGoogle(String token)
        {
            if (await _firebaseService.SignInWithGoogle(token))
            {
                await NavigationService.NavigateToAsync<MainViewModel>();
            }

        }
    }
}

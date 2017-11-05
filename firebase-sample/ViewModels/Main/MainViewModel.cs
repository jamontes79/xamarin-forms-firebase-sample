using System;
using System.Threading.Tasks;
using System.Windows.Input;
using firebasesample.Services.FirebaseAuth;
using firebasesample.ViewModels.Base;
using firebasesample.ViewModels.Login;
using Xamarin.Forms;

namespace firebasesample.ViewModels.Main
{
    public class MainViewModel : ViewModelBase
    {
        private ICommand _logoutCommand;
        private IFirebaseAuthService _firebaseService;

        public MainViewModel()
        {
            _firebaseService = DependencyService.Get<IFirebaseAuthService>();
        }

        public ICommand LogoutCommand
        {
            get { return _logoutCommand = _logoutCommand ?? new DelegateCommandAsync(LogoutCommandExecute); }
        }

        private async Task LogoutCommandExecute()
        {
            if (await _firebaseService.Logout())
            {
                await NavigationService.NavigateToAsync<LoginViewModel>();
            }


        }
    }
}

using System.Threading.Tasks;
using firebasesample.Services.Navigation;
using firebasesample.ViewModels.Base;
using Xamarin.Forms;

namespace firebasesample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


        }

        protected override void OnStart()
        {
            base.OnStart();

            InitNavigation();
        }
        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

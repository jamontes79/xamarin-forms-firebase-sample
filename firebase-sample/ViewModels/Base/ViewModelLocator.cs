using System;
using Acr.UserDialogs;
using firebasesample.Services.Navigation;
using firebasesample.ViewModels.Login;
using firebasesample.ViewModels.Main;
using Unity;
using Unity.Lifetime;

namespace firebasesample.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _unityContainer;
        private static readonly ViewModelLocator _instance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get
            {
                return _instance;
            }
        }

        public ViewModelLocator()
        {
            _unityContainer = new UnityContainer();

            _unityContainer.RegisterType<MainViewModel>();
            _unityContainer.RegisterType<LoginViewModel>();
            _unityContainer.RegisterType<SignUpViewModel>();


            // Services     
            _unityContainer.RegisterType<INavigationService, NavigationService>();
            _unityContainer.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
           
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public void Register<T>(T instance)
        {
            _unityContainer.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}

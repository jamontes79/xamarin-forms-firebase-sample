using System;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using firebasesample.iOS.Services.FirebaseAuth;
using firebasesample.Services.FirebaseAuth;
using Foundation;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAuthService))]
namespace firebasesample.iOS.Services.FirebaseAuth
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        public static String KEY_AUTH = "auth";
        public static OAuth2Authenticator XAuth;
        private static bool hasLoginResult = false;
        private static bool loginResult = false;
        private static bool signUpResult = false;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token;
        Task t;

        public IntPtr Handle => throw new NotImplementedException();

        

        private void HandleAuthResultHandlerGoogleSignin(User user, NSError error)
        {

            if (error != null)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that SignIn method with credentials could throw
                switch (errorCode)
                {
                    case AuthErrorCode.InvalidCredential:
                    case AuthErrorCode.InvalidEmail:
                    case AuthErrorCode.OperationNotAllowed:
                    case AuthErrorCode.EmailAlreadyInUse:
                    case AuthErrorCode.UserDisabled:
                    case AuthErrorCode.WrongPassword:
                    default:
                        loginResult = false;
                        hasLoginResult = true;
                        break;
                }
            }
            else
            {
                loginResult = true;
                hasLoginResult = true;
            }

            tokenSource.Cancel();
        }

        private void HandleAuthResultHandlerSignUp(User user, Foundation.NSError error)
        {
            if (error != null)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that CreateUser method could throw
                switch (errorCode)
                {
                    case AuthErrorCode.InvalidEmail:
                    case AuthErrorCode.EmailAlreadyInUse:
                    case AuthErrorCode.OperationNotAllowed:
                    case AuthErrorCode.WeakPassword:
                    default:
                        signUpResult = false;
                        hasLoginResult = true;
                        break;
                }
            }
            else
            {
                signUpResult = true;
                hasLoginResult = true;
            }
            tokenSource.Cancel();
        }

        private void HandleAuthResultLoginHandler(User user, Foundation.NSError error)
        {
            if (error != null)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that SignIn method with email and password could throw
                // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                switch (errorCode)
                {
                    case AuthErrorCode.OperationNotAllowed:
                    case AuthErrorCode.InvalidEmail:
                    case AuthErrorCode.UserDisabled:
                    case AuthErrorCode.WrongPassword:
                    default:
                        loginResult = false;
                        hasLoginResult = true;
                        break;
                }
            }
            else
            {
                // Do your magic to handle authentication result
                loginResult = true;
                hasLoginResult = true;
            }
            tokenSource.Cancel();
        }

        public string getAuthKey()
        {
            return KEY_AUTH;
        }

        public bool IsUserSigned()
        {
            var user = Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public async Task<bool> Logout()
        {
            NSError error;
            var signedOut = Auth.DefaultInstance.SignOut(out error);

            if (!signedOut)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that SignOut method with credentials could throw
                // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                switch (errorCode)
                {
                    case AuthErrorCode.KeychainError:
                    default:
                        return false;
                        break;
                }
            }
            else{
                return true;
            }
        }

        public async Task<bool> SignIn(string email, string password)
        {
            
            Auth.DefaultInstance.SignIn(email, password, HandleAuthResultLoginHandler);
            token = tokenSource.Token;
            t = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(4000);
            }, token).Unwrap();
            await t;
            
            
            return loginResult;
        }

        public void SignInWithGoogle()
        {
            XAuth = new OAuth2Authenticator(
                   clientId: "2485447395-h5buvvf05c44j54cmlg3qcnrndi0fd99.apps.googleusercontent.com",
                    clientSecret: "",
                   scope: "profile",
                   authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                   redirectUrl: new Uri("com.googleusercontent.apps.2485447395-h5buvvf05c44j54cmlg3qcnrndi0fd99:/oauth2redirect"),
                   accessTokenUrl: new Uri("https://www.googleapis.com/oauth2/v4/token"),
              isUsingNativeUI: true);
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            XAuth.Completed += OnAuthenticationCompleted;

            XAuth.Error += OnAuthenticationFailed;

            var viewController = XAuth.GetUI();
            vc.PresentViewController(viewController, true, null);

        }

        private void OnAuthenticationFailed(object sender, AuthenticatorErrorEventArgs e)
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            vc.DismissViewController(true, null);
        }

        public async Task<bool> SignInWithGoogle(string tokenId)
        {
            String[] tokens = tokenId.Split(new string[] { "###" }, StringSplitOptions.None);
            var credential = GoogleAuthProvider.GetCredential(tokens[0], tokens[1]);
            Auth.DefaultInstance.SignIn(credential, HandleAuthResultHandlerGoogleSignin);
            token = tokenSource.Token;
            t = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(4000);
            }, token).Unwrap();
            await t;

            return loginResult;
        }

        public async Task<bool> SignUp(string email, string password)
        {
 
            Auth.DefaultInstance.CreateUser(email, password,HandleAuthResultHandlerSignUp);
            token = tokenSource.Token;
            t = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(4000);
            }, token).Unwrap();
            await t;
            return signUpResult;
        }

        private void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            vc.DismissViewController(true, null);

            if (e.IsAuthenticated)
            {
                var access_token = e.Account.Properties["access_token"].ToString();
                var id_token = e.Account.Properties["id_token"].ToString();

                MessagingCenter.Send(FirebaseAuthService.KEY_AUTH, FirebaseAuthService.KEY_AUTH, id_token + "###" + access_token);
               
            }
            else
            {
                //Error
            }

        }
    }
}

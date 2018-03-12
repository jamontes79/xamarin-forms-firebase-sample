using System;
using System.Threading.Tasks;
using firebasesample.Droid.Services.FirebaseAuth;
using firebasesample.Services.FirebaseAuth;
using Xamarin.Forms;
using Firebase.Auth;
using Android.App;
using Android.Content;
using firebasesample.Droid.Activities;

[assembly: Dependency(typeof(FirebaseAuthService))]
namespace firebasesample.Droid.Services.FirebaseAuth
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        public static int REQ_AUTH = 9999;
        public static String KEY_AUTH = "auth";
        public bool IsUserSigned()
        {
            var user = Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).CurrentUser;
            var signedIn = user != null;
            return signedIn;
        }
        public async Task<bool> SignUp(string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).CreateUserWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SignIn(string email, string password)
        {
            try
            {

                await Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void SignInWithGoogle()
        {
            var googleIntent = new Intent(Forms.Context, typeof(GoogleLoginActivity));
            ((Activity)Forms.Context).StartActivityForResult(googleIntent, REQ_AUTH);
        }

        public async Task<bool> SignInWithGoogle(string token)
        {
            try
            {
                AuthCredential credential = GoogleAuthProvider.GetCredential(token, null);
                await Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).SignInWithCredentialAsync(credential);
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }
        public async Task<bool> Logout()
        {
            try
            {
                 Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).SignOut();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string getAuthKey()
        {
            return KEY_AUTH;
        }

        public string GetUserId()
        {
            var user = Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).CurrentUser;
            return user.Uid;
        }
    }
}

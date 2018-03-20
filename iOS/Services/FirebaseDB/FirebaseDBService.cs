using System;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using firebasesample.iOS.Services.FirebaseAuth;
using firebasesample.Services.FirebaseAuth;
using firebasesample.Services.FirebaseDB;
using firebasesample.iOS.Services.FirebaseDB;
using Foundation;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;
using Firebase.Database;

[assembly: Dependency(typeof(FirebaseDBService))]
namespace firebasesample.iOS.Services.FirebaseDB
{
    public class FirebaseDBService : IFirebaseDBService
    {
        public static String KEY_MESSAGE = "message";

        
        private FirebaseAuthService authService = new FirebaseAuthService();
        DatabaseReference databaseReference;
        
        public void Connect(){
            databaseReference = Database.DefaultInstance.GetRootReference();
        }
        public void GetMessage(){
          var userId = authService.GetUserId();
            var messages = databaseReference.GetChild("messages").GetChild(userId);
            nuint handleReference = messages.ObserveEvent(DataEventType.Value, (snapshot) => {
                //var folderData = snapshot.GetValue<NSDictionary>();
                // Do magic with the folder data
                String message = "";
                if (snapshot.GetValue() != NSNull.Null)
                {
                     message = snapshot.GetValue().ToString();
                }
                MessagingCenter.Send(FirebaseDBService.KEY_MESSAGE, FirebaseDBService.KEY_MESSAGE, message);
           });
        }
        public void SetMessage(String message){
            var userId = authService.GetUserId();
            var messages = databaseReference.GetChild("messages").GetChild(userId);
            messages.SetValue<NSString>((Foundation.NSString)message);
        }
        public String GetMessageKey(){
            return KEY_MESSAGE;
        }
    }
}

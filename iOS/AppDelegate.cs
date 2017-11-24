using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using firebasesample.iOS.Services.FirebaseAuth;
using Foundation;
using UIKit;
 
namespace firebasesample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
           


            Firebase.Core.App.Configure();
            return base.FinishedLaunching(app, options);
        }

        // For iOS 9 or newer
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            Uri uri_netfx = new Uri(url.AbsoluteString);

            // load redirect_url Page for parsing
            FirebaseAuthService.XAuth.OnPageLoading(uri_netfx);

            return true;

           
        }

        // For iOS 8 and older
        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            Uri uri_netfx = new Uri(url.AbsoluteString);

            // load redirect_url Page for parsing
            FirebaseAuthService.XAuth.OnPageLoading(uri_netfx);

            return true;
        }
        
    }
}

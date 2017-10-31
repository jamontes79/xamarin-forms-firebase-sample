using System;
using Xamarin.Forms;

namespace firebasesample.Helpers
{
    class DoubleResources
    {
        public static readonly Thickness ButtonGroupPadding = new Thickness(0, Device.OnPlatform(25, 0, 0), 0, 0);
        public static readonly double SignUpButtonHeight = Device.OnPlatform(40, 40, 80);
        public static readonly double FBButtonHeight = Device.OnPlatform(50, 50, 64);
    }
}

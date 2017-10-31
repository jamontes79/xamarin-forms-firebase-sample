using System;
using Xamarin.Forms;

namespace firebasesample.Helpers
{
    public class BoolResources
    {
        public static readonly bool ShouldShowBoxView = Device.OnPlatform(true, false, true);
    }
}

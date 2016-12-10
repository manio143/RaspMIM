using System.Net;
using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string url = "TODO";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            
            var request = WebRequest.Create(url);
            request.Method = "GET";
            using (var responce = request.GetResponse())
            {
                
            }

            SetContentView(Resource.Layout.Main);
        }
    }
}


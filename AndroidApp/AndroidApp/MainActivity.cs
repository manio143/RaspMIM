using System;
using System.Net.Http;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/iot", 
        ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        private const string Url = "http://raspmim.azurewebsites.net/api/data";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.Check).Click += CheckClick;

            FindViewById<Button>(Resource.Id.Plot).Click += delegate
            {
                StartActivity(typeof(PlotActivity));
            };
        }

        private void CheckClick(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            using (HttpResponseMessage response = client.GetAsync(Url).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetMessage("Error occurred, please try later");
                    dialog.SetNeutralButton("OK", delegate { });
                    dialog.Show();
                    return;
                }

                var data = JsonConvert.DeserializeObject<SensorsData>(response.Content.ReadAsStringAsync().Result);

                FindViewById<TextView>(Resource.Id.TemperatureValue).Text = data.Temperature.ToString("##.###");
                FindViewById<TextView>(Resource.Id.SoundValue).Text = data.Sound.ToString();
                FindViewById<TextView>(Resource.Id.LightValue).Text = data.Light.ToString();
                FindViewById<TextView>(Resource.Id.HumidityValue).Text = data.Humidity.ToString("##.###");
                
            }
        }
    }
}


using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.Serialization;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon", 
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
                /*
                XmlSerializer serializer = new XmlSerializer(typeof(SensorsData));
                var reader = new StreamReader(response.Content.ReadAsStreamAsync().Result);
                */

                var data = JsonConvert.DeserializeObject<SensorsData>(response.Content.ReadAsStringAsync().Result);

                FindViewById<TextView>(Resource.Id.TemperatureValue).Text = data.Temperature.ToString("##.###");
                FindViewById<TextView>(Resource.Id.SoundValue).Text = data.Sound.ToString();
                FindViewById<TextView>(Resource.Id.LightValue).Text = data.Light.ToString();
                FindViewById<TextView>(Resource.Id.HumidityValue).Text = data.Humidity.ToString("##.###");
                
                //var data = JsonConvert.DeserializeObject<SensorsData>(response.Content.ReadAsStringAsync().Result);

            }
        }
    }
}


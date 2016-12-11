using System;
using System.Net;
using Newtonsoft;
using System.Net.Http;
using Android.App;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string Url = "http://raspmim.azurewebsites.net/";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            /*
            var firstClient = new HttpClient();
            using (HttpResponseMessage response = firstClient.GetAsync(Url).Result)
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

            firstClient.Dispose();*/

            SetContentView(Resource.Layout.Main);/*
            while (true)
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
            }*/
        }
    }
}


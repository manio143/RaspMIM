using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using BarChart;
using Newtonsoft.Json;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/iot",
         ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    class PlotActivity : Activity
    {
        private const string Url = "http://raspmim.azurewebsites.net/api/dataAll";
        private int NumberOfIntervals = 10;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Plot);

            using (var client = new HttpClient())
            using (
                var response = client.GetAsync($"{Url}/{DateTime.Today:yyyy-MM-dd},{DateTime.Today:yyyy-MM-dd}").Result)
            { 
                var res = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IEnumerable<SensorsData>>(res).ToList();

                data.Sort((a, b) => a.Sound.CompareTo(b.Sound));
                var min = data.First();
                double m = data.Last().Sound - data.First().Sound;
                var newCol = data.Select(
                    s => new Tuple<SensorsData, int>(s, (int) Math.Floor((s.Sound - min.Sound)/(m/NumberOfIntervals))));

                var intervals = new int[NumberOfIntervals];
                foreach (var pair in newCol)
                {
                    if (pair.Item2 >= NumberOfIntervals)
                        intervals[NumberOfIntervals - 1]++;
                    else intervals[pair.Item2]++;
                }


                var data2 = new[] {6f, 7f, 6f, 8f, 5f, 7f};
                var barChart = new BarChartView(this)
                {
                    ItemsSource = Array.ConvertAll(intervals, v => new BarModel {Value = v})
                };
                FindViewById<LinearLayout>(Resource.Id.Lipa).AddView(barChart, new ViewGroup.LayoutParams(
                    ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent));
            }
        }
    }
}
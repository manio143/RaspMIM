using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string Url = "http://raspmim.azurewebsites.net/api/data";
        public MainPage()
        {
            this.InitializeComponent();
            Task.Run(async () => { while (true) { Update(); await Task.Delay(200); } });
        }

        private void Update()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetAsync(Url).Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultMessage = result.Content.ReadAsStringAsync().Result;
                    var sensors = Newtonsoft.Json.JsonConvert.DeserializeObject<SensorsData>(resultMessage);
                    tbTemperature.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => tbTemperature.Text = sensors.Temperature.ToString()).AsTask().Wait();
                    tbSound.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => tbSound.Text = sensors.Sound.ToString()).AsTask().Wait();
                    tbLight.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => tbLight.Text = sensors.Light.ToString()).AsTask().Wait();
                    tbHumidity.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => tbHumidity.Text = sensors.Humidity.ToString()).AsTask().Wait();
                }
            }
        }
    }
}

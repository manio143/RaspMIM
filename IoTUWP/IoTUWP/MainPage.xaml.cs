using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Windows;
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
using System.Collections;
using Mntone.SvgForXaml;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.Text;

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
            //DataContext = new MainViewModel();
            Task.Run(async () => { while (true) { Update(); await Task.Delay(200); } });
            //pltView.NavigateToString("<html><body><p>Plot</p></body></html>");
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

        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var today = DateTime.Today.ToString("yyyy-MM-dd");
                var result = client.GetAsync(Url + "All/" + today + "," + today).Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultMessage = result.Content.ReadAsStringAsync().Result;
                    var sensors = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SensorsData>>(resultMessage);
                    var model = new PlotModel();
                    //model.Series.Add(new LineSeries() { ItemsSource = FilterPlot(sensors) });
                    AddScatterSeries(model, Enumerable.Range(1, sensors.Count()).Select(i => (double)i), FilterPlot(sensors).Cast<double>(), OxyColor.FromArgb(255, 0, 0, 0));

                    var stream = new MemoryStream();
                    var svgExporter = new SvgExporter { Width = 600, Height = 400 };
                    svgExporter.Export(model, stream);
                    string temp = Encoding.UTF8.GetString(stream.ToArray()).Remove(0, 1);
                    var svg = SvgDocument.Parse(temp);
                    //StorageFolder storageFolder = KnownFolders.CameraRoll;//ApplicationData.Current.TemporaryFolder;
                    //StorageFile file = storageFolder.CreateFileAsync("plot.jpg",
                    //        CreationCollisionOption.ReplaceExisting).AsTask().Result;
                    //StorageFile filesvg = storageFolder.CreateFileAsync("plot.svg",
                    //        CreationCollisionOption.ReplaceExisting).AsTask().Result;
                    //using (var streamSvg = filesvg.OpenStreamForWriteAsync().Result)
                    //using (var writer = new StreamWriter(streamSvg))
                    //    writer.Write(temp);
                    temp = temp.Substring(155);
                    temp = "<html><body>" + temp + "</body></html>";
                    pltView.NavigateToString(temp);
                }
            }

        }

        private IEnumerable FilterPlot(IEnumerable<SensorsData> sensors)
        {
            if (optT.IsSelected)
                return sensors.Select(s => s.Temperature);
            else if (optS.IsSelected)
                return sensors.Select(s => (double)s.Sound);
            else if (optL.IsSelected)
                return sensors.Select(s => (double)s.Light);
            else
                return sensors.Select(s => s.Humidity);
        }

        public static void AddScatterSeries(PlotModel model, IEnumerable<double> xSeries, IEnumerable<double> ySeries, OxyColor color)
        {
            var scatterSeries = new ScatterSeries()
            {
                MarkerFill = color,
                MarkerSize = 1,
            };

            foreach (var item in xSeries.Zip(ySeries, (x, y) => new { x, y }))
            {
                scatterSeries.Points.Add(new ScatterPoint(item.x, item.y));
            }

            model.Series.Add(scatterSeries);
        }

    }
}

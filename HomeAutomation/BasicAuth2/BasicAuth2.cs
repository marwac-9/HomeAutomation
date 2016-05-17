using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Simple.OData.Client;
using Interfaces;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Input;



namespace BasicAuth2
{
    [ViewComponent(1,0,ViewComponentCategory.Device)]
    public class BasicAuth2 : ContentPage, IViewComponent
    {
        HttpClient client;
        ODataClient oDataClient;
        public ICommand GetAvaiableDevicesCmd;
        public ICommand GetDevicesAndFeaturesForLocationCmd;
        public ICommand GetLocationsCmd;
        public ICommand GetProductsAndFeaturesCmd;


        public View ViewComponent
        {
            get { return Content; }
        }
        public View GetComponent()
        {
            return Content;
        }

        public BasicAuth2()
        {
            InitializeCommands();
            SetUpUI();
            V3Adapter.Reference();
            var handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential(Constants.Username, Constants.Password);
            
            client = new HttpClient(handler);
            client = new HttpClient();

            client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Constants.Credentials);

            ODataClientSettings settings = new ODataClientSettings(Constants.ODataUrl); // new ODataClientSettings();
            //ODataClientSettings constructor taking in url and credentials is broken, no authentication in header and when inserting authentication we get collection modified
            //Constructor with url only can be used and credentials have to be set manually somewhere like in BeforeRequest
            //Constructor with no parameters can be used and then uri and credentials can be set after on settings object, we don't need to insert authentication
            //settings.BaseUri = new Uri(Constants.ODataUrl);
            //settings.Credentials = new NetworkCredential(Constants.Username, Constants.Password);
            
            settings.OnApplyClientHandler = h =>
            {
                h.PreAuthenticate = true;
            };
            settings.BeforeRequest = r =>
            {
                r.Headers.Authorization = new AuthenticationHeaderValue("Basic", Constants.Credentials);
            };
            settings.OnTrace = (x, y) => 
            {
                string result = string.Format(x, y);
            };

            oDataClient = new ODataClient(settings);

        }

        private void SetUpUI()
        {
            var button = new Button
            {
                Text = "Get AvailableDevices",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            button.Command = GetAvaiableDevicesCmd;

            Content = button;
        }

        void InitializeCommands()
        {
            GetAvaiableDevicesCmd = new Command(
                       async execute =>
                       {
                           try
                           {
                               var devices = await oDataClient.For("AvailableDevices").FindEntriesAsync();
                               foreach (var device in devices)
                               {
                                   string test = (string)device["AvailableDevices"];
                               }
                           }
                           catch (Exception e)
                           {

                               throw;
                           }
                          
                       },
                       canExecute => true);

            GetDevicesAndFeaturesForLocationCmd = new Command(
                       async execute =>
                       {
                           try
                           {
                               var devicesAndFeatures = await oDataClient.For("Locations('" + Constants.Location + "')/Devices").Expand("Features").FindEntriesAsync();
                           }
                           catch (Exception e)
                           {

                               throw;
                           }

                       },
                       canExecute => true);

            GetLocationsCmd = new Command(
                       async execute =>
                       {
                           try
                           {
                               var locations = await oDataClient.FindEntriesAsync("Locations");
                           }
                           catch (Exception e)
                           {

                               throw;
                           }

                       },
                       canExecute => true);

            GetProductsAndFeaturesCmd = new Command(
                       async execute =>
                       {
                           try
                           {
                               var productsAndFeatures = await oDataClient.For("Locations('" + Constants.Location + "')/Products").Expand("Features").FindEntriesAsync();
                           }
                           catch (Exception e)
                           {

                               throw;
                           }

                       },
                       canExecute => true);
        }

        #region Device Setup
        public async Task GetAvaiableDevices()
        {
            //var devices = await oDataClient.FindEntriesAsync("AvailableDevices?$filter=Title eq 'AvailableDevices'");
            var devices = await oDataClient.For("AvailableDevices").Filter("Title eq 'AvailableDevices'").FindEntriesAsync();
        }

        public async Task ProvisionAvaiableDevice(string avaiableDevice)
        {
            string json = "test";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(Constants.RestUrl + "rest/system/provisionDevice?locationId=" + Constants.Location + "&availableDeviceId="+ avaiableDevice, content);
        }
        #endregion

        #region Devices
        public async Task GetDevicesAndFeaturesForLocation()
        {
            var devicesAndFeatures = await oDataClient.For("Locations('" + Constants.Location + "')/Devices").Expand("Features").FindEntriesAsync();
        }

        #endregion

        #region Energy
        public async Task BinarySwitch(string device)
        {
            var binarySwitch = await oDataClient.FindEntriesAsync("BinarySwitches('" + device + "')");
        }
        #endregion

        #region Location
        public async Task GetLocations()
        {
            var locations = await oDataClient.FindEntriesAsync("Locations");
        }

        #endregion

        #region Products
        public async Task GetProductsAndFeatures()
        {
            var productsAndFeatures = await oDataClient.For("Locations('" + Constants.Location + "')/Products").Expand("Features").FindEntriesAsync();
        }
        #endregion

    }

    public class MyContent : HttpContent
    {
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool TryComputeLength(out long length)
        {
            throw new NotImplementedException();
        }
    }
}

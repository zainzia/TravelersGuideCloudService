using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Configuration;
using System.Configuration.Install;

using TravelersGuideCloudService.Models;


namespace TravelersGuideCloudService
{
    public partial class Service1 : ServiceBase
    {

        public GoogleCloudVisionService VisionService;
        public GoogleCloudStorageService StorageService;
        public ServiceHost serviceHost = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            InitializeAPIs();

            InitializeWCFService();
        }

        protected override void OnStop()
        {
            CloseWCFService();
        }

        private void CloseWCFService()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }

        private void InitializeWCFService()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            string strAdrHTTP = "http://localhost:9001/TravelersGuideWCFService";
            string strAdrTCP = "net.tcp://localhost:9002/TravelersGuideWCFService";

            Uri[] adrbase = { new Uri(strAdrHTTP), new Uri(strAdrTCP) };
            serviceHost = new ServiceHost(typeof(TravelersGuideWCFService), adrbase);

            ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
            serviceHost.Description.Behaviors.Add(mBehave);

            BasicHttpBinding httpb = new BasicHttpBinding();
            serviceHost.AddServiceEndpoint(typeof(ITravelersGuide), httpb, strAdrHTTP);
            serviceHost.AddServiceEndpoint(typeof(IMetadataExchange),
            MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            serviceHost.OpenTimeout = new TimeSpan(0, 5, 0);
            serviceHost.CloseTimeout = new TimeSpan(0, 5, 0);

            NetTcpBinding tcpb = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(ITravelersGuide), tcpb, strAdrTCP);
            serviceHost.AddServiceEndpoint(typeof(IMetadataExchange),
            MetadataExchangeBindings.CreateMexTcpBinding(), "mex");


            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();
        }

        public void InitializeAPIs()
        {
            VisionService = new GoogleCloudVisionService();
            StorageService = new GoogleCloudStorageService();
        }
    }
}

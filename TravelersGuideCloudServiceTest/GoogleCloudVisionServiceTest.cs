using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TravelersGuideCloudService;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;

namespace TravelersGuideCloudServiceTest
{
    [TestClass]
    public class GoogleCloudVisionServiceTest
    {

        GoogleCloudVisionService service;

        [TestInitialize]
        public void TestInitialize()
        {
            service = new GoogleCloudVisionService();
        }
        

        [TestMethod]
        public void TestCreateAuthorizedClient()
        {
            TestInitialize();
            VisionService visionService = service.CreateAuthorizedClient();
        }

        [TestMethod]
        public void TestDetectText()
        {
            TestInitialize();

            VisionService visionService = service.CreateAuthorizedClient();
            byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\TravelersGuide\Images\EidMubarak.jpg");
            service.DetectText(visionService, imageArray);
        }
    }
}

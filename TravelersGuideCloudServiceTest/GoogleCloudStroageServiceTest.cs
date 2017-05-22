using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TravelersGuideCloudService;

namespace TravelersGuideCloudServiceTest
{
    [TestClass]
    public class GoogleCloudStorageServiceTest
    {

        GoogleCloudStorageService service;

        [TestInitialize]
        public void TestInitialize()
        {
            service = new GoogleCloudStorageService();
        }

        [TestMethod]
        public void TestCreateStorageClient()
        {
            TestInitialize();
        }


        [TestMethod]
        public void TestListBuckets()
        {
            TestInitialize();
            service.ListBuckets("travlersguide-1382");
        }

        [TestMethod]
        public void TestUploadStream()
        {
            TestInitialize();
            service.UploadStream("travlersguide-1382.appspot.com");
        }

        [TestMethod]
        public void TestUploadImage()
        {
            TestInitialize();
            service.UploadImage("travlersguide-1382.appspot.com", @"C:\Users\Zain\Pictures\IMG_20160731_004733.jpg", "EidMubarak.jpg");
        }

        [TestMethod]
        public void TestDeleteObject()
        {
            TestInitialize();
            service.DeleteObject("travlersguide-1382.appspot.com", "EidMubarak.jpg");
        }
    }
}

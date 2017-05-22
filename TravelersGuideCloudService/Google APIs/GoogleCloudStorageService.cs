using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;


using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Download;
using System.Net.Http;


namespace TravelersGuideCloudService
{
    public class GoogleCloudStorageService
    {

        public GoogleCloudStorageService()
        {

        }

        public StorageService CreateStorageClient()
        {
            var credentials = Google.Apis.Auth.OAuth2.GoogleCredential.GetApplicationDefaultAsync().Result;

            if (credentials.IsCreateScopedRequired)
            {
                credentials = credentials.CreateScoped(new[] { StorageService.Scope.DevstorageFullControl });
            }

            var serviceInitializer = new BaseClientService.Initializer()
            {
                ApplicationName = "Travelers Guide",
                HttpClientInitializer = credentials
            };

            return new StorageService(serviceInitializer);
        }

        // [START list_buckets]
        public void ListBuckets(string projectId)
        {
            StorageService storage = CreateStorageClient();

            var buckets = storage.Buckets.List(projectId).Execute();

        }
        // [END list_buckets]


        // [START upload_stream]
        public void UploadStream(string bucketName)
        {
            StorageService storage = CreateStorageClient();

            var content = "My text object content";
            var uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            storage.Objects.Insert(
                bucket: bucketName,
                stream: uploadStream,
                contentType: "text/plain",
                body: new Google.Apis.Storage.v1.Data.Object() { Name = "my-file.txt" }
            ).Upload();

            Console.WriteLine("Uploaded my-file.txt");
        }
        // [END upload_stream]


        // [START upload_image]
        public void UploadImage(string bucketName, string ImagePath, string FileName)
        {
            try
            {
                StorageService storage = CreateStorageClient();

                MemoryStream uploadStream = new MemoryStream();
                var content = Bitmap.FromFile(ImagePath);
                content.Save(uploadStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                storage.Objects.Insert(
                    bucket: bucketName,
                    stream: uploadStream,
                    contentType: "image/jpg",
                    body: new Google.Apis.Storage.v1.Data.Object() { Name = FileName }
                ).Upload();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }
        // [END upload_image]


        // [START delete_object]
        public void DeleteObject(string bucketName, string FileName)
        {
            try
            {
                StorageService storage = CreateStorageClient();
                storage.Objects.Delete(bucketName, FileName).Execute();
            }
            catch(Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }
        // [END delete_object]

    }
}

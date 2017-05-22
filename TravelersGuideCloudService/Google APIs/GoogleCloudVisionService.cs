using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net;

using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Download;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;

namespace TravelersGuideCloudService
{
    public class GoogleCloudVisionService
    {

        public GoogleCloudVisionService()
        {
            
        }

        public VisionService CreateAuthorizedClient()
        {
            GoogleCredential credential =
                GoogleCredential.GetApplicationDefaultAsync().Result;
            // Inject the Cloud Vision scopes
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    VisionService.Scope.CloudPlatform
                });
            }
            VisionService service = new VisionService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                GZipEnabled = false
            });

            return service;
        }


        public List<string> DetectText(VisionService vision, byte[] imageArray)
        {
            // Convert image to Base64 encoded for JSON ASCII text based request   
            
            
            string imageContent = Convert.ToBase64String(imageArray);
            // Post text detection request to the Vision API
            var responses = vision.Images.Annotate(
                new BatchAnnotateImagesRequest()
                {
                    Requests = new[] {
                    new AnnotateImageRequest() {
                        Features = new [] { new Feature() { Type =
                          "TEXT_DETECTION"}},
                        Image = new Image() { Content = imageContent }
                    }
               }
                }).Execute();

            List<string> DetectedText = new List<string>();
            IList<AnnotateImageResponse> imageResponses = responses.Responses;

            foreach (AnnotateImageResponse imageResponse in imageResponses)
            {
                if (imageResponse.Error != null || imageResponse.TextAnnotations == null)
                    return null;

                foreach(EntityAnnotation textAnnotation in imageResponse.TextAnnotations)
                {
                    DetectedText.Add(textAnnotation.Description);
                }
            }

            return DetectedText;
        }

    }
}

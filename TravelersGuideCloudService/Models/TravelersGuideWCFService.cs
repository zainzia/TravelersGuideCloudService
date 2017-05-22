using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelersGuideCloudService.Models
{

    public class TravelersGuideWCFService : ITravelersGuide
    {
        GoogleCloudVisionService VisionService;
        GoogleCloudTranslateService TranslateService; 

        public TravelersGuideWCFService()
        {
            VisionService = new GoogleCloudVisionService();
            TranslateService = new GoogleCloudTranslateService();
        }

        public double TestAddMethod(int num1, int num2)
        {
            return num1 + num2;
        }

        public void UploadImage(byte[] imageByteArray)
        {

        }

        public Task<string> DetectLanguage(string TextToTranslate)
        {
            return TranslateService.DetectLanguage(TextToTranslate);
        }

        public Task<string> GetTranslation(string TextToTranslate, string source, string target)
        {
            return TranslateService.GetTranslation(TextToTranslate, source, target);
        }

        public Task<string> DetectLanguageAndGetTranslation(string TextToTranslate, string target, out string source)
        {
            source = DetectLanguage(TextToTranslate).Result;
            if (source != null)
            {
                return GetTranslation(TextToTranslate, source, target);
            }

            return null;
        }

        
        public List<string> GetImageText(byte[] ImageByteArray)
        {
            List<string> ImageText;
            ImageText = VisionService.DetectText(VisionService.CreateAuthorizedClient(), ImageByteArray);
            return ImageText;
        }
    }
}

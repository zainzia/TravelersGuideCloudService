using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;

namespace TravelersGuideCloudService.Models
{
    [ServiceContract(Namespace = "TravelersGuideCloudService")]
    public interface ITravelersGuide
    {
        [OperationContract]
        double TestAddMethod(int num1, int num2);

        [OperationContract]
        void UploadImage(byte[] imageByteArray);

        [OperationContract]
        List<string> GetImageText(byte[] ImageByteArray);

        [OperationContract]
        Task<string> DetectLanguage(string TextToTranslate);

        [OperationContract]
        Task<string> GetTranslation(string TextToTranslate, string source, string target);

        [OperationContract]
        Task<string> DetectLanguageAndGetTranslation(string TextToTranslate, string target, out string source);
    }
}

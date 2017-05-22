using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TravelersGuideCloudService
{
    public class GoogleCloudTranslateService
    {

        private StringBuilder googleTranslationURL;

        private string textToTranslate;
        private string translatedText;
        private string sourceLanguage;
        private string targetLanguage;

        public Func<string, string> OnResultsObtained;
        public Func<string, string> OnLanguageDetected;

        public GoogleCloudTranslateService()
        {
            googleTranslationURL = new StringBuilder();
        }

        /// <summary>
        /// Translates given Text
        /// </summary>
        /// <param name="text">text to Translate</param>
        /// <returns>Translated Text. Returns -1 if there is an exception</returns>
        public async Task<string> GetTranslation(string text, string sourceLang, string targetLang)
        {
            try
            {
                this.textToTranslate = text;
                this.sourceLanguage = sourceLang;
                this.targetLanguage = targetLang;

                //Setup Call to Google API
                //Append First Part
                googleTranslationURL.Append("https://www.googleapis.com/language/translate/v2?");
                //Append Key
                googleTranslationURL.Append("key=AIzaSyCepszr8omfGuPTtlBvOnLx1OxaSIgd0TU");
                //Append text to Translate
                googleTranslationURL.Append("&q=");
                googleTranslationURL.Append(textToTranslate);
                //Append Source Language
                googleTranslationURL.Append("&source=");
                googleTranslationURL.Append(sourceLanguage);
                //Append Destination Language
                googleTranslationURL.Append("&target=");
                googleTranslationURL.Append(targetLanguage);

                //var formContent = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("q", textToTranslate)
                //});

                //Make asynch Call to Google API
                //HttpWebRequest wr = WebRequest.Create(googleTranslationURL.ToString()) as HttpWebRequest;
                //wr.BeginGetResponse(WebRequestResponseCallback, wr);
                HttpClient client = new HttpClient();
                var response = client.GetStringAsync(googleTranslationURL.ToString());

                googleTranslationURL.Clear();

                JObject deserializedObject = JObject.Parse(await response);
                translatedText = deserializedObject["data"]["translations"][0]["translatedText"].ToString();
                return translatedText;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally { }

            return null;
        }


        public async Task<string> DetectLanguage(string text)
        {
            try
            {
                textToTranslate = text;

                //Setup call to Google API
                //https://www.googleapis.com/language/translate/v2/detect?key=YOUR_API_KEY&q=google+translate+is+fast

                //Append First Part
                googleTranslationURL.Append("https://www.googleapis.com/language/translate/v2/detect?");
                googleTranslationURL.Append("key=AIzaSyCepszr8omfGuPTtlBvOnLx1OxaSIgd0TU");
                googleTranslationURL.Append("&q=");
                googleTranslationURL.Append(text);


                //HttpWebRequest wr = WebRequest.Create(googleTranslationURL.ToString()) as HttpWebRequest;
                //wr.BeginGetResponse(WebRequestResponseCallback, wr);
                HttpClient client = new HttpClient();
                var getStringTask = client.GetStringAsync(googleTranslationURL.ToString());

                JObject deserializedObject = JObject.Parse(await getStringTask);
                string sourceLanguagePrefix = deserializedObject["data"]["detections"][0][0]["language"].ToString();
                return sourceLanguagePrefix;

                //sourceLanguage = db.GetLanguageFromPrefix(sourceLanguagePrefix).Name;
                //OnLanguageDetected(sourceLanguage);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                googleTranslationURL.Clear();
            }

            return null;
        }
    }
}

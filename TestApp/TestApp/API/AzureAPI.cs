using System;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestApp.Model;
using Newtonsoft.Json;

namespace TestApp.API
{
    class AzureAPI
    {
        public static double getSentimentScore(string text)
        {
            return MakeRequest(text).Result;
        }



        static async Task<double> MakeRequest(string inputText)
        {
            AzureResponseRootObject result = null;
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers            
            client.DefaultRequestHeaders.Add(Constants.AzureRequestHeaderSubscriptionKeyName, Constants.AzureRequestHeaderSubscriptionKeyValue);


            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment?" + queryString;

            HttpResponseMessage response;

            // Request body

            string body = @"{""documents"":[{""id"":""1"", ""text"":""@inputText""}]}".Replace("@inputText", inputText);
            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //response = await client.PostAsync(uri, content);
                response = client.PostAsync(uri, content).Result;

                string responseContent = response.Content.ReadAsStringAsync().Result;

                System.Diagnostics.Debug.WriteLine(string.Format("Azure Request URI - {0}", uri));
                System.Diagnostics.Debug.WriteLine(string.Format("Azure Response - {0}", responseContent));

                result = JsonConvert.DeserializeObject<AzureResponseRootObject>(responseContent);

            }

            return result.documents[0].score;
        }



    }
}


using LimerickStreetArtApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LimerickStreetArtApp.Data
{
    public class RestService
    {
        private HttpClient client;

       // private readonly string grant_type = "password";

        public RestService() 
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlenconded"));
        }


        public async Task<T> PostResponse<T>(string weburl, string jsonstring) where T : class
        {
           

            try
            {
                HttpResponseMessage httpResponseMessage = await client.PostAsync(weburl, new StringContent(jsonstring, Encoding.UTF8, "application/json"));

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResult = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }
        public async Task<HttpResponseMessage> PostResponse_HTTPRESPONSE(string weburl, string jsonstring)
        {
           

            try
            {
                HttpResponseMessage httpResponseMessage = await client.PostAsync(weburl, new StringContent(jsonstring, Encoding.UTF8, "application/json"));
                return httpResponseMessage;

            }
            catch (Exception e)
            {

            }

            return null;
        }

        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
           

            try
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(weburl);
                string jsonResulta = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResult = await httpResponseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }
        public async Task<HttpResponseMessage> GetResponse_HTTPRESPONSE(string weburl)
        {
            

            try
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(weburl);
                return httpResponseMessage;
            }
            catch (Exception e)
            {

            }

            return null;
        }
    }

   
}

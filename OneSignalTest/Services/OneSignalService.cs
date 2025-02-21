using Microsoft.Extensions.Configuration;
using OneSignalApi.Api;
using OneSignalApi.Client;
using OneSignalApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OneSignalTest.Models;
using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

namespace OneSignalTest.Services
{
    public class OneSignalService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public OneSignalService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> SendNotificationAsync(string message, OneSignalUser user)
        {
            var appId = _config["OneSignal:AppId"];
            var apiKey = _config["OneSignal:ApiKey"];
            var baseUrl = _config["OneSignal:BaseUrl"];
            
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);    
            client.DefaultRequestHeaders.Add("Authorization", $"Key {apiKey}");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", $"application/json");
            
            
            var data = new
            {
                app_id = appId,
                contents = new { en = message },
                included_segments = new[] { "Subscribed Users" },
                include_aliases = new
                {
                    onesignal_id = new[] {user.PlayerId},
                },
                target_channel = "push"
            };
            
            string payload = JsonConvert.SerializeObject(data, Formatting.Indented);
            Console.WriteLine("Request Payload: " + payload);



            var result = false;
            
            try
            {
                var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "/notifications");
                
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                await client.SendAsync(request).ContinueWith(response =>
                {
                    Console.WriteLine(response.Result.Content.ReadAsStringAsync().Result);
                    result = true;
                });
                
                return result;

            }
            catch (ApiException e)
            {
                Console.WriteLine("Notification Error: " + e.Message);
                return false;
            }

        }
        
        
        
    }
}


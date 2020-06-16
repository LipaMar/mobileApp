
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using mobileApp.Models;
using Android.App;

namespace mobileApp.Services
{
    class MessageDataStore:IDataStore<Message>
    {
HttpClient client;
        IEnumerable<Message> students;

        public MessageDataStore()
        {

            client = new HttpClient(GetInsecureHandler());
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            students = new List<Message>();
        }
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain,
           errors) =>
            {
                return true;
            };
            return handler;
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Message>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/messages");
                students = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Message>>(json));
            }

            return students;
        }

        public async Task<Message> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/messages/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Message>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Message item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/messages", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Message item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/messages/{item.Id}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/messages/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

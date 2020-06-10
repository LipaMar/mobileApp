using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using mobileApp.Models;

namespace mobileApp.Services
{
    public class AzureDataStore : IDataStore<Student>
    {
        HttpClient client;
        IEnumerable<Student> students;

        public AzureDataStore()
        {

            client = new HttpClient(GetInsecureHandler());
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            students = new List<Student>();
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
        public async Task<IEnumerable<Student>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                students = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Student>>(json));
            }

            return students;
        }

        public async Task<Student> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Student>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Student item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            Console.WriteLine(content);
            var response = await client.PostAsync($"api/item",content );
            Console.WriteLine("ZAWARTOŚĆ"+response.Content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

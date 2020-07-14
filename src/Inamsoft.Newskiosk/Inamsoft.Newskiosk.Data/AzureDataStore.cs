using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Inamsoft.Newskiosk.Abstractions;
using Inamsoft.Newskiosk.Abstractions.Models;

namespace Inamsoft.Newskiosk.Data
{
    public class AzureDataStore : BaseDataStore, IDataStore<NewsLinkItem>
    {
        HttpClient client;
        IEnumerable<NewsLinkItem> items;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://10.0.2.2:5000/");

            items = new List<NewsLinkItem>();
        }

        public async Task<IEnumerable<NewsLinkItem>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await ObjectSerializer.Default.DeserializeAsync<IEnumerable<NewsLinkItem>>(json);
            }

            return items;
        }

        public async Task<NewsLinkItem> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await ObjectSerializer.Default.DeserializeAsync<NewsLinkItem>(json);
            }

            return null;
        }

        public async Task<bool> AddItemAsync(NewsLinkItem item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = await ObjectSerializer.Default.SerializeAsync(item)
                                                               .ConfigureAwait(false);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(NewsLinkItem item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            var serializedItem = await ObjectSerializer.Default.SerializeAsync(item)
                                                               .ConfigureAwait(false);
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

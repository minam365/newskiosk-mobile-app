using Inamsoft.Newskiosk.Abstractions;
using Inamsoft.Newskiosk.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inamsoft.Newskiosk.Data
{
    public class MockDataStore : BaseDataStore, IDataStore<NewsLinkItem>
    {
        readonly List<NewsLinkItem> NewsLinkItems;

        public MockDataStore()
        {
            NewsLinkItems = new List<NewsLinkItem>()
            {
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "First NewsLinkItem", Description="This is an NewsLinkItem description." },
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Second NewsLinkItem", Description="This is an NewsLinkItem description." },
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Third NewsLinkItem", Description="This is an NewsLinkItem description." },
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Fourth NewsLinkItem", Description="This is an NewsLinkItem description." },
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Fifth NewsLinkItem", Description="This is an NewsLinkItem description." },
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Sixth NewsLinkItem", Description="This is an NewsLinkItem description." }
            };
        }

        public async Task<bool> AddItemAsync(NewsLinkItem NewsLinkItem)
        {
            NewsLinkItems.Add(NewsLinkItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(NewsLinkItem NewsLinkItem)
        {
            var oldNewsLinkItem = NewsLinkItems.Where((NewsLinkItem arg) => arg.Id == NewsLinkItem.Id).FirstOrDefault();
            NewsLinkItems.Remove(oldNewsLinkItem);
            NewsLinkItems.Add(NewsLinkItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldNewsLinkItem = NewsLinkItems.Where((NewsLinkItem arg) => arg.Id == id).FirstOrDefault();
            NewsLinkItems.Remove(oldNewsLinkItem);

            return await Task.FromResult(true);
        }

        public async Task<NewsLinkItem> GetItemAsync(string id)
        {
            return await Task.FromResult(NewsLinkItems.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<NewsLinkItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(NewsLinkItems);
        }
    }
}
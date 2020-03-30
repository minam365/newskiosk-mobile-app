using Inamsoft.Newskiosk.Abstractions;
using Inamsoft.Newskiosk.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inamsoft.Newskiosk.Data
{
    public class NewsSourceDataStore : IDataStore<NewsLinkItem>
    {
        readonly List<NewsLinkItem> items;

        #region --- Constructors ---

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsSourceDataStore"/> class.
        /// </summary>
        public NewsSourceDataStore()
        {
            items = new List<NewsLinkItem>
            {
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Express", Description="express.de", LinkUrl="https://mobil.express.de" },
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Kölner Stadt Anzeiger", Description="Kölner Stadt Anzeiger", LinkUrl="https://mobil.ksta.de" },
                new NewsLinkItem { Id = Guid.NewGuid().ToString(), Name = "Frankfurter Allgemeine Zeitung", Description="faz", LinkUrl="https://m.faz.net" }
            };
        }

        #endregion

        /// <summary>
        /// Adds new item to the data store.
        /// </summary>
        /// <param name="item">The data to add.</param>
        /// <returns>
        /// <code>True</code> if the <paramref name="item"/> is added; otherwise, <code>false</code>.
        /// </returns>
        public async Task<bool> AddItemAsync(NewsLinkItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(NewsLinkItem item)
        {
            var oldItem = items.Where((NewsLinkItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((NewsLinkItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<NewsLinkItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<NewsLinkItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}

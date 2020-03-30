using Inamsoft.Newskiosk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Inamsoft.Newskiosk.Services
{
    public class NewsSourcesFileDataStore : BaseDataStore, IDataStore<NewsLinkItem>
    {
        readonly string _dataFolderPath;
        readonly string _dataFileName;
        readonly string _dataFilePath;

        List<NewsLinkItem> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsSourcesFileDataStore"/> class.
        /// </summary>
        public NewsSourcesFileDataStore()
        {
            _dataFileName = "NewsSources.de.json";

            _dataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataFilePath = Path.Combine(_dataFolderPath, _dataFileName);
        }

        /// <summary>
        /// Adds new item to the data store.
        /// </summary>
        /// <param name="item">The data to add.</param>
        /// <returns>
        /// <code>True</code> if the <paramref name="item"/> is added; otherwise, <code>false</code>.
        /// </returns>
        public Task<bool> AddItemAsync(NewsLinkItem item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the item with <paramref name="id"/> from data store.
        /// </summary>
        /// <param name="id">The Id of the item to delete.</param>
        /// <returns></returns>
        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the item with <paramref name="id"/> from data store.
        /// </summary>
        /// <param name="id">The Id of the item to get.</param>
        /// <returns>
        /// The object of type <code><typeparamref name="T"/>.</code>
        /// </returns>
        public Task<NewsLinkItem> GetItemAsync(string id)
        {
            var item = _items?.Where(itm => itm.Id == id).SingleOrDefault();
            return Task.FromResult(item);
        }

        /// <summary>
        /// Gets all the items from data store.
        /// </summary>
        /// <param name="forceRefresh">The optional flag that determines whether items
        /// should be retrieved from data store or the cache.
        /// </param>
        /// <returns>A sequence of items.</returns>
        public async Task<IEnumerable<NewsLinkItem>> GetItemsAsync(bool forceRefresh = false)
        {
            await EnsureDataFileExistsAsync();

            if (forceRefresh || _items?.Any() == false)
            {
                using var fs = File.OpenRead(_dataFilePath);

                JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
                serializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.LatinExtendedE);
                _items = await JsonSerializer.DeserializeAsync<List<NewsLinkItem>>(fs);

            }

            return _items;
        }

        /// <summary>
        /// Updates an existing <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>
        /// <code>True</code> if the <paramref name="item"/> is updated; otherwise, <code>false</code>.
        /// </returns>
        public Task<bool> UpdateItemAsync(NewsLinkItem item)
        {
            throw new NotImplementedException();
        }

        #region Utility Methods

        async Task SaveItemsAsync(IEnumerable<NewsLinkItem> items)
        {
            using var fs = File.OpenWrite(_dataFilePath);
            await JsonSerializer.SerializeAsync<IEnumerable<NewsLinkItem>>(fs, items);
        }

        async ValueTask EnsureDataFileExistsAsync()
        {
            if (File.Exists(_dataFilePath))
            {
                return;
            }

            var assembly = IntrospectionExtensions.GetTypeInfo(GetType()).Assembly;
            using var resourceStream = assembly.GetManifestResourceStream("Inamsoft.Newskiosk.Data.NewsSources-de.json");
            using var sr = new StreamReader(resourceStream);
            using var sw = new StreamWriter(_dataFilePath, append: false, Encoding.UTF8);
            
            while (!sr.EndOfStream)
            {
                var line = await sr.ReadLineAsync();
                await sw.WriteLineAsync(line);
            }
            //await sw.FlushAsync();
        }

        #endregion
    }
}

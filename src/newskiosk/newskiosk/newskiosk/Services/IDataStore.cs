using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace newskiosk.Services
{
    public interface IDataStore<T>
    {
        /// <summary>
        /// Adds new item to the data store.
        /// </summary>
        /// <param name="item">The data to add.</param>
        /// <returns>
        /// <code>True</code> if the <paramref name="item"/> is added; otherwise, <code>false</code>.
        /// </returns>
        Task<bool> AddItemAsync(T item);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

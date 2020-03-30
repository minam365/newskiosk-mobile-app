using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inamsoft.Newskiosk.Services
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
        /// Updates an existing <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>
        /// <code>True</code> if the <paramref name="item"/> is updated; otherwise, <code>false</code>.
        /// </returns>
        Task<bool> UpdateItemAsync(T item);

        /// <summary>
        /// Deletes the item with <paramref name="id"/> from data store.
        /// </summary>
        /// <param name="id">The Id of the item to delete.</param>
        /// <returns></returns>
        Task<bool> DeleteItemAsync(string id);

        /// <summary>
        /// Gets the item with <paramref name="id"/> from data store.
        /// </summary>
        /// <param name="id">The Id of the item to get.</param>
        /// <returns>
        /// The object of type <code><typeparamref name="T"/>.</code>
        /// </returns>
        Task<T> GetItemAsync(string id);

        /// <summary>
        /// Gets all the items from data store.
        /// </summary>
        /// <param name="forceRefresh">The optional flag that determines whether items
        /// should be retrieved from data store or the cache.
        /// </param>
        /// <returns>A sequence of items.</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

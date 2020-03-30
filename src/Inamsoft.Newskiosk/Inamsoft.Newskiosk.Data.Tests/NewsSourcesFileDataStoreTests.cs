using Inamsoft.Newskiosk.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Inamsoft.Newskiosk.Data.Tests
{
    public class NewsSourcesFileDataStoreTests
    {
        [Fact]
        public async Task TestGetItemsFromResources()
        {
            var dataStore = new NewsSourcesFileDataStore();

            var items = await dataStore.GetItemsAsync(true);

            Assert.NotNull(items);
            Assert.NotEmpty(items);

        }
    }
}

﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Inamsoft.Newskiosk.Models;
using Inamsoft.Newskiosk.Views;
using Inamsoft.Newskiosk.Abstractions.Models;

namespace Inamsoft.Newskiosk.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<NewsLinkItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<NewsLinkItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, NewsLinkItem>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as NewsLinkItem;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
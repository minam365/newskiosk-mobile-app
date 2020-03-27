// Copyrigth (c) 2020 Inamsoft. All rights reserved.
//  Licensed under the GNU general public license, v3.

using System;
using System.Windows.Input;
using Inamsoft.Newskiosk.Models;
using Xamarin.Forms;

namespace Inamsoft.Newskiosk.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        ICommand _refreshCommand = null;

        public NewsLinkItem Item { get; set; }

        public ItemDetailViewModel(NewsLinkItem item = null)
        {
            Title = item?.Name;
            Item = item;
            LinkUrl = item.LinkUrl;
        }

        string linkUrl = string.Empty;
        public string LinkUrl
        {
            get { return linkUrl; }
            set { SetProperty(ref linkUrl, value); }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new Command(ExecuteRefreshCommand);
                }

                return _refreshCommand;
            }
        }

        public void ExecuteRefreshCommand()
        {
            IsBusy = true;

            OnPropertyChanged(nameof(LinkUrl));

            IsBusy = false;
        }
    }
}

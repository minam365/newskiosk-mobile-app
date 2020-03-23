using System;

using newskiosk.Models;

namespace newskiosk.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public NewsLinkItem Item { get; set; }
        public ItemDetailViewModel(NewsLinkItem item = null)
        {
            Title = item?.Name;
            Item = item;
            LinkUrl = item?.LinkUrl;
        }

        string linkUrl = string.Empty;
        public string LinkUrl
        {
            get { return linkUrl; }
            set { SetProperty(ref linkUrl, value); }
        }

    }
}

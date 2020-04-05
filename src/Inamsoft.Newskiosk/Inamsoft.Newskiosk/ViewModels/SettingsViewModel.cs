using System;
using System.Collections.Generic;
using System.Text;

namespace Inamsoft.Newskiosk.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel()
        {
            Title = "Settings";
        }


        string _sortItemsByName = string.Empty;

        public string SortItemsByName
        {
            get { return _sortItemsByName; }
            set { SetProperty(ref _sortItemsByName, value); }
        }


        string _openLinksInDefaultBrowser = string.Empty;

        public string OpenLinksInDefaultBrowser
        {
            get { return _openLinksInDefaultBrowser; }
            set { SetProperty(ref _openLinksInDefaultBrowser, value); }
        }
    }
}

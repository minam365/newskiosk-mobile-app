using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Inamsoft.Newskiosk.Services
{
    public class BaseDataStore
    {
        /// <summary>
        /// Determines whether the Internet access is available.
        /// </summary>
        protected bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

    }
}

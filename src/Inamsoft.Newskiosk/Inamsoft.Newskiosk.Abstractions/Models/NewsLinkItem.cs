using System;
using System.Collections.Generic;
using System.Text;

namespace Inamsoft.Newskiosk.Abstractions.Models
{
    public class NewsLinkItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LinkUrl { get; set; }

        public string MobileUrl { get; set; }

        public string Category { get; set; }

    }
}

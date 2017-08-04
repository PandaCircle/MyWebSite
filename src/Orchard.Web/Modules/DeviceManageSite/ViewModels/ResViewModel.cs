using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.ViewModels
{
    public class ResViewModel
    {
        public int ResId { get; set; }
        public string DisplayContent { get; set; }
        public int Using { get; set; }
        public string Catagories { get; set; }
        public bool IsChecked { get; set; }
    }
}
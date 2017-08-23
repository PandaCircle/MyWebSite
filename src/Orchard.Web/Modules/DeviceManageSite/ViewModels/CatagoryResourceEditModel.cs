using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.ViewModels
{
    public class CatagoryResourceEditModel
    {
        public IEnumerable<SimpleResViewModel> Classified { get; set; }
        public IEnumerable<SimpleResViewModel> Uncatalogued { get; set; }
    }

    public class SimpleResViewModel
    {
        public int ResId { get; set; }
        public string ResContent { get; set; }
    }
}
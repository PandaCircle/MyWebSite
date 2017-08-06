using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.ViewModels
{
    public class CatagoryEditViewModel
    {
        public IEnumerable<ResTypeModel> ResTypes { get; set; }
        public IEnumerable<CatagoryModel> Catagories { get; set; }
        public string CurrentType { get; set; }
    }


    public class ResTypeModel
    {
        public int TypeId { get; set; }
        public string DisplayName { get; set; }
    }

    public class CatagoryModel
    {
        public int CataId { get; set; }
        public string DisplayName { get; set; }
    }
}
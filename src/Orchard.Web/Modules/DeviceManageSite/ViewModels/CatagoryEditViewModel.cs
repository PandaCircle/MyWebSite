using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManageSite.ViewModels
{
    public class CatagoryEditViewModel
    {
        public CatagoryEditViewModel()
        {
            ResTypes = new List<ResTypeModel>();
        }
        public IList<ResTypeModel> ResTypes { get; set; }

    }


    public class ResTypeModel
    {
        public int TypeId { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<CatagoryModel> Catagories { get; set; }
    }

    public class CatagoryModel
    {
        public int CataId { get; set; }
        public string DisplayName { get; set; }
    }

}
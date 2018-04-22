using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Models
{
    public class Parameters
    {
        public static List<string> Departments
        {
            get
            {
                return new List<string> { "局长室","收入核算股","人事教育股","征收管理股","监察室","资料组","税源管理二股","政策法规股","信息中心","办公室","财务组","稽查局","税源管理三股","税源管理一股","纳税服务股","龙口","雅瑶","古劳","桃源","鹤城税务分局","共和","址山","宅梧税务分局"};
            }
        }
    }
}
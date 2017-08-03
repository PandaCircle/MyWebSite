using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;

namespace Department.Helper
{
    public class RegIp
    {
        public static bool TryGetIp(string bip,string eip,out List<string> ips)
        {
            ips = new List<string>();
            if (!CheckIp(bip)) return false;
            else
            if (!CheckIp(eip)) return false;
            else
            if (!(bip.ToTupleInt().LessThan(eip.ToTupleInt().NextIp()))) return false;
            else
            {
                var bipInt = bip.ToTupleInt();
                var eipInt = eip.ToTupleInt().NextIp();
                while (!bipInt.Equals(eipInt))
                {
                    if (bipInt.Item4 == 0) { bipInt = bipInt.NextIp(); continue; }
                    ips.Add(bipInt.ToIpString());
                    bipInt = bipInt.NextIp();
                }
            }
            
            return true;
        }

        public static bool CheckIp(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        } 

    }

    public static class RegExtension
    {
        public static Tuple<int, int, int, int> ToTupleInt(this string a)
        {
           // if (!RegIp.CheckIp(a)) throw new ArgumentException("format is invalid");
            var ipArray = a.Split('.');
            return new Tuple<int, int, int, int>(Convert.ToInt32(ipArray[0]), Convert.ToInt32(ipArray[1]), Convert.ToInt32(ipArray[2]), Convert.ToInt32(ipArray[3]));
        }

        public static string ToIpString(this Tuple<int, int, int, int> a)
        {
            return a.Item1.ToString() + "." + a.Item2.ToString() + "." + a.Item3.ToString() + "." + a.Item4.ToString();
        }

        public static Tuple<int, int, int, int> NextIp(this Tuple<int, int, int, int> a)
        {
            int ipInt32 = (a.Item1 << 24) + (a.Item2 << 16) + (a.Item3 << 8) + a.Item4;
            ipInt32++;
            return new Tuple<int, int, int, int>(ipInt32 >> 24 & 0xff, ipInt32 >> 16 & 0xff, ipInt32 >> 8 & 0xff, ipInt32 & 0xff);
        }

        public static bool Equals(this Tuple<int,int,int,int> a ,Tuple<int,int,int,int> b)
        {
            return a.Item1 == b.Item1 && a.Item2 == b.Item2 && a.Item3 == b.Item3 && a.Item4 == b.Item4;
        }

        public static bool LessThan(this Tuple<int,int,int,int> a,Tuple<int,int,int,int> b)
        {
            int ipInt32  = (a.Item1 << 24) + (a.Item2 << 16) + (a.Item3 << 8) + a.Item4;
            int ipInt32Compare = (b.Item1 << 24) + (b.Item2 << 16) + (b.Item3 << 8) + b.Item4;
            return ipInt32 < ipInt32Compare;
        }

        public static bool GraterThan(this Tuple<int,int,int,int> a,Tuple<int,int,int,int> b)
        {
            int ipInt32 = (a.Item1 << 24) + (a.Item2 << 16) + (a.Item3 << 8) + a.Item4;
            int ipInt32Compare = (b.Item1 << 24) + (b.Item2 << 16) + (b.Item3 << 8) + b.Item4;
            return ipInt32 > ipInt32Compare;
        }

    }
}

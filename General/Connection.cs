using System;
using System.Configuration;

namespace PcPoint
{
    public class Connection
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["pc_point_connection"].ConnectionString;
        }
    }
}

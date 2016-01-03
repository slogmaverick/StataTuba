using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.Common
{
    public static class Vars
    {


        /// <summary>
        /// Quick and easy access to various folders on the drive.
        /// </summary>
        public static class Paths
        {
            private static string rootDrive = @"d:\";
            public static string Temp = Path.Combine(rootDrive, "temp");

            //Trading Directories
            public static string Trading = Path.Combine(rootDrive, @"OneDrive\Data\Trading");
            public static string YahooFinance = Path.Combine(Trading, @"Raw\YahooFinance\");


            public static void InitPaths()
            {
                if (!Directory.Exists(Temp)) Directory.CreateDirectory(Temp);
                if (!Directory.Exists(Trading)) Directory.CreateDirectory(Trading);
                if (!Directory.Exists(YahooFinance)) Directory.CreateDirectory(YahooFinance);
            }

        }
    }
}

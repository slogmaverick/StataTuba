using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StataTuba.Common;

namespace StataTuba.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Create any directories that I forgot to.
            Vars.Paths.InitPaths();
            StartHere();
        }

        private static void StartHere()
        {
            Trading.StartHere();
        }

    }
}

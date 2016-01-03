using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StataTuba.Common
{
    public static class StringFormats
    {
        public static string BuildDelimiter(string delimiter, int count, bool withNewLine = true)
        {
            var sb = new StringBuilder();
            for (var x = 0; x < count; x++)
            {
                sb.Append("{" + x.ToString() + "}" + delimiter);
            }
            sb.Remove(sb.Length - 1, 1);
            if (withNewLine) sb.Append(Environment.NewLine);
            return sb.ToString();
        }
        public const string TabFmt1 = "{0}";
        public const string TabFmt2 = "{0}\t{1}";
        public const string TabFmt3 = "{0}\t{1}\t{2}";
        public const string TabFmt4 = "{0}\t{1}\t{2}\t{3}";
        public const string TabFmt5 = "{0}\t{1}\t{2}\t{3}\t{4}";
        public const string TabFmt6 = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}";
        public const string TabFmt9 = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}";

        public const string TableRow5 = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>";
        public const string TableRow6 = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>";
        public const string TableRow7 = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>";
        public const string TableRow8 = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>";
        public const string TableRow9 = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>";
        public const string TableRow10 = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td></tr>";


        public const string TableCell5 = "<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>";
        public const string TableCell6 = "<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td>";
        public const string TableCell7 = "<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td>";
        public const string TableCell8 = "<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td>";
        public const string TableCell9 = "<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td>";
        public const string TableCell10 = "<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td>";
    }
}

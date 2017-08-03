using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Server
{
    internal static class Utils
    {
        public static string GetReportPath(this ReportInfo report)
        {
            DateTime dtNow = DateTime.Now;
            string path = System.IO.Path.Combine(ReportServer.ReportPath, string.Format("{0:D4}\\{1:D2}\\{2:D2}\\{3}\\{4}", dtNow.Year, dtNow.Month, dtNow.Day, report.CallingIP, report.Id));
            return path;
        }
    }
}

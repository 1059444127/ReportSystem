using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Client
{
    internal static class Utils
    {
        internal static bool NeedConfirm(this ReportInfo report)
        {
            if (report.Status == ReportStatus.ConfirmExistReport)
                return true;
            else if (report.Status == ReportStatus.ErrorGetIdFail)
                return true;
            else if (report.Status == ReportStatus.ConfirmPatientId && ReportClient.ConfirmPatientId)
                return true;

            return false;
        }
    }
}

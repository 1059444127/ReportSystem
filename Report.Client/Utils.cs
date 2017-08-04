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
            else if (report.Status == ReportStatus.ConfirmPatientId && ReportClient.NeedConfirmPatientId)
                return true;

            return false;
        }

        internal static void CopyFrom(this ReportInfo self, ReportInfo target)
        {
            //self.Id = target.Id;
            self.PatientId = target.PatientId;
            self.CallingIP = target.CallingIP;
            self.PatientName = target.PatientName;
            self.PdfReportExist = target.PdfReportExist;
            //self.PdfReport = target.PdfReport;
            self.Status = target.Status;
            self.ExistReportAction = target.ExistReportAction;
            self.ErrorMessage = target.ErrorMessage;
        }
    }
}

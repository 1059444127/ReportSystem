using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Client
{
    internal static class Utils
    {
        private static LogManager _logMgr = new LogManager();

        internal static bool NeedConfirm(this ReportInfo report)
        {
            if (report.Status == ReportStatus.ConfirmExistReport)
                return true;
            else if (report.Status == ReportStatus.FailedGetPatientId)
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

        internal static bool HasError(this ReportInfo report)
        {
            return report.Status == ReportStatus.Error;
        }

        internal static bool IsServerDone(this ReportInfo report)
        {
            return report.Status == ReportStatus.ConfirmOK;
        }

        internal static LogManager LogMgr
        {
            get { return _logMgr; }
        }

        internal static void Log(string format, params object[] values)
        {
            LogMgr.Log(format, values);
        }

        internal static void Error(string format, params object[] values)
        {
            LogMgr.Error(format, values);
        }
    }
}

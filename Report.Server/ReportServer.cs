using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Server
{
    public enum GetPatientIDMethod
    {
        ByText = 1,
        ByImage = 2
    }

    public class ReportServer
    {
        //listen port
        private static int _port = 11121;
        private static string _reportPath = @"D:\ReportFolder";
        private static GetPatientIDMethod _parseIdMethod = GetPatientIDMethod.ByText;

        public static int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public static string ReportPath
        {
            get { return _reportPath; }
            set { _reportPath = value; }
        }

        public static GetPatientIDMethod GetPatientIdMethod
        {
            get { return _parseIdMethod; }
            set { _parseIdMethod = value; }
        }

        public static void Start()
        {

        }

        internal static void SendReport(ReportInfo report)
        {

        }

        internal static void HandleReport(ReportInfo report)
        {
            if(report.Status == ReportStatus.SubmitInitial)
            {
                //save report
                SaveReport(report);

                string patientId = ParsePatientId(report);
                report.PatientId = patientId;
                if(string.IsNullOrEmpty(patientId))
                {
                    report.Status = ReportStatus.ErrorGetIdFail;
                    SendReport(report);
                    return;
                }

                //check exist report
                byte[] existReport = GetExistReport(patientId);
                if(existReport != null)
                {
                    report.PdfReportExist = existReport;
                    report.Status = ReportStatus.ConfirmExistReport;

                    SendReport(report);
                    return;
                }

                //all is OK, let client confirm whether the patientId is correct.
                report.Status = ReportStatus.ConfirmPatientId;
                SendReport(report);
            }
            else if(report.Status == ReportStatus.SubmitNewPatientId)
            {
                byte[] existReport = GetExistReport(report.PatientId);
                if (existReport != null)
                {
                    report.PdfReportExist = existReport;
                    report.Status = ReportStatus.ConfirmExistReport;

                    SendReport(report);
                    return;
                }

                report.Status = ReportStatus.ConfirmOK;
                //TODO: notify main program


                SendReport(report);
            }
            else if(report.Status == ReportStatus.SubmitOK)
            {
                //notify main program
                report.Status = ReportStatus.ConfirmOK;
                //TODO: notify main program


                SendReport(report);
            }
        }

        private static void SaveReport(ReportInfo report)
        {
            //save pdf to report folder

            //save txt and jpg file.

        }

        private static string ParsePatientId(ReportInfo report)
        {
            return string.Empty;
        }

        private static byte[] GetExistReport(string patientId)
        {
            return null;
        }
    }
}

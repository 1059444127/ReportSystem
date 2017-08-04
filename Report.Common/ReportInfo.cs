using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public enum ReportStatus
    {
        Unknown = 0,
        SubmitInitial = 10,             //client first submit the report to server
        SubmitNewPatientId = 11,        //client changed patientid and submit again    
        SubmitExistReportAction = 12,   //client confirmed exist report action and submit
        SubmitOK = 13,                  //client confirmed all ok and submit
        ConfirmPatientId = 20,          //server let client confirm patientid
        ConfirmExistReport = 21,        //server let client confirm how to handle exist report
        ConfirmOK = 22,                 //server finish the report and tell client all is ok
        FailedGetPatientId = 30,        //server has failed to get patientid
        Error  = 51                     //server get other error
    }

    public enum ExistReportAction
    {
        Unknown = 0,
        Overwrite = 1,
        Append = 2,
        Abandon = 3
    }

    [Serializable]
    public class ReportInfo
    {
        private ReportStatus _status = ReportStatus.Unknown;
        private string _patientId;
        private string _callingIP;
        private string _patientName;
        private byte[] _pdfReport;
        private byte[] _pdfReportExist;
        private ExistReportAction _existReportAction = ExistReportAction.Unknown;
        private string _errorMessage;

        public string Id
        {
            get;
            private set;
        }

        public ReportStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }

        public string CallingIP
        {
            get { return _callingIP; }
            set { _callingIP = value; }
        }

        public byte[] PdfReport
        {
            get { return _pdfReport; }
            set { _pdfReport = value; }
        }

        public byte[] PdfReportExist
        {
            get { return _pdfReportExist; }
            set { _pdfReportExist = value; }
        }

        public ExistReportAction ExistReportAction
        {
            get { return _existReportAction; }
            set { _existReportAction = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public ReportInfo()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Status = ReportStatus.SubmitInitial;
        }
    }
}

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoServer
{
    public class ReportStruct
    {
        public string PatientId { get; set; }
        public string CallingIP { get; set; }
        public string ReportPath { get; set; }
        public bool IsOverwrite { get; set; }
    }
}

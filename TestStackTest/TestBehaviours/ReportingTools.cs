using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStackTest.Reporting {
    public class ReportingTools {
        
        private List<List<string>> _report = new List<List<string>>();
        private const string _fileLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\Report_Results";
        public ReportingTools(string reportName) {
            List<string> header = new List<string>();            
            header.Add(reportName);
            header.Add(getNow().ToString());
            addReportItem(header);
        }
        
        public DateTime getNow() {
            return DateTime.Now;
        }

        public List<List<string>> getReport() {
            return _report;
        }
        public void createReportItem(string title) {
            DateTime end = getNow();
            List<string> item = new List<string>();
            item.Add(title);
            _report.Add(item);
        }


        public void createReportItem(string title, DateTime start) {
            DateTime end = getNow();
            List<string> item = new List<string>();
            item.Add(title);
            item.Add(start.ToLongTimeString());
            item.Add(end.ToLongTimeString());
            item.Add(getTimeDelta(start, end).ToString());
            item.Add("PASS");
            _report.Add(item);
        }

        public void createReportItem(string title, DateTime start, string failureReason) {
            DateTime end = getNow();
            List<string> item = new List<string>();
            item.Add(title);
            item.Add(start.ToString());
            item.Add(end.ToString());
            item.Add(getTimeDelta(start,end).ToString());
            item.Add("FAIL");
            item.Add(failureReason);
            _report.Add(item);
        }



        
        public void addReportItem(List<string>item) {
            _report.Add(item);
        }
        
        public TimeSpan getTimeDelta (DateTime start, DateTime end) {
            return (end - start);
        }

        public string getFileLocation() {
            return _fileLocation;
        }
        
    }



}

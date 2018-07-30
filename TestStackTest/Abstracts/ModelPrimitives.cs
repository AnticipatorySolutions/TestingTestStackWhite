using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStackTest.Abstracts {
    public class ModelBase : IReport {
        public Dictionary<string, string> mandatoryTextBoxes = new Dictionary<string, string>();
        public Dictionary<string, string> mandatoryComboBoxes = new Dictionary<string, string>();
        public Dictionary<string, string> mandatoryDatePickers = new Dictionary<string, string>();

        public Dictionary<string, string> optionalTextBoxes = new Dictionary<string, string>();
        public Dictionary<string, string> optionalComboBoxes = new Dictionary<string, string>();
        public Dictionary<string, string> optionalDatePickers = new Dictionary<string, string>();
        
        List<string> _reportDetails = new List<string>();

        public void establishDictionary(string[] keys, string[] values, Dictionary<string, string> dictionary) {
            int track = 0;
            foreach (string x in keys) {
                dictionary.Add(x, values[track]);
                track++;
            }
        }

        public List<string> reportDetails {
            get {   return _reportDetails; }

            set {   _reportDetails.Concat(value); }
        }

        public virtual void SetUp(List<string> info, Dictionary<string, int> headers) {
        }

        public void Report() {
            foreach (string report in _reportDetails) {
                Console.WriteLine(report);
            }
        }
    }
}

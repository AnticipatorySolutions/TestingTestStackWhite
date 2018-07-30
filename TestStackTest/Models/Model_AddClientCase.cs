using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStackTest.Abstracts;


namespace TestStackTest.Models{
    public class Model_AddClientCase : ModelBase {
        public Dictionary<string, string> conditionalTreatmentTextBoxes = new Dictionary<string, string>();
        public Dictionary<string, string> conditionalTreatmentComboBoxes = new Dictionary<string, string>();

        public Dictionary<string, string> conditionalBillingTextBoxes = new Dictionary<string, string>();
        public Dictionary<string, string> conditionalBillingComboBoxes = new Dictionary<string, string>();
        
        public Model_AddClientCase(List<string> info, Dictionary<string, int> headers) {
            SetUp(info, headers);
        }

        public override void SetUp(List<string> info, Dictionary<string, int> headers) {
            setMandatoryTextBoxes(info, headers);
            setMandatoryComboBoxes(info, headers);
            setMandatoryDatePickers(info, headers);
            setOptionalTextBoxes(info, headers);
            setOptionalComboBoxes(info, headers);
            setOptionalDatePickers(info, headers);
            setConditionalTreatmentTextBoxes(info, headers);
            setConditionalBillingTextBoxes(info, headers);
        }


        #region TextBoxes
        public void setMandatoryTextBoxes(List<string> info, Dictionary<string, int> headers) {
            string[] keys = new string[] { "firstName", "lastName", "line1TextBox", "postalTextBox" };
            string[] values = new string[] {
                info[headers["FirstName"]],
                info[headers["LastName"]],
                info[headers["Address1"]],
                info[headers["Postal"]]
            };

            establishDictionary(keys, values, mandatoryTextBoxes);
        }

        public void setOptionalTextBoxes(List<string> info, Dictionary<string, int> headers) {
            string[] keys = new string[] {
                "invoiceNotes",
                "contactNotes",
                "accessInstructions",
                "specialInstructions",
                "buildingTextBox",
                "roomNumberTextBox",
                "cityTextBox",
                "line2TextBox",
                "phoneHomeExtension",
                "phoneWorkExtension",
                "preferredNameTextBox",
                "healthNumber",
                "email",
                "phoneCell",
                "phoneWork",
                "phoneHome"
            };

            string[] values = new string[] {
                info[headers["Invoice Notes"]],
                info[headers["Contact Notes"]],
                info[headers["Access Instructions"]],
                info[headers["Special Instructions"]],
                info[headers["Building Name"]],
                info[headers["Room Number"]],
                info[headers["City"]],
                info[headers["Address2"]],
                info[headers["HomeExt"]],
                info[headers["WorkExt"]],
                info[headers["Preferred Name"]],
                info[headers["HealthNumber"]],
                info[headers["Email"]],
                info[headers["Cell"]],
                info[headers["WorkPhone"]],
                info[headers["HomePhone"]]
            };
            establishDictionary(keys, values, optionalTextBoxes);
        }

        public void setConditionalTreatmentTextBoxes(List<string> info, Dictionary<string, int> headers) {
            string[] keys = new string[] { "line1TextBox", "postalTextBox", "line2TextBox", "cityTextBox"};
            string[] values = new string[] {
                info[headers["TreatmentAddress1"]],
                info[headers["TreatmentPostal"]],
                info[headers["TreatmentAddress2"]],
                info[headers["TreatmentCity"]]
            };

            establishDictionary(keys, values, conditionalTreatmentTextBoxes);
        }

        public void setConditionalBillingTextBoxes(List<string> info, Dictionary<string, int> headers) {
            string[] keys = new string[] { "line1TextBox", "postalTextBox", "line2TextBox", "cityTextBox" };
            string[] values = new string[] {
                info[headers["BillingAddress1"]],
                info[headers["BillingPostal"]],
                info[headers["BillingAddress2"]],
                info[headers["BillingCity"]]
            };

            establishDictionary(keys, values, conditionalBillingTextBoxes);
        }
        #endregion

        #region comboBoxes
        public void setMandatoryComboBoxes(List<string> info, Dictionary<string, int> headers) {
            string[] keys = new string[] { "gender", "caseDescription", "referralSources"};
            string[] values = new string[] {
                info[headers["Gender"]],
                info[headers["Case Description"]],
                info[headers["Referral Source"]],                
            };
            establishDictionary(keys, values, mandatoryComboBoxes);
        }

        public void setOptionalComboBoxes(List<string> info, Dictionary<string, int> headers) {           
            string[] keys = new string[] {
                "contacts",
                "careTeams",
                "title",
                "provinceComboBox",
                "holidaySetting",
                "contracts",
                "programs",
                "seeByType",
                "overrideApplyTax",
                "status"
            };
            
            string[] values = new string[] {
                info[headers["Contact"]],
                info[headers["Care Team"]],
                info[headers["Title"]],
                info[headers["Province"]],
                info[headers["Holiday Setting"]],
                info[headers["Contract"]],
                info[headers["Program"]],                
                info[headers["DateType"]],
                info[headers["Override apply tax"]],
                info[headers["Status"]]                
            };
            establishDictionary(keys, values, optionalComboBoxes);
        }
        #endregion

        #region datePickers
        public void setMandatoryDatePickers(List<string> info, Dictionary<string, int> headers) {
            string[] keys = new string[] { "dateOfBirth", "endDate", "seeByDate" };
            string[] values = new string[] {
                info[headers["DOB"]],
                info[headers["EndDate"]],
                info[headers["Date Type Value"]],
            };
            establishDictionary(keys, values, mandatoryDatePickers);
        }

        public void setOptionalDatePickers(List<string> info, Dictionary<string, int> headers) {
            string[] keys = new string[] { "startDate"};
            string[] values = new string[] {
                info[headers["Start Date"]]
            };
            establishDictionary(keys, values, optionalDatePickers);
        }
        #endregion
        
    }    
}


using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;

namespace TestStackTest.Views {
    public class SelectFacility : TestViewBase {
        enum controlNames { click_centralOffice_radioButton, click_systemAdmin_radioButton, click_facility_radioButton, click_ok_button, click_divisions_comboBox_criteria, click_listView_listView_criteria }
        enum itemNames {centralOfficeRadioButton, systemAdminRadioButton, facilityRadioButton, okButton, divisions, listView}
        

        public SelectFacility(Application app, ITestCommands TestCommands, string facilityName) {
            SetUp(app, WindowNames.SelectFacility, facilityName);
            foreach (string command in TestCommands.Commands) {
                if (command ==  nameof(controlNames.click_listView_listView_criteria)) {
                    //state of listview changes after divisions combo box is updated
                    controls[nameof(controlNames.click_listView_listView_criteria)] = controlFactory.Assign(window.Get<ListView>(SearchCriteria.ByAutomationId(itemNames.listView.ToString())), ControlType.listView, ActionType.click, facilityName);
                }
                controls[command].Action();
            }            
        }
        public void SetUp(Application app, WindowNames windowName, string facilityName) {
            base.SetUp(app, windowName);         
//          controls.Add(nameof(controlNames.click_centralOffice_radioButton), controlFactory.Assign(window.Get<RadioButton>(SearchCriteria.ByAutomationId(nameof(itemNames.centralOfficeRadioButton))), ControlType.radioButton, ActionType.click));
            controls.Add(nameof(controlNames.click_systemAdmin_radioButton), controlFactory.Assign(window.Get<RadioButton>(SearchCriteria.ByAutomationId(nameof(itemNames.systemAdminRadioButton))), ControlType.radioButton, ActionType.click));
            controls.Add(nameof(controlNames.click_facility_radioButton), controlFactory.Assign(window.Get<RadioButton>(SearchCriteria.ByAutomationId(nameof(itemNames.facilityRadioButton))), ControlType.radioButton, ActionType.click));
            controls.Add(nameof(controlNames.click_ok_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.okButton))), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.click_divisions_comboBox_criteria), controlFactory.Assign(window.Get<ComboBox>(SearchCriteria.ByAutomationId(nameof(itemNames.divisions))), ControlType.winFormComboBox, ActionType.click, "Any"));
            controls.Add(nameof(controlNames.click_listView_listView_criteria), controlFactory.Assign(window.Get<ListView>(SearchCriteria.ByAutomationId(nameof(itemNames.listView))), ControlType.listView, ActionType.click, facilityName));        
        }        
    }
}

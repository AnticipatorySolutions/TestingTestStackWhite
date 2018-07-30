using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;

namespace TestStackTest.Views {
    public class EditCarePlan_Revision : TestViewBase {
        enum controlNames { click_ok_button, write_endDate_datePicker }
        enum itemNames { okButton, endDate }

        public EditCarePlan_Revision(Application app, ITestCommands TestCommands, string criteria = "") {
            SetUp(app, WindowNames.EditCarePlanRevision, criteria);
            foreach (string command in TestCommands.Commands) {
                controls[command].Action();
            }
        }

        public void SetUp(Application app, WindowNames windowName, string criteria) {
            base.SetUp(app, windowName);

            controls.Add(nameof(controlNames.click_ok_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.okButton))), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.write_endDate_datePicker), controlFactory.Assign(windowTools.GetDateTimePicker(window, SearchCriteria.ByAutomationId("endDate")), ControlType.dateTimePicker, ActionType.write, criteria));

            //            controls.Add(nameof(controlNames.), controlFactory.Assign(,ControlType,ActionType));            
        }


    }
}

using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;

namespace TestStackTest.Views {
    public class SelectVisitCompletionTypeTime : TestViewBase {
        enum controlNames { click_ok_button, click_cancel_button, write_note_textBox, click_type_comboBox, click_reason_comboBox}
        enum itemNames { okButton, cancelButton, completionNote, visitType, visitCompletionType}

        public SelectVisitCompletionTypeTime(Application app, ITestCommands TestCommands, VisitCompletionTypes type, VisitCompletionReasons reason, string note) {
            
            SetUp(app, WindowNames.SelectVisitCompletionTypeTime, type.ToString(), reason.ToString(), note);
            foreach (string command in TestCommands.Commands) {
                if (command == nameof(controlNames.click_type_comboBox)) 
                    { controls[nameof(controlNames.click_type_comboBox)] = controlFactory.Assign(window.Get<ComboBox>(SearchCriteria.ByAutomationId(nameof(itemNames.visitType))), ControlType.winFormComboBox, ActionType.click, type.ToString()); }

                //values of comboBox are updated when the value of the type comboBox are reset
                else if (command == nameof(controlNames.click_reason_comboBox)) 
                    { controls[nameof(controlNames.click_reason_comboBox)] = controlFactory.Assign(window.Get<ComboBox>(SearchCriteria.ByAutomationId(nameof(itemNames.visitCompletionType))), ControlType.winFormComboBox, ActionType.click, reason.ToString()); }

                    controls[command].Action();
            }
        }

        public void SetUp(Application app, WindowNames windowName, string type, string reason, string note) {
            base.SetUp(app, windowName);

            controls.Add(nameof(controlNames.click_ok_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.okButton))), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.click_cancel_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.cancelButton))), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.write_note_textBox), controlFactory.Assign(window.Get<TextBox>(SearchCriteria.ByAutomationId(nameof(itemNames.completionNote))), ControlType.textBox, ActionType.write, note));
            controls.Add(nameof(controlNames.click_type_comboBox), controlFactory.Assign(window.Get<ComboBox>(SearchCriteria.ByAutomationId(nameof(itemNames.visitType))), ControlType.winFormComboBox, ActionType.click, type));
            controls.Add(nameof(controlNames.click_reason_comboBox), controlFactory.Assign(window.Get<ComboBox>(SearchCriteria.ByAutomationId(nameof(itemNames.visitCompletionType))), ControlType.winFormComboBox, ActionType.click, reason));


            //controls.Add(nameof(controlNames.write_endDate_datePicker), controlFactory.Assign(windowTools.GetDateTimePicker(window, SearchCriteria.ByAutomationId("endDate")), ControlType.dateTimePicker, ActionType.write, criteria));

            //            controls.Add(nameof(controlNames.), controlFactory.Assign(,ControlType,ActionType));            
        }


    }
}
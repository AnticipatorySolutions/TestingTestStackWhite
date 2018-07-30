using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;

namespace TestStackTest.Views {
    public class ClientCaseSearch : TestViewBase {
        enum controlNames { write_criteria_winFormTextBox, click_lastName_button, click_newClient_button, click_ok_button }
        enum itemNames { criteria, lastNameButton, newClientButton, okButton }
        
        public ClientCaseSearch(Application app, ITestCommands TestCommands, string searchText ="") {
            SetUp(app, WindowNames.SearchClientCases, searchText);
            foreach (string command in TestCommands.Commands) {
                controls[command].Action();
            }
        }

        public void SetUp(Application app, WindowNames windowName, string searchText) {
            base.SetUp(app, windowName);
            controls.Add(nameof(controlNames.write_criteria_winFormTextBox), controlFactory.Assign(window.Get<WinFormTextBox>(SearchCriteria.ByAutomationId(nameof(itemNames.criteria))), ControlType.winFormTextBox, ActionType.write, searchText));
            controls.Add(nameof(controlNames.click_lastName_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.lastNameButton))), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.click_newClient_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.newClientButton))), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.click_ok_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.okButton))), ControlType.button, ActionType.click));


            //            controls.Add(nameof(controlNames.), controlFactory.Assign(,ControlType,ActionType));            
        }
        

    }
}

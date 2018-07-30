using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems;

namespace TestStackTest.Views {
    public class Desktop : TestViewBase {

        enum controlNames { click_clients_button, click_tabControl_tabPage}

        enum itemNames { Clients }
        public Desktop(Application app, ITestCommands TestCommands, int WindowNumber, string tabName = "") {
            SetUp(app, WindowNumber, TestCommands, tabName);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();
            }         
        }
        public void SetUp(Application app, int windowNumber, ITestCommands commands, string tabName) {
            base.SetUp(app, windowNumber);
            
            IUIItem tab = windowTools.GetIUIItemList<Tab>(window)[0];
            controls.Add(nameof(controlNames.click_clients_button), controlFactory.Assign(windowTools.GetButton(window, nameof(itemNames.Clients)), ControlType.button , ActionType.click));
            controls.Add(nameof(controlNames.click_tabControl_tabPage), controlFactory.Assign(tab, ControlType.tab, ActionType.click, tabName));
            
                      
            /* TODO: add other buttons
             
Drop Down Button
Drop Down Button
Toggle Care/Coordination Team
Exit
Message
<done> Clients 
Providers
Organizations
Shift Schedules
Internal Schedule
Import PXML
Font
Minimize
Maximize
Close 
             */
             
        }
    }
}


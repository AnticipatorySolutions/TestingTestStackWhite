using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems;

namespace TestStackTest.Views {
    public class ClientNoteBook : TestViewBase {

        enum controlNames { click_tabControl_tabPage }

        enum itemNames { Clients }
        public ClientNoteBook(Application app, ITestCommands TestCommands, int WindowNumber, string tabName = "") {
            SetUp(app, WindowNumber, TestCommands, tabName);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();
            }
        }
        public void SetUp(Application app, int windowNumber, ITestCommands commands, string tabName) {
            base.SetUp(app, windowNumber);

            IUIItem tab = windowTools.GetIUIItemList<Tab>(window)[1];
            controls.Add(nameof(controlNames.click_tabControl_tabPage), controlFactory.Assign(tab, ControlType.tab, ActionType.click, tabName));
            
        }
    }

}

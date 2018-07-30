using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;

namespace TestStackTest.Views {
    public class MessageBox : TestViewBase {
        enum controlNames {doubleClick_ok_button }
        
        public MessageBox(Application app, ITestCommands TestCommands) {
            SetUp(app, WindowNames.CBI, TestCommands);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();
            }
        }
        public void SetUp(Application app, WindowNames windowName, ITestCommands commands) {
            base.SetUp(app, windowName);
            
            controls.Add(nameof(controlNames.doubleClick_ok_button), controlFactory.Assign(window.Items[0], ControlType.button, ActionType.doubleClick));
        }
    }
}

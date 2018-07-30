using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;

namespace TestStackTest.Views {
    public class Login : TestViewBase  {
        #region credentials
        private const string username = "jorussell";
        private const string password = "CountZero25";
        #endregion
        enum controlNames {write_username_textBox, write_password_textBox, doubleClick_ok_button}
        enum itemNames { username, password, okButton}
        public Login(Application app,  ITestCommands TestCommands) {
            SetUp(app, WindowNames.Login, TestCommands);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();
            }                        
        }        
        public void SetUp(Application app, WindowNames windowName, ITestCommands commands) {
            base.SetUp(app, windowName);
            controls.Add(nameof(controlNames.write_username_textBox), 
                controlFactory.Assign(window.Get<WinFormTextBox>(SearchCriteria.ByAutomationId(nameof(itemNames.username))), ControlType.winFormTextBox, ActionType.write, username));
            controls.Add(nameof(controlNames.write_password_textBox), 
                controlFactory.Assign(window.Get<WinFormTextBox>(SearchCriteria.ByAutomationId(nameof(itemNames.password))), ControlType.winFormTextBox, ActionType.write, password));
            controls.Add(nameof(controlNames.doubleClick_ok_button), 
                controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId(nameof(itemNames.okButton))), ControlType.button, ActionType.doubleClick));            
        }        
    }
}

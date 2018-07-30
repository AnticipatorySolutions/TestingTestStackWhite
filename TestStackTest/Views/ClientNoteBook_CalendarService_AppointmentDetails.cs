using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace TestStackTest.Views {
    public class ClientNoteBook_CalendarService_AppointmentDetails : TestViewBase {

        enum controlNames { click_visitCompletion_button, click_close_button}

        enum itemNames {  }
        public ClientNoteBook_CalendarService_AppointmentDetails(Application app, ITestCommands TestCommands) {
            SetUp(app, WindowNames.AppointmentDetails);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();
            }
        }
        public override void SetUp(Application app, WindowNames windowName) {
            base.SetUp(app, windowName);

            //id of visit completion button is not deterministic, but it has a distict borderColor.... not the best solution
            controls.Add(nameof(controlNames.click_visitCompletion_button), controlFactory.Assign(windowTools.GetButton(window, System.Drawing.Color.FromArgb(255,255,255,255)), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.click_close_button), controlFactory.Assign(window.Get<Button>(SearchCriteria.ByAutomationId("cancelButton")), ControlType.button, ActionType.click));

            /*TODO: Add more button controls

            */
        }
    }

}

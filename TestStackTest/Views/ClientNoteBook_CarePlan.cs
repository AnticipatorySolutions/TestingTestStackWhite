using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems;

namespace TestStackTest.Views {
    public class ClientNoteBook_CarePlan : TestViewBase {

        enum controlNames { click_editCarePlan_button, click_fillAssignments_button }

        enum itemNames { EditCarePlan, FillAssignments }
        public ClientNoteBook_CarePlan(Application app, ITestCommands TestCommands, int WindowNumber) {
            SetUp(app, WindowNumber, TestCommands);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();
            }
        }
        public void SetUp(Application app, int windowNumber, ITestCommands commands) {
            base.SetUp(app, windowNumber);

            controls.Add(nameof(controlNames.click_editCarePlan_button), controlFactory.Assign(windowTools.GetButton(window, nameof(itemNames.EditCarePlan)), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.click_fillAssignments_button), controlFactory.Assign(windowTools.GetButton(window, nameof(itemNames.FillAssignments)), ControlType.button, ActionType.click));


            /*TODO: Add more button controls
            Add Care Plan
        Edit Care Plan
Merge Care Plan
Edit Status
Fill Assignments
treatmentPlanButton
Equipment Orders

            */
        }
    }

}

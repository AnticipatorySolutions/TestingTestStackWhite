using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;

namespace TestStackTest.Views {
    public class EditCarePlan : TestViewBase {
        enum controlNames {click_editRevision_button}
        enum itemNames {EditRevision}
        public EditCarePlan(Application app, ITestCommands TestCommands, int WindowNumber) {
            SetUp(app, WindowNumber, TestCommands);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();
            }
        }
        public void SetUp(Application app, int windowNumber, ITestCommands commands) {
            base.SetUp(app, windowNumber);
            controls.Add(nameof(controlNames.click_editRevision_button), controlFactory.Assign(windowTools.GetButton(window,nameof(itemNames.EditRevision)), ControlType.winFormTextBox, ActionType.click));


        }
    }


    /* TODO: ADD MORE CONTROLS   
    History
    All
    None
    Maxim.
    Add Note
    Edit Note
    Schedule
    Edit Time / Duration
    Manage Classes (dropdown)

    Invert DAys
    Reset
    Delete
    Apply
    Save

    establish method of handling gdi+ control treeview
    handle all the other controls.... lots to do... later


    */
}

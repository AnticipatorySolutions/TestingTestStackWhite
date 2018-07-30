using System;
using TestStackTest.Abstracts;
using TestStack.White;
using TestStack.White.UIItems;
using System.Windows;
using TestStack.White.UIItems.Finders;

namespace TestStackTest.Views {
    public class ClientNoteBook_CalendarService : TestViewBase {

        enum controlNames { click_showOneDay_button, doubleClick_point }

        enum itemNames { ShowOneDay}
        public ClientNoteBook_CalendarService(Application app, ITestCommands TestCommands, int WindowNumber, Point? point = null ) {
            SetUp(app, WindowNumber, TestCommands, point);
            foreach (string test in TestCommands.Commands) {
                controls[test].Action();                
            }
        }
        public void SetUp(Application app, int windowNumber, ITestCommands commands, Point? point) {
            base.SetUp(app, windowNumber);                    
            controls.Add(nameof(controlNames.click_showOneDay_button), controlFactory.Assign(windowTools.GetButton(window, nameof(itemNames.ShowOneDay)), ControlType.button, ActionType.click));
            controls.Add(nameof(controlNames.doubleClick_point),controlFactory.Produce(ControlType.point, ActionType.doubleClick));
            setUpPointControl(controls[nameof(controlNames.doubleClick_point)], point);            
        }


        private void setUpPointControl(IControl control, Point? point) {
            if (point == null) {
                control.point = new Point(0, 0);
            } else { control.point = (Point)point; }            
            control.window = window;
        }

    }

}


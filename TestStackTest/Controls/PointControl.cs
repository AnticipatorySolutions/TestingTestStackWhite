using System;
using TestStackTest.Abstracts;
using TestStack.White.UIItems.WindowItems;
using System.Windows;
using TestStackTest.TestBehaviours;


namespace TestStackTest.Controls {
    public class PointControl : ControlBase {

        public PointControl(ControlType control) {
            SetUp(control);
        }
        public PointControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }
        
        
        public override void Action() {
            if (actionType == ActionType.click) {
                window.Mouse.Location = point;
                window.Mouse.Click(point);
                return;
            } else if (actionType == ActionType.doubleClick) {
                window.Mouse.Location = point;
                window.Mouse.DoubleClick(point);
                return;
            }

            throw new Exception($"TextBox Control Doesn't Accept Action Type {actionType}");
        }
    }
}

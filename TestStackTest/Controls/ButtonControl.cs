using System;
using TestStackTest.Abstracts;

namespace TestStackTest.Controls {
    public class ButtonControl : ControlBase {
        public ButtonControl(ControlType control) {
            SetUp(control);
        }
        public ButtonControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }
        public override void Action() {
            if (actionType == ActionType.click) {
                control.Click();
                return;
            }
            if (actionType == ActionType.doubleClick) {
                control.DoubleClick();
                return;
            }
            throw new Exception($"Control Doesn't Accept Action Type {actionType}");
        }    
    }
}

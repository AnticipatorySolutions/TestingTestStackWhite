using System;
using TestStackTest.Abstracts;

namespace TestStackTest.Controls {
    public class WinFormTextBoxControl : ControlBase {
        public WinFormTextBoxControl(ControlType control) {
            SetUp(control);
        }
        public WinFormTextBoxControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }


        public WinFormTextBoxControl(ControlType control, ActionType action, string criteria = "") {
            SetUp(control, action, criteria);
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
            if (actionType == ActionType.write) {
                control.Enter(criteria);
                return;
            }
            throw new Exception($"TextBox Control Doesn't Accept Action Type {actionType}");
        }
    }


}

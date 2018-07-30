using System;
using TestStackTest.Abstracts;

namespace TestStackTest.Controls {
    public class TextBoxControl : ControlBase {
        public TextBoxControl(ControlType control) {
            SetUp(control);
        }
        public TextBoxControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }

        public TextBoxControl(ControlType control, ActionType action, string criteria = "") {
            SetUp(control, action, criteria);
        }

        public override void Action() {
            if (actionType == ActionType.focus) {
                control.Focus();
                return;
            }
            if (actionType == ActionType.click) {
                control.Click();
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

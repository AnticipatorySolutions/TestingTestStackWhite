using System;
using TestStackTest.Abstracts;

namespace TestStackTest.Controls {
    public class CheckBoxControl : ControlBase {
        public CheckBoxControl(ControlType control) {
            SetUp(control);
        }

        public CheckBoxControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }

        public override void Action() {
            if (actionType == ActionType.click) {
                control.Click();
                return;
            }
            throw new Exception($"CheckBox Control Doesn't Accept Action Type {actionType}");
        }

    }

}

using System;
using TestStackTest.Abstracts;

namespace TestStackTest.Controls {
    public class RadioButtonControl : ControlBase {
        public RadioButtonControl(ControlType control) {
            SetUp(control);
        }

        public RadioButtonControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }

        public RadioButtonControl(ControlType control, ActionType action, string Criteria) {
            SetUp(control, action, Criteria);
        }


        public override void Action() {
            if (actionType == ActionType.click) {
                control.Click();
                return;
            }
            throw new Exception($"Control Doesn't Accept Action Type {actionType}");
        }
    }
}

using System;
using TestStackTest.Abstracts;

namespace TestStackTest.Controls {
    public class KeyboardControl : ControlBase {
        public KeyboardControl(ControlType control) {
            SetUp(control);
        }
        public KeyboardControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }

        public KeyboardControl(ControlType control, ActionType action, string criteria = "") {
            SetUp(control, action, criteria);
        }
        
        public override void Action() {
            if (actionType == ActionType.custom) {
                
                return;
            }


            throw new Exception($"TextBox Control Doesn't Accept Action Type {actionType}");
        }
    }
}

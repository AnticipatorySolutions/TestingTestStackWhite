using System;
using System.Linq;
using TestStackTest.Abstracts;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems;

using TestStackTest.TestBehaviours;

namespace TestStackTest.Controls {
    public class ListViewControl : ControlBase {
        public ListViewControl(ControlType control) {
            SetUp(control);
        }

        public ListViewControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }

        public ListViewControl(ControlType control, ActionType action, string Criteria) {
            SetUp(control, action, Criteria);
        }

        public override void Action() {
            ListView cb = (ListView)control;
            ListViewRow listItem = cb.Rows.Where(item => item.Name == criteria).FirstOrDefault();
            
            if (actionType == ActionType.click) {
                listItem.Click();
                return;
            }
            if (actionType == ActionType.doubleClick) {
                listItem.DoubleClick();
                return;
            }


            if (actionType == ActionType.select) {
                listItem.Select();
                return;
            }

            throw new Exception($"Control Doesn't Accept Action Type {actionType}");
        }
    }
}

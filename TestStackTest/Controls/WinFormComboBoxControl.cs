using System;
using System.Linq;
using TestStackTest.Abstracts;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.ListViewItems;
using TestStackTest.TestBehaviours;

namespace TestStackTest.Controls {
    public class WinFormComboBoxControl : ControlBase {
        public WinFormComboBoxControl(ControlType control) {
            SetUp(control);
        }

        public WinFormComboBoxControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }

        public WinFormComboBoxControl(ControlType control, ActionType action, string Criteria) {
            SetUp(control, action, Criteria);
        }

        public override void Action() {
            WinFormComboBox cb = (WinFormComboBox)control;
            
            ListItem listItem = cb.Items.Where(
                item => item.NameMatches(criteria) || 
                item.Name.Replace(" ","").Contains(criteria) ||
                item.Name.Replace(" ","") == criteria
                ).FirstOrDefault();
            

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

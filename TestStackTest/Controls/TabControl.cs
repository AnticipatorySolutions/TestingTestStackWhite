using System;
using System.Linq;
using TestStackTest.Abstracts;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TabItems;
using TestStackTest.TestBehaviours;


namespace TestStackTest.Controls {
    public class TabControl : ControlBase {
        public TabControl(ControlType control) {
            SetUp(control);
        }

        public TabControl(ControlType control, ActionType action) {
            SetUp(control, action);
        }

        public TabControl(ControlType control, ActionType action, string Criteria) {
            SetUp(control, action, Criteria);
        }
        
        public override void Action() {
            Tab tab = (Tab)control;
            
            ITabPage iTabPage = tab.Pages.Where(x => x.Name.Replace(" ","").Contains(criteria)).FirstOrDefault();
            TabPage tabPage = (TabPage)iTabPage;

            if (actionType == ActionType.click) {
                tabPage.Click();
                return;
            }
            if (actionType == ActionType.doubleClick) {
                tabPage.DoubleClick();
                return;
            }

            if (actionType == ActionType.select) {
                tabPage.Select();
                return;
            }

            throw new Exception($"Control Doesn't Accept Action Type {actionType}");
        }
    }
}

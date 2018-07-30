using System;

using TestStack.White;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using System.Windows;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.Custom;
using ExcelToolSet;
using Castle.Core.Logging;
using TestStackTest.TestBehaviours;
using TestStack.White.UIItems.TabItems;

namespace TestStackTest.TestCases {
    public class Complete_EditCarePlan {

        public Complete_EditCarePlan(Application application) {
            WindowTools windowTools = new WindowTools();
            UtilityTools utilityTools = new UtilityTools();
            List<Window> windows = application.GetWindows();
            Window window = windowTools.GetWindow(windows, "HomeHealthDesktop");
            CheckBox activeBox;

            string[] dayCheckBoxes = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            List<IUIItem> checkBoxes = windowTools.GetIUIItemList<CheckBox>(window);
            foreach (string day in dayCheckBoxes) {
                activeBox = windowTools.GetCheckBox(checkBoxes, day);
                activeBox.Checked = true;
            }

            activeBox = windowTools.GetCheckBox(checkBoxes, "Evening");
            activeBox.Checked = true;


            //TreeView isn't really a treeView object, it's a GDI control (for some reason, I dunno)
            List<IUIItem> tabs = windowTools.GetIUIItemList<Tab>(window);
            Tab treeTab = (Tab)tabs.Where(x => x.Id == "treeTabControl").FirstOrDefault();
            TabPage ActivityTree = windowTools.GetTabPage(treeTab, "Activity Tree");
            ActivityTree.Select();

            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.DOWN);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.UP);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.DOWN);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.DOWN);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.DOWN);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.RIGHT);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.DOWN);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
            
                //ApplyButton cannot be found, (all right orientated buttons on toolStrip1 are not available) so click coordinate required
                Point ApplyButton = new Point(1544, 114);
                windowTools.MouseClickPoint(window, ApplyButton);

                //SaveButton cannot be found, (all right orientated buttons on toolStrip1 are not available) so click coordinate required
                Point SaveButton = new Point(1739, 114);
                windowTools.MouseClickPoint(window, SaveButton);
            
            windowTools.waitForLoadingWindowToClose(application, 1);

        }


    }
}

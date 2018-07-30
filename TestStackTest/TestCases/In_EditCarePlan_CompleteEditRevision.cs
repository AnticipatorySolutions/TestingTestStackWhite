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
    public class In_EditCarePlan_CompleteEditRevision {
        //Workaround to resolve issue with AddClientCase EndDate DatePicker (Checked datepickers block text entry)
        public In_EditCarePlan_CompleteEditRevision(Application application) {
            WindowTools windowTools = new WindowTools();
            UtilityTools utilityTools = new UtilityTools();
            List<Window> windows = windowTools.GetWindows(application);
            
            Window window = windowTools.GetWindow(windows, "EditCarePlanRevision");
            DateTimePicker endDate = windowTools.GetDateTimePicker(window, SearchCriteria.ByAutomationId("endDate"));
            endDate.Enter("11112020");

            utilityTools.DebugWindowItems(window);

            Button okButton = windowTools.GetButton(window, "OK");            
            okButton.DoubleClick();            
        }
        
    }
}

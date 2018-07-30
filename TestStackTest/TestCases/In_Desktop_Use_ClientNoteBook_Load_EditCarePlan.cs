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
    public class In_Desktop_Use_ClientNoteBook_Load_EditCarePlan {
        public In_Desktop_Use_ClientNoteBook_Load_EditCarePlan(Application application) {
            WindowTools windowTools = new WindowTools();
            List<Window> windows = windowTools.GetWindows(application);
            Window window = windowTools.GetWindow(windows, "HomeHealthDesktop");
            List<IUIItem> tabs = windowTools.GetIUIItemList<Tab>(window);            
            Tab tab = (Tab) tabs[1]; //tabs have generic name tabControl, requires ordinal selection            
            windowTools.GetAndSelectTabPage(tab, "Services / Care Plans");
            Button EditCarePlan = windowTools.GetButton(window, "Edit Care Plan");
            windowTools.DoubleClickIUItem(EditCarePlan);            
        }        
    }
}

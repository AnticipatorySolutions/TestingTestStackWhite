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

namespace TestStackTest.TestCases {
    public class SelectFacility {
        public SelectFacility(Application application, string facilityName) {
            WindowTools windowTools = new WindowTools();

            Window window = windowTools.GetWindow(application, "Select Facility");
            RadioButton facilityRadioButton = windowTools.GetRadioButton(window, SearchCriteria.ByAutomationId("facilityRadioButton"));
            windowTools.ClickIUIItem(facilityRadioButton);
            ListBox facilityFilter = windowTools.GetListBox(window, SearchCriteria.ByAutomationId("ListBox"));
            windowTools.SelectListBoxItem(facilityFilter, "Any");
            ListView facilityList = windowTools.GetListView(window, SearchCriteria.ByAutomationId("listView"));
            ListViewRow row = windowTools.SelectListViewRowName(facilityList, facilityName);
            windowTools.DoubleClickIUItem(row);            
        }        
    }
}

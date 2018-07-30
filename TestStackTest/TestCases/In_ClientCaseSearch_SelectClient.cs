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
    public class In_ClientCaseSearch_SelectClient {

        public In_ClientCaseSearch_SelectClient(Application application, string clientName) {
            WindowTools windowTools = new WindowTools();
            List<Window> windows = windowTools.GetWindows(application);
            
            Window window = windowTools.GetWindow(windows, "ClientCaseSearch");
            ListView listView = windowTools.GetListView(window, SearchCriteria.ByAutomationId("listView"));
            ListViewRow result = windowTools.SelectListViewRowName(listView, clientName);
            Assert.IsNotNull(result, "Client Name Not Found In ClientCaseSearch List View.");
            windowTools.DoubleClickIUItem(result);            
        }        
    }
}

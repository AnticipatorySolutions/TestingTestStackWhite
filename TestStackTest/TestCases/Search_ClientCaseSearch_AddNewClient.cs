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
    public class Search_ClientCaseSearch{
        public Search_ClientCaseSearch(Application application, string searchText) {
            WindowTools windowTools = new WindowTools();

            List<Window> windows = windowTools.GetWindows(application);
            Window window = windowTools.GetWindow(windows, "ClientCaseSearch");
            WinFormTextBox textBox = windowTools.GetIUIItem<WinFormTextBox>(window, SearchCriteria.ByAutomationId("criteria"));
            windowTools.PostText(textBox, searchText);
            Button lastNameButton = windowTools.GetButton(window, SearchCriteria.ByAutomationId("lastNameButton"));
            windowTools.DoubleClickIUItem(lastNameButton);
            windows = windowTools.GetWindows(application);
            if (windows.Count > 2) {
                windows[2].Items[0].DoubleClick();
                Button newClientButton = windowTools.GetButton(window, SearchCriteria.ByAutomationId("newClientButton"));
                windowTools.DoubleClickIUItem(newClientButton);
            }
        }        
    }
}
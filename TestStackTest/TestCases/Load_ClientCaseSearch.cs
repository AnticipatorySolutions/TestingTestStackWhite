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
    public class Load_ClientCaseSearch {
        public const string buttonText = "Clients";

        public Load_ClientCaseSearch(Application application) {
            WindowTools windowTools = new WindowTools();
            Window window = windowTools.GetWindow(application, SearchCriteria.ByAutomationId("HomeHealthDesktop"));
            Button clientButton = windowTools.GetButton(window, SearchCriteria.ByText("Clients"));
            windowTools.DoubleClickIUItem(clientButton);           
        }        
    }
}

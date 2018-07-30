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
    public class Login {
        private const string username = "jorussell";
        private const string password = "KillingMeSoftly25";
        public Login(Application application) {
            WindowTools windowTools = new WindowTools();
            Window window = windowTools.GetWindow(application, "Login");
            WinFormTextBox usernameTextBox = windowTools.GetIUIItem<WinFormTextBox>(window, SearchCriteria.ByAutomationId("username"));
            WinFormTextBox passwordTextBox = windowTools.GetIUIItem<WinFormTextBox>(window, SearchCriteria.ByAutomationId("password"));
            Button okButton = windowTools.GetButton(window, SearchCriteria.ByAutomationId("okButton"));


            windowTools.ClickIUIItem(usernameTextBox);
            windowTools.PostText(usernameTextBox, username);
            windowTools.ClickIUIItem(passwordTextBox);
            windowTools.PostText(passwordTextBox, password);
            windowTools.DoubleClickIUItem(okButton);            
        }        
    }
}

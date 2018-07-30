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
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using System.Windows;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.Custom;
using ExcelToolSet;
using Castle.Core.Logging;
using TestStackTest.TestBehaviours;
using TestStackTest.Models;

namespace TestStackTest.TestCases {
    public class Complete_AddClientCase {
        Application application;
        WindowTools windowTools = new WindowTools();

        public Complete_AddClientCase(Application app, Model_AddClientCase clientDetails) {
            application = app;
            
            List<Window> windows = windowTools.GetWindows(application);
            Window window = windowTools.GetWindow(windows, "AddClientCase");

            UIItemCollection items = windowTools.GetWindowItems(window);

            windowTools.PostToComboBoxes(items, clientDetails.mandatoryComboBoxes);
            windowTools.PostToTextBoxes(items, clientDetails.mandatoryTextBoxes);

            windowTools.PostToDatePicker(items, clientDetails.mandatoryDatePickers, window);
            windowTools.PostToTextBoxes(items, clientDetails.optionalTextBoxes);
            windowTools.PostToComboBoxes(items, clientDetails.optionalComboBoxes);
            windowTools.PostToDatePicker(items, clientDetails.optionalDatePickers, window);

            //Note: GeoCode cannot be manually entered, code below solves issue
            window.Items[26].DoubleClick(); //Find button sets GeoCode after address details are entered            
            windows = application.GetWindows(); //Find EditAddressFindByMap
            windows[3].Items[1].DoubleClick(); //DoubleClick OkButton
            
            Button okButton = windowTools.GetButton(window, SearchCriteria.ByAutomationId("okButton"));
            windowTools.DoubleClickIUItem(okButton);            
        }
    }
}

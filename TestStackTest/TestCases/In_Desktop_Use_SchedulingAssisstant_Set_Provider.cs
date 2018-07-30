using System;
using TestStack.White;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
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
using System.Windows.Automation;

namespace TestStackTest.TestCases {
    public class In_Desktop_Use_SchedulingAssisstant_Set_Provider {
        Window window;
        WindowTools windowTools;
        Application application;

        public In_Desktop_Use_SchedulingAssisstant_Set_Provider(Application app, string candidateName) {
            application = app;
            windowTools = new WindowTools();
            String[] weekDays = new String[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            //String[] weekDays = new String[] { "Saturday" };
            Button dayToClick = null;

            foreach (string day in weekDays) {
                List<Window> windows = windowTools.GetWindows(application);
                window = windowTools.GetWindow(windows, "HomeHealthDesktop");
                dayToClick = GetButton(window, day);

                if (dayToClick.IsOffScreen == false) {
                    dayToClick.DoubleClick();
                } else {
                    windowTools.MouseDoubleClickPoint(window, new Point(dayToClick.Location.X, dayToClick.Location.Y));
                }

                SetProviderToCalendar(candidateName);
            }

            CloseSchedulingAssistantTab();
        }

        private Button GetButton(Window window, string buttonName) {
            Button button = null;
            do {
                button = windowTools.GetButton(window, buttonName);
            } while (button == null || !button.Enabled);
            return button;
        }

        private void setCandidate(string candidateName) {
            ListView candidates = windowTools.GetListView(window, SearchCriteria.ByAutomationId("listView"));

            //ListView candidates = (ListView)window.Items[16];
            ListViewRows rows = candidates.Rows;
            bool candidateFound = false;

            foreach (ListViewRow row in rows) {
                if (row.Cells[0].Name == candidateName) {
                    row.Select();
                    row.DoubleClick();
                    candidateFound = true;
                    break;
                }
            }

            if (candidateFound == false) {
                rows[0].Select();
                rows[1].DoubleClick();
            }

        }

        private void ClickGDIControl_ClickApplyButton_HandleOverrideDialog() {
            Button ApplyButton;
            Point pointX;

            int track = 353;
            do {
                //scheduling window doesn't contain named controls, so mouse clicks
                pointX = new Point(track, 606);

                windowTools.MouseDoubleClickPoint(window, pointX);

                ApplyButton = windowTools.GetButton(window, "Apply Changes");
                track += 8;
            } while (!ApplyButton.Enabled);

            ApplyButton.DoubleClick();

            int winCount = application.GetWindows().Count;


            var dialogBox = windowTools.GetFirstDescendant(AutomationElement.RootElement,
                    (e) => e.Name == "CBI" && e.ClassName == "#32770");

            if (dialogBox != null) {
                var button = windowTools.GetFirstDescendant(dialogBox,
                    (e) => e.Name.Contains("Yes") && e.ClassName.Contains("Button"));

                Assert.IsNotNull(button);

                var caption = windowTools.GetFirstDescendant(dialogBox,
                    (e) => e.Name.Contains("Do you wish to attempt to override availability"));

                ((InvokePattern)button.GetCurrentPattern(InvokePattern.Pattern)).Invoke();

            }
        }
        private void SetProviderToCalendar(string candidateName) {
            Button loadButton = null;
            setCandidate(candidateName);
            loadButton = GetButton(window, "Load");
            loadButton.DoubleClick();
            windowTools.waitForLoadingWindowToClose(application, 1);
            ClickGDIControl_ClickApplyButton_HandleOverrideDialog();
            windowTools.waitForLoadingWindowToClose(application, 1);
        }


        private void CloseSchedulingAssistantTab() {
            Tab scheduleAssistantTab = (Tab)window.Items[2];
            TabPages tabPages = scheduleAssistantTab.Pages;
            foreach (ITabPage tabPage in tabPages) {
                if (tabPage.Name.Contains("SA:")) {
                    var x = tabPage.Bounds;
                    Point closeButton = new Point(tabPage.Bounds.Right - 5, tabPage.Bounds.Top + 5);
                    windowTools.MouseClickPoint(window, closeButton);
                    break;
                }
            }
        }

    }
}

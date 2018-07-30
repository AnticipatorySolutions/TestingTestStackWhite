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
using TestStackTest.Models;
using TestStackTest.TestBehaviours;
using TestStackTest.TestCases;
using TestStackTest.Reporting;
using TestStackTest.Abstracts;
using TestStackTest;
using ScreenCapture;

namespace QA_Simulator {

    public class TestManager {
        //my build        private const string exeLocation = @"C:\development\Jordan\CBIBilling\IMS-V3.6\DataCenter\Desktop\bin\Debug\CBI.DataCenter.Desktop.exe";
        private const string exeLocation = @"C:\development\Jordan\CBIBilling\IMS-V3.70\DataCenter\Desktop\bin\Debug\CBI.DataCenter.Desktop.exe"; //QA Build

        private const string workingDirectory = @"C:\development\Jordan\CBIBilling\IMS-V3.70\DataCenter\Desktop\bin\Debug\";
        private const string testDataLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\TestData.xlsx";

        private Application _application = default(Application);
        private ReportingTools _report = default(ReportingTools);
        private DateTime start;
        private DateTime originStart;
        private string _clientLastName;

        private void createSUT() {
            InitializeSystemUnderTest SUT = new InitializeSystemUnderTest(exeLocation, workingDirectory);
            _application = SUT.application;
        }


        
        public TestManager() {
            _report = new ReportingTools("QA Simulator");
            ExcelWriter excelWriter = new ExcelWriter(_report.getFileLocation());
            DateTime start = _report.getNow();
            originStart = start;
            List<Action> tests = new List<Action>();
            tests.Add(Test_CreateNewClient);
            tests.Add(Test_AddNewVisitPlan);
            tests.Add(Test_SetProvider);
            tests.Add(Test_CompleteScheduledVisit);
            foreach (Action test in tests) {
                _report.createReportItem("");
                start = _report.getNow();
                _report.createReportItem(test.Method.Name);
                try {
                    createSUT();                
                    test();
                    _report.createReportItem($"{test.Method.Name}", start);
                } catch (Exception e) {

                    ScreenCapturer.CaptureAndSave($@"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\%NOW%_{test.Method.Name}", CaptureMode.Screen);

                    _report.createReportItem($"Test failure {test.Method.Name}", start, $"Target: {e.TargetSite} Source: {e.Source} Message: {e.Message}");
                    excelWriter.WriteToFile(_report.getReport());
                    ShutDownSystemUnderTest close = new ShutDownSystemUnderTest(_application);
                    return;
                }
                
            }

            _report.createReportItem("");
            _report.createReportItem("Test Set Complete", originStart);
            excelWriter.WriteToFile(_report.getReport());
        }

        public TestManager(Boolean test) {

            //createSUT();            
            Test_Test();
        }
        


        private void Test_Test() {
            createSUT();
            Test_CreateNewClient();
            createSUT();
            Test_AddNewVisitPlan();
            createSUT();
            Test_SetProvider();
            createSUT();
            Test_CompleteScheduledVisit();
        }

        #region currentTestModules



        private void Test_CreateNewClient() {
            string facilityName = "CBI Toronto Yonge";

            start = _report.getNow();
            new TestStackTest.Views.Login(_application, new TestStackTest.Commands.LoginDefault());
            _report.createReportItem("System Login - Create New Client", start);

            start = _report.getNow();
            new TestStackTest.Views.SelectFacility(_application, new TestStackTest.Commands.SelectFacilityDefault(), facilityName);
            _report.createReportItem("Select Facility - Create New Client", start);

            start = _report.getNow();
            new TestStackTest.Views.Desktop(_application, new TestStackTest.Commands.Desktop_ClickButton(), 0);
            _report.createReportItem("Click Desktop Client Button- Create New Client", start);
            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_SearchLastName(), "ZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
            _report.createReportItem("Search For Unkown Client - Create New Client", start);
            // the step above should result in a message box appearing saying that no user of that name has been found, but if it doesn't open skip closing it
            if (_application.GetWindows().Count == 3) {
                start = _report.getNow();
                new TestStackTest.Views.MessageBox(_application, new TestStackTest.Commands.MessageBoxDefault());
                _report.createReportItem("Close Unknown Client Modal Message Box - Create New Client", start);
            }
            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_ClickAddNewClientButton());
            _report.createReportItem("Click Add New Client Button - Create New Client", start);
            //the section below is data driven, and contains the older style of assembly, keep it until it can be refactored
            start = _report.getNow();
            ExcelReader excelReader = new ExcelReader(testDataLocation);
            ExcelReader.ReturnObject dataSet = excelReader.ReadSheet(1, 51);
            _report.createReportItem("Read Test Data Set - Create New Client", start);

            start = _report.getNow();
            Model_AddClientCase clientDetails = new Model_AddClientCase(dataSet.results[0], dataSet.headers);
            _report.createReportItem("Create New Client Data Model - Create New Client", start);

            //set client last name test global to be used in all 
            _clientLastName = clientDetails.mandatoryTextBoxes["lastName"];

            start = _report.getNow();
            new Complete_AddClientCase(_application, clientDetails);
            _report.createReportItem("Fill Add New Client Form - Create New Client", start);

            start = _report.getNow();
            new ShutDownSystemUnderTest(_application);
            _report.createReportItem("Shutdown - Create New Client", start);
        }

        private void Test_AddNewVisitPlan() {
            string clientName;
            if (_clientLastName.Length != 0) 
            {
                clientName = _clientLastName;
            } else { clientName = "TestAccount"; }

            
            string facilityName = "CBI Toronto Yonge";
            start = _report.getNow();
            new TestStackTest.Views.Login(_application, new TestStackTest.Commands.LoginDefault());
            _report.createReportItem("Login - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.SelectFacility(_application, new TestStackTest.Commands.SelectFacilityDefault(), facilityName);
            _report.createReportItem("Select Facility - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.Desktop(_application, new TestStackTest.Commands.Desktop_ClickButton(), 0);
            _report.createReportItem("Click Desktop Client Button - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_SearchLastName(), clientName);
            _report.createReportItem("Search For Existing Client - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_ClickOkButton(), clientName);
            _report.createReportItem("Confirm Client Selection - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.Desktop(_application, new TestStackTest.Commands.Desktop_ClickTab(), 0, clientName);
            _report.createReportItem("Select Client Notebook - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook(_application, new TestStackTest.Commands.ClientNoteBook_ClickTab(), 0, nameof(ClientNoteBookTabs.CarePlans));
            _report.createReportItem("Select Care Plan Tab in Client Notebook - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook_CarePlan(_application, new TestStackTest.Commands.ClientNoteBook_CarePlan_EditCarePlan(), 0);
            _report.createReportItem("Click Edit Care Plan Button in Client Notebook - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.EditCarePlan(_application, new TestStackTest.Commands.EditCarePlan_ClickRevisionButton(), 0);
            _report.createReportItem("Click Revision Button in Edit Care Plan - New Visit Plan", start);

            start = _report.getNow();
            new TestStackTest.Views.EditCarePlan_Revision(_application, new TestStackTest.Commands.EditCarePlanRevisionCommands(), "12122020");
            _report.createReportItem("Update Care Plan - New Visit Plan", start);

            // the section below contains several workaround commands in the older style of assembly, keep until it can be refactored   
            start = _report.getNow();
            new Complete_EditCarePlan(_application);
            _report.createReportItem("Complete Edit Care Plan (This test behaviour requires revision) - New Visit Plan", start);

            start = _report.getNow();
            new ShutDownSystemUnderTest(_application);
            _report.createReportItem("Shutdown - New Visit Plan", start);
        }
        private void Test_SetProvider() {
            string clientName;
            if (_clientLastName.Length != 0) {
                clientName = _clientLastName;
            } else { clientName = "TestAccount"; }

            string providerName = "SAID, SOFIA";
            string facilityName = "CBI Toronto Yonge";

            start = _report.getNow();
            new TestStackTest.Views.Login(_application, new TestStackTest.Commands.LoginDefault());
            _report.createReportItem("Login - Set Provider", start);

            start = _report.getNow();
            new TestStackTest.Views.SelectFacility(_application, new TestStackTest.Commands.SelectFacilityDefault(), facilityName);
            _report.createReportItem("Select Facility - Set Provider", start);

            start = _report.getNow();
            new TestStackTest.Views.Desktop(_application, new TestStackTest.Commands.Desktop_ClickButton(), 0);
            _report.createReportItem("Click Desktop Client Button - Set Provider", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_SearchLastName(), clientName);
            _report.createReportItem("Search for Existing Client - Set Provider", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_ClickOkButton(), clientName);
            _report.createReportItem("Confirm Client Selection - Set Provider", start);

            start = _report.getNow();
            new TestStackTest.Views.Desktop(_application, new TestStackTest.Commands.Desktop_ClickTab(), 0, clientName);
            _report.createReportItem("Select Client Notebook - Set Provider", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook(_application, new TestStackTest.Commands.ClientNoteBook_ClickTab(), 0, nameof(ClientNoteBookTabs.CarePlans));
            _report.createReportItem("Select Care Plan Tab of Client Notebook - Set Provider", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook_CarePlan(_application, new TestStackTest.Commands.ClientNoteBook_CarePlan_SchedulingAssistant(), 0);
            _report.createReportItem("Select Scheduling Assistant in Client Notebook - Set Provider", start);

            // waiting for modal window to close
            start = _report.getNow();
            WindowTools windowTools = new WindowTools();
            windowTools.waitForLoadingWindowToClose(_application, 1);
            _report.createReportItem("Wait for Modal Window to Close - Set Provider", start);

            // the section below contains several workaround commands in the older style of assembly, keep until it can be refactored   
            start = _report.getNow();
            new In_Desktop_Use_SchedulingAssisstant_Set_Provider(_application, providerName);
            _report.createReportItem("Set Provider Details (This test behaviour requires revision) - Set Provider", start);

            start = _report.getNow();
            new ShutDownSystemUnderTest(_application);
            _report.createReportItem("Shutdown - Set Provider", start);
        }

        private void Test_CompleteScheduledVisit() {
            VisitCompletionTypes type = VisitCompletionTypes.VisitCancelled;
            VisitCompletionReasons reason = VisitCompletionReasons.CANNOTICE;

            string clientName;
            if (_clientLastName.Length != 0) {
                clientName = _clientLastName;
            } else { clientName = "TestAccount"; }

            string facilityName = "CBI Toronto Yonge";

            //This test will fail if visit completion of type  NSNF || Visit Completion and a provider is not scheduled. 
            //This test accounts for the 21 variations of visits completion
            start = _report.getNow();
            new TestStackTest.Views.Login(_application, new TestStackTest.Commands.LoginDefault());
            _report.createReportItem("Login - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.SelectFacility(_application, new TestStackTest.Commands.SelectFacilityDefault(), facilityName);
            _report.createReportItem("Select Facility - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.Desktop(_application, new TestStackTest.Commands.Desktop_ClickButton(), 0);
            _report.createReportItem("Click Desktop Client Button - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_SearchLastName(), clientName);
            _report.createReportItem("Search for Existing Client - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientCaseSearch(_application, new TestStackTest.Commands.ClientCaseSearch_ClickOkButton(), clientName);
            _report.createReportItem("Confirm Selection of Client - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.Desktop(_application, new TestStackTest.Commands.Desktop_ClickTab(), 0, clientName);
            _report.createReportItem("Select Client Notebook - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook(_application, new TestStackTest.Commands.ClientNoteBook_ClickTab(), 0, nameof(ClientNoteBookTabs.Calendar));
            _report.createReportItem("Select Calendar from Client Notebook - Complete Scheduled Visit", start);

            // waiting for modal window to close
            start = _report.getNow();
            WindowTools windowTools = new WindowTools();
            windowTools.waitForLoadingWindowToClose(_application, 1);
            _report.createReportItem("Waiting for Modal Window to Close - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook_CalendarService(_application, new TestStackTest.Commands.ClientNoteBook_CalendarService_ClickOneDay(), 0);
            _report.createReportItem("Select One Day View in Calendar Service - Complete Scheduled Visit", start);

            // waiting for modal window to close
            start = _report.getNow();
            windowTools.waitForLoadingWindowToClose(_application, 1);
            _report.createReportItem("Waiting for Modal Window to Close - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook_CalendarService(_application, new TestStackTest.Commands.ClientNoteBook_CalendarService_DoubleClickPoint(), 0, new Point(441, 363));
            _report.createReportItem("Open Appointment Details in Calendar Service- Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook_CalendarService_AppointmentDetails(_application, new TestStackTest.Commands.ClientNoteBook_CalendarService_AppointmentDetails_ClickVisitCompletion());
            _report.createReportItem("Open Visit Completion in Appointment Details - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.SelectVisitCompletionTypeTime(_application, new TestStackTest.Commands.SelectVisitCompletionTypeTime_Cancelled(), VisitCompletionTypes.VisitCancelled, VisitCompletionReasons.CANNOTICE, $"type:{nameof(type)} reason:{nameof(reason)}");
            _report.createReportItem("Fill Visit Completion Form - Complete Scheduled Visit", start);

            start = _report.getNow();
            new TestStackTest.Views.ClientNoteBook_CalendarService_AppointmentDetails(_application, new TestStackTest.Commands.ClientNoteBook_CalendarService_AppointmentDetails_ClickCloseButton());
            _report.createReportItem("Close Appointment Details  - Complete Scheduled Visit", start);

            start = _report.getNow();
            new ShutDownSystemUnderTest(_application);
            _report.createReportItem("Shutdown - Complete Scheduled Visit", start);
        }

        #endregion

    }
}

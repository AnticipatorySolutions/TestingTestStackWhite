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


namespace TestStackTest {
    [TestClass]
    public class UnitTest1 {
        static ProcessStartInfo processStartInfo = new ProcessStartInfo(@"C:\development\Jordan\CBIBilling\IMS-V3.6\DataCenter\Desktop\bin\Debug\CBI.DataCenter.Desktop.exe") {
            WorkingDirectory = @"C:\development\Jordan\CBIBilling\IMS-V3.6\DataCenter\Desktop\bin\Debug"
        };

        static Application application = Application.Launch(processStartInfo);

        public Window Load_LoginForm_Submit_ValidSignIn_Load_SelectFacility(Window window) {
            window = application.GetWindow("Login", InitializeOption.NoCache);
            WinFormTextBox login = window.Get<WinFormTextBox>(SearchCriteria.ByAutomationId("username"));
            WinFormTextBox password = window.Get<WinFormTextBox>(SearchCriteria.ByAutomationId("password"));
            Button okButton = window.Get<Button>(SearchCriteria.ByAutomationId("okButton"));

            login.Click();
            login.BulkText = "jorussell";

            password.Click();
            password.BulkText = "KillingMeSoftly25";

            okButton.DoubleClick();

            List<Window> newWindows = application.GetWindows();

            
            Assert.IsTrue(newWindows.Count == 1);
            Assert.IsTrue(newWindows[0].Name == "Select Facility");
            Assert.IsTrue(newWindows[0].Title == "Select Facility");

            window.Close();

            return newWindows[0];
        }

        public void In_SelectFacility_Select_SystemAdminRadioButton(Window window) {
            RadioButton systemAdminRadioButton = window.Get<RadioButton>(SearchCriteria.ByAutomationId("systemAdminRadioButton"));

            systemAdminRadioButton.Click();
            Assert.IsTrue(systemAdminRadioButton.IsSelected);
        }

        public void In_SelectFacility_Select_FacilityRadioButton(Window window) {
            RadioButton facilityRadioButton = window.Get<RadioButton>(SearchCriteria.ByAutomationId("facilityRadioButton"));

            facilityRadioButton.Click();
            Assert.IsTrue(facilityRadioButton.IsSelected);
        }

        public void In_SelectFacility_Count_Labels(Window window) {
            int count = window.Items.Where(x => x.GetType() == typeof(Label)).Count();
            Assert.IsTrue(count > 0);
        }


        public void In_SelectFacility_Compare_FacilityListingsCounts(Window window) {
            //Dropdown box containing: Any/RehabClinic/HomeHealth/MonarchHouse
            ListBox facilityFilter = window.Get<ListBox>(SearchCriteria.ByAutomationId("ListBox"));
            ListView FacilityListings = null;
            int rehabCount, homeHealthCount, monarchCount, anyCount, total = 0;

            facilityFilter.Item("Rehabilitation Clinic").Select();
            FacilityListings = window.Get<ListView>(SearchCriteria.ByAutomationId("listView"));
            rehabCount = FacilityListings.Rows.Count;
            System.Diagnostics.Debug.WriteLine($"rehab {rehabCount}");

            facilityFilter.Item("Home Health Office").Select();
            FacilityListings = window.Get<ListView>(SearchCriteria.ByAutomationId("listView"));
            homeHealthCount = FacilityListings.Rows.Count;
            System.Diagnostics.Debug.WriteLine($"homeHealth {homeHealthCount}");

            facilityFilter.Item("Monarch House").Select();
            FacilityListings = window.Get<ListView>(SearchCriteria.ByAutomationId("listView"));
            monarchCount = FacilityListings.Rows.Count;
            System.Diagnostics.Debug.WriteLine($"monarch {monarchCount}");

            facilityFilter.Item("Any").Select();
            FacilityListings = window.Get<ListView>(SearchCriteria.ByAutomationId("listView"));
            anyCount = FacilityListings.Rows.Count;
            System.Diagnostics.Debug.WriteLine($"any {anyCount}");

            total = (rehabCount + homeHealthCount + monarchCount);
            System.Diagnostics.Debug.WriteLine($"total {total}");

            Assert.IsTrue(anyCount == total);
        }


        public Window In_SelectFacility_Choose_Facility_Load_Desktop(Window window, string facility) {
            ListBox facilityFilter = window.Get<ListBox>(SearchCriteria.ByAutomationId("ListBox"));
            facilityFilter.Item("Any").Select();

            ListView facilityListings = window.Get<ListView>(SearchCriteria.ByAutomationId("listView"));

            ListViewRow row = facilityListings.Rows.Where(x => x.Name == facility).FirstOrDefault();

            row.Select();
            row.DoubleClick();

            List<Window> newWindows = application.GetWindows();
            Assert.IsTrue(newWindows.Count == 1);
            Assert.IsTrue(newWindows[0].Id == "HomeHealthDesktop");

            window.Close();

            return newWindows[0];

        }

        public Window In_Desktop_Load_ClientCaseSearch(Window window) {
            Button clientButton = window.Get<Button>(SearchCriteria.ByText("Clients"));

            clientButton.DoubleClick();

            List<Window> newWindows = application.GetWindows();

            Assert.IsTrue(newWindows.Count == 2);
            Assert.IsTrue(newWindows[1].Id == "ClientCaseSearch");

            return newWindows[1];
        }

        public Window In_ClientCaseSearch_Enable_NewClientButton_Load_AddClientCase(Window window) {
            WinFormTextBox criteria = window.Get<WinFormTextBox>(SearchCriteria.ByAutomationId("criteria"));
            criteria.Text = "ZZZZZZZZZZZZZZZZZZZZ";  //unlikely to be a last name
            Button search = window.Get<Button>(SearchCriteria.ByAutomationId("lastNameButton"));
            search.DoubleClick();

            List<Window> newWindows = application.GetWindows();

            //If Client Name not found close "Client not found window"
            if (newWindows.Count == 3) {
                newWindows[2].Items[0].DoubleClick();
            }

            window.Items[4].DoubleClick();

            newWindows = application.GetWindows();

            return newWindows[2];

        }

        class AddClientCase_ClientDetails {
            //listed in order of text boxes            
            public string building = "";
            public string room = "";
            public string postal = "M4S 2K9"; //mandatory
            public string city = "Toronto";
            public string address1 = "75 Brownlow Ave."; //mandatory
            public string address2 = "";
            public string home_extension = "";
            public string work_extension = "";
            public string preferredName = "";
            public string healthNumber = "";
            public string email = "";
            public string cell = "";
            public string workPhone = "";
            public string homePhone = "";
            public string firstName = "Catechism"; //mandatory
            public string lastName = "TestAccount";  //mandatory
        }

        public Window In_AddClientCase_Set_ClientDetails_Load_ClientCaseSearch(Window window) {
            DateTimePicker seeByDate, endDate, startDate, dateOfBirth;
            
            List<Window> newWindows = new List<Window>();
            List<IUIItem> textBoxes = window.Items.Where(x => x.GetType() == typeof(WinFormTextBox)).ToList();
            List<IUIItem> comboBoxes = window.Items.Where(x => x.GetType() == typeof(WinFormComboBox)).ToList();
            
            ComboBox comboBoxAction;
            
            AddClientCase_ClientDetails clientDetails = new AddClientCase_ClientDetails();
            textBoxes[0].SetValue(clientDetails.building);
            textBoxes[1].SetValue(clientDetails.room);
            textBoxes[2].SetValue(clientDetails.postal);
            textBoxes[4].SetValue(clientDetails.city);
            textBoxes[5].SetValue(clientDetails.address1);
            textBoxes[6].SetValue(clientDetails.address2);
            textBoxes[7].SetValue(clientDetails.home_extension);
            textBoxes[8].SetValue(clientDetails.work_extension);
            textBoxes[9].SetValue(clientDetails.preferredName);
            textBoxes[10].SetValue(clientDetails.healthNumber);
            textBoxes[11].SetValue(clientDetails.email);
            textBoxes[12].SetValue(clientDetails.cell);
            textBoxes[13].SetValue(clientDetails.workPhone);
            textBoxes[14].SetValue(clientDetails.homePhone);
            textBoxes[15].SetValue(clientDetails.firstName);
            textBoxes[16].SetValue(clientDetails.lastName);

            //select first value for all combo boxes
            for (var i = 0; i < comboBoxes.Count; i++) {
                comboBoxAction = (ComboBox)comboBoxes[i];
                if (comboBoxAction.Items.Count > 0) {
                    if (comboBoxAction.Id == "provinceComboBox") { continue; } 
                    comboBoxAction.Items[0].Select();
                }
            }
            
            //Note: GeoCode cannot be manually entered, code below solves issue
            window.Items[26].DoubleClick(); //Find button sets GeoCode after address details are entered            
            newWindows = application.GetWindows(); //Find EditAddressFindByMap
            newWindows[3].Items[1].DoubleClick(); //DoubleClick OkButton


            
            dateOfBirth = window.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dateOfBirth"));
            seeByDate = window.Get<DateTimePicker>(SearchCriteria.ByAutomationId("seeByDate"));
            startDate = window.Get<DateTimePicker>(SearchCriteria.ByAutomationId("startDate"));
            endDate = window.Get<DateTimePicker>(SearchCriteria.ByAutomationId("endDate"));
            
            startDate.DoubleClick();
            seeByDate.Click();
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.F4);
            Panel cc = window.Get<Panel>(SearchCriteria.ByText("Calendar Control"));
            cc.Items[2].Click();

            setDateInCheckedPicker(window, dateOfBirth, -1);
            setDateInCheckedPicker(window, endDate, 1);

            window.Get<Button>(SearchCriteria.ByAutomationId("okButton")).DoubleClick();


            List<Window> w = application.GetWindows();
            return w[1];
            
        }


        public void In_ClientCaseSearch_Confirmation(Window window) {
            ListView listView = window.Get<ListView>(SearchCriteria.ByAutomationId("listView"));
            Assert.IsTrue(listView.Items.Count == 1);
            Assert.IsTrue(listView.Rows[0].IsSelected);
            Assert.IsTrue(window.Items[3].Name == "TestAccount, Catechism");
            
        }


        [TestMethod]
        public void CreateNewClient_HomeHealth() {

            //TestStack.White.Configuration.CoreAppXmlConfiguration.Instance.LoggerFactory = new TestStack.White.Configuration.WhiteDefaultLoggerFactory(LoggerLevel.Off);
            //TestStack.White.Configuration.CoreAppXmlConfiguration.Instance.LoggerFactory = new ConsoleFactory(LoggerLevel.Off);
            

            Window window = null;
            Window desktop = null;
            Window dialog = null;
            window = Load_LoginForm_Submit_ValidSignIn_Load_SelectFacility(window);
            In_SelectFacility_Select_FacilityRadioButton(window);
            desktop = In_SelectFacility_Choose_Facility_Load_Desktop(window, "CBI Toronto Yonge");
            window = In_Desktop_Load_ClientCaseSearch(desktop);
            dialog = In_ClientCaseSearch_Enable_NewClientButton_Load_AddClientCase(window);
            window = In_AddClientCase_Set_ClientDetails_Load_ClientCaseSearch(dialog);
            In_ClientCaseSearch_Confirmation(window);
            //TODO: reporting module    
            

            //leads to AddNewVisitPlan_HomeHealth
        }


        public Window In_ClientCaseSearch_SelectClient_Load_Desktop_Display_ClientNotebook(Window window) {
            Console.WriteLine("In_ClientCaseSearch_SelectClient_Load_ClientNotebook");

            ListView listView = window.Get<ListView>(SearchCriteria.ByAutomationId("listView"));
            Assert.IsTrue(listView.Items.Count > 0);

            foreach (var x in listView.Rows) {
                if (x.Name == "TestAccount, Catechism") {
                    x.DoubleClick();
                    break;
                }
            }

            List<Window> w = application.GetWindows();
            Assert.IsTrue(w.Count == 1);
            return w[0];
            
        }

        public void In_Desktop_Use_ClientNotebook_Load_EditCarePlan(Window window) {
            Review_Window_Panels(window);
        
        }

        [TestMethod]
        public void AddNewVisitPlan_HomeHealth() {
            //Continues from previous test CreateNewClient_HomeHealth

            List< Window > w = application.GetWindows();
            Window clientCaseSearch = w[1];
            Window desktop = In_ClientCaseSearch_SelectClient_Load_Desktop_Display_ClientNotebook(clientCaseSearch);
            
            In_Desktop_Use_ClientNotebook_Load_EditCarePlan(desktop);
            //In_EditCarePlan_Add_Activities_Load_ClientNoteBook(window);

            //TODO: reporting module    
            
            ShutDown();


        }











        /*
        [TestMethod]
        public void RunExcelReader() {
            ExcelReader xlReader = new ExcelReader(@"S:\Dev_IMS\Development\Jordan\uiAutomation\oddities.xlsx");
            ExcelWriter xlWriter = new ExcelWriter(@"S:\Dev_IMS\Development\Jordan\uiAutomation\odd");


            List<List<string>> x = xlReader.ReadSheet(1);
            xlWriter.WriteToFile(x);

            try {
                Assert.IsNull(xlReader);
                Console.WriteLine("Success");
            } catch {
                Console.WriteLine("Failure");
            }

        }
        */

        //Utility functions
        public void Review_Panel_Contents(Panel panel) {
            Console.WriteLine($"Panel Name {panel.Name}");
            Console.WriteLine($"Panel ID {panel.Id}");
            Console.WriteLine($"Panel Prime {panel.PrimaryIdentification}");
            int c = 0;            
            foreach (var i in panel.Items) {
                Console.WriteLine($"Item Number {c}");
                Console.WriteLine($"Item Name {i.Name}");
                Console.WriteLine($"Item ID {i.Id}");
                Console.WriteLine($"Item Prime {i.PrimaryIdentification}");
                Console.WriteLine($"Item Type {i.GetType().ToString()}");
                Console.WriteLine($"IsOffScreen {i.IsOffScreen}");
                Console.WriteLine($"IsFocussed {i.IsFocussed}");
                Console.WriteLine($"Location {i.Location.ToString()}");
                Console.WriteLine($"IsVisible {i.Visible}");
                Console.WriteLine($"Framework {i.Framework}");
                Console.WriteLine($"Enabled {i.Enabled}");
                
                Console.WriteLine($"Bounds {i.Bounds.ToString()}");
                Console.WriteLine($"Boarder Colour {i.BorderColor.ToString()}");
                Console.WriteLine($"Automation Element {i.AutomationElement.ToString()}");
                Console.WriteLine($"Access Key {i.AccessKey.ToString()}");
                Console.WriteLine("");
                c++;
            }           
        }

        public void Review_Window_Panels(Window window) {
            Console.WriteLine($"{window.Name}");
            Console.WriteLine($"{window.Id}");
            Console.WriteLine($"{window.PrimaryIdentification}");
            List<IUIItem> panels = window.Items.Where(x => x.GetType() == typeof(Panel)).ToList();
            
            foreach (var i in panels) {
                Review_Panel_Contents((Panel)i);    
            }

        }



        public void clickDatePickerCheckBox(Window window, DateTimePicker picker) {
            Point point = new Point((picker.Location.X + 5), (picker.Location.Y + 3));
            window.Mouse.Location = point;
            window.Mouse.Click();
        }


        public void setDateInCheckedPicker(Window window, DateTimePicker picker, int years) {
            clickDatePickerCheckBox(window, picker);
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.F4);
            int numberOfMonths = findNumberOfMonthsFromNumberOfYears(years);
            int directionMarker = -1;
            Panel cc = window.Get<Panel>(SearchCriteria.ByText("Calendar Control"));

            if (numberOfMonths == 0) {
                cc.Items[2].Click();
                return;
            }
            if (numberOfMonths < 0) {
                directionMarker = 1;
                numberOfMonths *= -1;
            } else { directionMarker = 0; }

            for (var i = 0; i <= numberOfMonths; i++) {
                cc.Items[directionMarker].Click();                
            }
            picker.Click();

        }


        public int findNumberOfMonthsFromNumberOfYears(int years) {
            DateTime n = DateTime.Now.AddYears(years);
            Double o = (n - DateTime.Now).TotalDays;
            o = Math.Round(o / 365 * 12);
            int x = (int)o;
            return x;
        }



        public void ShutDown() {
            application.Close();
            application.Dispose();
        }


        public void Label_All_TextBoxes(Window window) {
            List<IUIItem> textBoxes = window.Items.Where(x => x.GetType() == typeof(WinFormTextBox)).ToList();

            for (var i = 0; i < textBoxes.Count; i++) {
                Console.WriteLine($"Name {textBoxes[i].Name}");
                Console.WriteLine($"Id {textBoxes[i].Id}");


                try {

                    textBoxes[i].SetValue(i);
                    Console.WriteLine("success");
                } catch (Exception e) {
                    Console.WriteLine("failure");

                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }




        public void Review_Window_Items(Window window) {
            var x = window.Items;

            Assert.IsTrue(x.Count > 0);
            int track = 0;
            foreach (var y in x) {
                System.Diagnostics.Debug.WriteLine($"");
                System.Diagnostics.Debug.WriteLine($"Window.Items Number: {track}");
                System.Diagnostics.Debug.WriteLine($"Name {y.Name}");
                System.Diagnostics.Debug.WriteLine($"Type: {y.GetType()}");
                System.Diagnostics.Debug.WriteLine($"Id: {y.Id}");
                System.Diagnostics.Debug.WriteLine($"PrimeId: {y.PrimaryIdentification}");
                System.Diagnostics.Debug.WriteLine($"IsFocussed: {y.IsFocussed}");
                
                track++;
            }
        }


        public void Review_New_Window(List<Window> newWindows) {
            if (newWindows == null || newWindows.Count == 0) {
                System.Diagnostics.Debug.WriteLine("New Window is empty");
            } else {
                System.Diagnostics.Debug.WriteLine("New Window is not empty");
                int track = 0;
                foreach (Window n in newWindows) {
                    System.Diagnostics.Debug.WriteLine($"");
                    System.Diagnostics.Debug.WriteLine($"newWindow{track}.GetType {n.GetType()}");
                    System.Diagnostics.Debug.WriteLine($"newWindow{track}.Id {n.Id}");
                    System.Diagnostics.Debug.WriteLine($"newWindow{track}.Name {n.Name}");
                    System.Diagnostics.Debug.WriteLine($"newWindow{track}.Title {n.Title}");
                    track++;
                }
            }
        }

        public void Review_Modal_Windows(Window window) {
            List<Window> modal = window.ModalWindows();

            if (modal == null || modal.Count == 0) {
                System.Diagnostics.Debug.WriteLine("Modal is empty");
            } else {
                System.Diagnostics.Debug.WriteLine("Modal is not empty");
                int track = 0;
                foreach (Window m in modal) {
                    System.Diagnostics.Debug.WriteLine("");
                    System.Diagnostics.Debug.WriteLine($"modal{track}.GetType {m.GetType()}");
                    System.Diagnostics.Debug.WriteLine($"modal{track}.Name {m.Name}");
                    System.Diagnostics.Debug.WriteLine($"modal{track}.Title {m.Title}");
                    track++;
                }
            }
        }



    }
}

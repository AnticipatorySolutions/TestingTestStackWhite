using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestStackTest.TestBehaviours;
using System.IO;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TabItems;
using ScreenCapture;
using TestStackTest.Abstracts;
using TestStack.White.UIItems.ListBoxItems;

namespace TestStackTest {
    public class CaptureTool {
        private const string exeLocation = @"C:\development\Jordan\CBIBilling\IMS-V3.70\DataCenter\Desktop\bin\Debug\CBI.DataCenter.Desktop.exe"; //QA Build
        private const string workingDirectory = @"C:\development\Jordan\CBIBilling\IMS-V3.70\DataCenter\Desktop\bin\Debug";
        private const string testDataLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\TestData.xlsx";
        private Application _application = default(Application);
        private UtilityTools utilityTools = new UtilityTools();
        private WindowTools windowTools = new WindowTools();
        private string inputString;
        private const string trackingLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\";
        private const string windowMapLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\test.xml";

        private const string trackingType = ".txt";
        private int DesktopCount = 0;
        TextWriter temp = Console.Out;
       
        public CaptureTool() {
            
            bool runState = true;
            startUp();
            do {
                runState = Monitor();

            } while (runState == true);

            shutDown();
            
        }

        public string MakeFileLocation(string name) {
            return $"{trackingLocation}{DateTime.Now.DayOfYear.ToString()}_{name}{trackingType}";            
        }



        public bool Monitor() {
            string activeWindowName;
            string fileLocation;
            if (Console.KeyAvailable) {
                inputString = Console.ReadLine();

                if (inputString.Contains("click")) {
                    Window active = getActiveWindow();
                    UtilityTools ut = new UtilityTools();
                    activeWindowName = ut.getWindowDetails(active);

                    utilityTools.serializeWindow(active, activeWindowName, windowMapLocation, trackingLocation);

                    //Console.WriteLine("Taking Desktop Image : Start");
                    //ScreenCapturer.CaptureAndSave($@"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\%NOW%_{activeWindowName}", CaptureMode.Screen);
                    //Console.WriteLine("Taking Desktop Image : Finished");


                    /*

                                        fileLocation = MakeFileLocation(activeWindowName);
                                        FileStream fs = new FileStream(fileLocation, FileMode.Create);
                                        StreamWriter sw = new StreamWriter(fs);
                                        Console.SetOut(sw);
                                        click(active);
                                        Console.SetOut(temp);
                                        sw.Close();
                                        */
                    Console.WriteLine("On to the next");
                    
                    return true;
                }
                if (inputString.Contains("Exit")){
                    return false;
                }                
            }
            return true;
        }



        public Application getApp() { return _application; }

        private void startUp() {
            _application = new TestCases.InitializeSystemUnderTest(exeLocation, workingDirectory).application;
        }

        private void shutDown() {
            new TestCases.ShutDownSystemUnderTest(_application);
        }

        public Window getActiveWindow() {
            Application app = getApp();
            List<Window> windows = app.GetWindows();
            Window active;
            if (windows.Count > 1) {
                active = windows[windows.Count - 1];
            } else 
            {
                active = windows[0];
            }
            return active;
        }


        public string getWindowDetails(Window window) {
            if (window.Id != "") {
                if (window.Id=="HomeHealthDesktop") {
                    Tab tab = (Tab)windowTools.GetIUIItemList<Tab>(window)[0];
                    ITabPage iTabPage = tab.Pages.Where(x => x.IsSelected).FirstOrDefault();
                    if (iTabPage.Name.Replace(" ","").Contains("EditCarePlan")) {
                        return $"Id_{window.Id}_EditCarePlan_items_{window.Items.Count}";
                    }
                    return $"Id_{window.Id}_{iTabPage.Name.Replace(" ","")}_items_{window.Items.Count}";
                }
                return $"Id_{window.Id}_items_{window.Items.Count}";
            }
            if (window.Name != "") {
                return $"Name_{window.Name.Replace(" ","")}_items_{window.Items.Count}";
            }
            if(window.PrimaryIdentification != "") {
                return $"Prime_{window.PrimaryIdentification}_items_{window.Items.Count}";
            }
            return $"LOC_{window.Location.X}_{window.Location.Y}_items_{window.Items.Count}";
        }
        
        public void click(Window activeWindow) {            
            utilityTools.DebugWindowItems(activeWindow);                                                
        }
        
    }
}

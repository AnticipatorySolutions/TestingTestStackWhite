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

namespace TestStackTest {
    public static class Session {
        //SUT Instance
        public static Application App; 

        //Archive of Paths to Window Items Archives
        public static Dictionary<string, WindowPath> WindowRoutes;

        //Constants
        public static Context SystemContext = new Context();
        
        public static ReportingTools ReportTools;
        public static UtilityTools UtilityTools = new UtilityTools();
        public static WindowTools WindowTools = new WindowTools();
        public static XmlTools XmlTools = new XmlTools();


        static Session() {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(Context.exeLocation) {WorkingDirectory = Context.workingDirectory};
            App = Application.Launch(processStartInfo);
            WindowRoutes = XmlTools.ReadWindowArchive(Context.trackingLocation, "test.xml");
            


        }

        public static void ShutDownSUT() 
        {
            App.Close();
            App.Dispose();
        }


    }

    public class Context 
    {
        public const string exeLocation = @"C:\development\Jordan\CBIBilling\IMS-V3.70\DataCenter\Desktop\bin\Debug\CBI.DataCenter.Desktop.exe"; 
        public const string workingDirectory = @"C:\development\Jordan\CBIBilling\IMS-V3.70\DataCenter\Desktop\bin\Debug\";
        public const string testDataLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\TestData.xlsx";
        public const string trackingLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\";
        public const string windowMapLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\test.xml";
    }



}

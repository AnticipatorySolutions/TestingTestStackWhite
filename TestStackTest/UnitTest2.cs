using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelToolSet;
using System.Collections.Generic;
using TestStackTest.TestBehaviours;
using GenerateXml;
using TestStackTest.TestBehaviours;

namespace TestStackTest {
    [TestClass]
    public class UnitTest2 {
        private const string testDataLocation = @"S:\Dev_IMS\Development\Jordan\uiAutomation\TestData.xlsx";
        [TestMethod]
        public void TestMethod1() {

            //            XmlToolSet xts = new XmlToolSet();
            //          xts.Read();


            UtilityTools ut = new UtilityTools();

            Console.WriteLine(ut.tester(@"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\test.xml", "product", ""));



            //S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\test.xml


            //var x = new QA_Simulator.TestManager(true);

            /*
            ExcelReader excelReader = new ExcelReader(testDataLocation);
            ExcelReader.ReturnObject dataSet = excelReader.ReadSheet(1, 51);
            List<string> details = dataSet.results[0];
            Dictionary<string, int> headers = dataSet.headers;

            Model_AddClientCase clientDetails = new Model_AddClientCase(details, headers);

            foreach (var x in clientDetails.mandatoryTextBoxes) {
                Console.WriteLine($"{x.Key} {x.Value}");

            }
            */


        }

    }

}
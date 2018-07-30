using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStackTest;


namespace QA_Simulator
{
    public class TestManager
    {
        private const string exeLocation = @"C:\development\Jordan\CBIBilling\IMS-V3.6\DataCenter\Desktop\bin\Debug\CBI.DataCenter.Desktop.exe";
        private const string workingDirectory = @"C:\development\Jordan\CBIBilling\IMS-V3.6\DataCenter\Desktop\bin\Debug";


        //Run Test sets in constructor
        public TestManager() {
            InitializeSystemUnderTest SUT = new InitializeSystemUnderTest(exeLocation, workingDirectory);
            Login loginText = new Login(SUT.application);


        }


    }
}

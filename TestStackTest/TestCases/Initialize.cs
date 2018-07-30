using TestStack.White;
using System.Diagnostics;


namespace TestStackTest.TestCases {
   public class InitializeSystemUnderTest {
        public Application application = null;
        public string exeLocation = null;
        public string workingDirectory = null;        
        public InitializeSystemUnderTest(string exeAddress, string workingDir){
            ProcessStartInfo processStartInfo = new ProcessStartInfo(exeAddress) {WorkingDirectory = workingDir};
            exeLocation = exeAddress;
            workingDirectory = workingDir;
            application = Application.Launch(processStartInfo);                        
        }
    }

    public class ShutDownSystemUnderTest {
        public ShutDownSystemUnderTest(Application application) {
            application.Close();
            application.Dispose();
        }
    
    }
    
}

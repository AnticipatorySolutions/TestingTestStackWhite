using System.Collections.Generic;

namespace TestStackTest.Abstracts {
    public abstract class TestCommands : ITestCommands {
        private List<string> _Commands = new List<string>();
        public List<string> Commands { get { return _Commands; } set { _Commands = value; } }
    }
}

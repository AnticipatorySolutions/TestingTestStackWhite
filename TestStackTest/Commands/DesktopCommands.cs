using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class Desktop_ClickButton : TestCommands {
        enum CommandEnum { click_clients_button }
        public Desktop_ClickButton() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

    public class Desktop_ClickTab : TestCommands {
        enum CommandEnum { click_tabControl_tabPage }
        public Desktop_ClickTab() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }        
    }
    
}

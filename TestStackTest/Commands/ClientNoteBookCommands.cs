using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class ClientNoteBook_ClickButton : TestCommands {
        enum CommandEnum { click_clients_button }
        public ClientNoteBook_ClickButton() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

    public class ClientNoteBook_ClickTab : TestCommands {
        enum CommandEnum { click_tabControl_tabPage }
        public ClientNoteBook_ClickTab() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

}

using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class ClientCaseSearch_SearchLastName : TestCommands {
        enum CommandEnum { write_criteria_winFormTextBox, click_lastName_button }

        public ClientCaseSearch_SearchLastName() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

    public class ClientCaseSearch_ClickAddNewClientButton : TestCommands {
        enum CommandEnum { click_newClient_button }

        public ClientCaseSearch_ClickAddNewClientButton() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

    public class ClientCaseSearch_ClickOkButton : TestCommands {
        enum CommandEnum { click_ok_button }

        public ClientCaseSearch_ClickOkButton() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }



}

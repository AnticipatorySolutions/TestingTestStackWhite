using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class LoginDefault : TestCommands {
        enum CommandEnum { write_username_textBox, write_password_textBox, doubleClick_ok_button }
        public LoginDefault() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }
}

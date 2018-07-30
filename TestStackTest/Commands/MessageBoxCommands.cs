using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class MessageBoxDefault : TestCommands {
        enum CommandEnum { doubleClick_ok_button }

        public MessageBoxDefault() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }
}

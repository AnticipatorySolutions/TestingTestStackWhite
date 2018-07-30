using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class SelectVisitCompletionTypeTime_Cancelled : TestCommands {
        enum CommandEnum { click_type_comboBox, click_reason_comboBox, write_note_textBox, click_ok_button }
        public SelectVisitCompletionTypeTime_Cancelled() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }
}


using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class EditCarePlanRevisionCommands : TestCommands {
        enum CommandEnum { write_endDate_datePicker, click_ok_button}
        public EditCarePlanRevisionCommands() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

}

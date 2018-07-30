using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class EditCarePlan_ClickRevisionButton : TestCommands {
        enum CommandEnum { click_editRevision_button }
        public EditCarePlan_ClickRevisionButton() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }


}


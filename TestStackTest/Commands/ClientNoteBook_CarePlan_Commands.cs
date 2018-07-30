using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class ClientNoteBook_CarePlan_EditCarePlan : TestCommands {
        enum CommandEnum { click_editCarePlan_button }
        public ClientNoteBook_CarePlan_EditCarePlan() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

    public class ClientNoteBook_CarePlan_SchedulingAssistant : TestCommands {
        enum CommandEnum { click_fillAssignments_button }
        public ClientNoteBook_CarePlan_SchedulingAssistant() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }


}

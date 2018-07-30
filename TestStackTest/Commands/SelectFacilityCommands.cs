using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class SelectFacilityDefault : TestCommands {
        enum CommandEnum { click_facility_radioButton, click_divisions_comboBox_criteria, click_listView_listView_criteria, click_ok_button, }
        public SelectFacilityDefault() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }
}

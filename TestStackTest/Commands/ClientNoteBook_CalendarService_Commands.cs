using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    public class ClientNoteBook_CalendarService_ClickOneDay : TestCommands {
        enum CommandEnum { click_showOneDay_button}
        public ClientNoteBook_CalendarService_ClickOneDay() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }

    public class ClientNoteBook_CalendarService_DoubleClickPoint : TestCommands {
        enum CommandEnum { doubleClick_point }
        public ClientNoteBook_CalendarService_DoubleClickPoint() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }
    }


}

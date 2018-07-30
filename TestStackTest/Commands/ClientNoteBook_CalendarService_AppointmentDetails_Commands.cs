using System;
using System.Collections.Generic;
using TestStackTest.Abstracts;

namespace TestStackTest.Commands {
    class ClientNoteBook_CalendarService_AppointmentDetails_ClickVisitCompletion : TestCommands {
        enum CommandEnum { click_visitCompletion_button }
        public ClientNoteBook_CalendarService_AppointmentDetails_ClickVisitCompletion() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }

    }

    class ClientNoteBook_CalendarService_AppointmentDetails_ClickCloseButton : TestCommands {
        enum CommandEnum { click_close_button }
        public ClientNoteBook_CalendarService_AppointmentDetails_ClickCloseButton() {
            Commands = new List<string>();
            foreach (string name in Enum.GetNames(typeof(CommandEnum))) { Commands.Add(name); }
        }

    }



}

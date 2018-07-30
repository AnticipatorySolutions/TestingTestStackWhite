using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStackTest.Abstracts {
    public enum ActionType {
        focus,
        select,
        click,
        doubleClick,
        read,
        write,
        invoke,
        custom
    }

    public enum ControlType {
        button,
        radioButton,
        tab,
        textBox,
        winFormTextBox,
        winFormComboBox,
        listView,
        checkBox,        
        custom,
        dateTimePicker,
        keyboard,
        point
    }

    
    public enum WindowNames {
        Login,
        SelectFacility,
        SearchClientCases,
        CBI,  //Modal MessageBox
        EditCarePlanRevision,
        AppointmentDetails,
        SelectVisitCompletionTypeTime 
    }

    
    public enum VisitCompletionReasons {
                AUTHP, //completed
                LONGAUTH,
                LONGUNAUTH,
                NORMAL,
                SHORTCLI,
                SHORTUNAUTH,
                UNAUTHP,
                MVINT, //missed
                MVNOSHOW,
                MVNOSTAFF,
                MVSYS,
                MVWEATHER,
                NSNFCONFIRM, //nsnf
                NSNFNOCONFIRM,
                CANADJ, //cancelled
                CANDECEASE,
                CANFUNDER,
                CANINT,
                CANNONOTICE,
                CANNOTICE,
                CANRISK
   }

    public enum VisitCompletionTypes {
        VisitCompleted,        
        MissedVisit,
        NSNF,
        VisitCancelled
    }



    public enum ClientNoteBookTabs {
        Client,
        Calendar,
        CarePlans,
        Risks,
        Delegations,
        Financials,
        Tasks
    }

    public enum DesktopTabs {
        Tasks,
        EditCarePlan,
        SA


    }


}

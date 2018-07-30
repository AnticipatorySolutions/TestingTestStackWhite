using System;
using TestStack.White;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using System.Windows;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.Custom;
using ExcelToolSet;
using Castle.Core.Logging;
using TestStackTest.TestBehaviours;
using TestStackTest.Controls;
using System.Windows.Automation;

namespace TestStackTest.Abstracts {



    //Sixth Order
    public interface ITestView : IView {
        ControlFactory controlFactory {get;}        
        void GetWindow(string windowName);
    }
    
    //Fourth Order

    public interface IControl : IAction, IAssign {
        Window window { get; set; }
        Point point { get; set; }
        ControlType controlType { get; set; }
        IUIItem control { get; set; }        
        AutomationElement element { get; set; }
    }

    //Third Order
    public interface IAction : ISetUp, ITearDown, IVerify 
    {
        ActionType actionType { get; set;}
        void Action();
    }

    public interface IView : IApplication {
        
        Window window { get; set; }
    }

    //Second Order

    public interface ISetUp : IReport, ICriteria
    {
        void SetUp(ControlType control);
        void SetUp(ControlType control, ActionType actionType);
        void SetUp(ControlType control, ActionType actionType, String Criteria);
    }

    public interface IVerify : IReport 
    {
        void Verify();   
    }
    public interface ITearDown : IReport 
    {
        void TearDown();
    }

    public interface IApplication : IReport {
        Application application { get; set; }
    }

    public interface IFactory : IReport
    {
        
        IControl Produce(ControlType controlType);
        IControl Produce(ControlType controlType, ActionType actionType);
        IControl Produce(ControlType controlType, ActionType actionType, string Criteria);
        IControl Assign(IUIItem item, ControlType controlType, ActionType actionType);
        IControl Assign(IUIItem item, ControlType controlType, ActionType actionType, string Criteria);
        IControl Assign(AutomationElement element, ControlType controlType, ActionType actionType);
        IControl Assign(AutomationElement element, ControlType controlType, ActionType actionType, string Criteria);
        
    }

    //Most Primitive

    public interface ITestCommands {
        List<string> Commands { get; set; }
    }

    public interface IReport {
        List<string> reportDetails { get; set; }
        void Report();
    }

    public interface ICriteria {
       string criteria { get; set; }
    }

    public interface IAssign {
        void Assign(IUIItem item);
        void Assign(AutomationElement element);
    }
}

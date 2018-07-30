using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using System.Windows.Automation;
using System.Windows;
using TestStack.White.UIItems.WindowItems;



namespace TestStackTest.Abstracts {

    public class ControlBase : IControl {
        private ActionType _actionType;
        private IUIItem _control;
        private AutomationElement _automationElement;
        private ControlType _controlType;
        private List<string> _reportDetails;
        private string _criteria = "";
        private Point _point;
        private Window _window;

        public ActionType actionType {
            get {
                return _actionType;
            }

            set {
                _actionType = value;
            }
        }

        public Window window {
            get { return _window; }
            set { _window = value; }
        }


        public IUIItem control {
            get {
                return _control;
            }

            set {
                _control = value;
            }
        }

        public AutomationElement element {
            get {
                return _automationElement;
            }

            set {
                _automationElement = value;
            }
        }

        public ControlType controlType {
            get {
                return _controlType;
            }

            set {
                _controlType = value;
            }
        }

        public List<string> reportDetails {
            get {
                return _reportDetails;
            }

            set {
                _reportDetails.Concat(value);
            }
        }
        
        public string criteria {
            get {
                return _criteria;
            }

            set {
                _criteria = value;
            }
        }
        public Point point {
            get {
                return _point;
            }

            set {
                _point = value;
            }
        }

        public virtual void Action() { }
        
        public void Assign(IUIItem item) {
            control = item;
        }

        public void Assign(AutomationElement element) {
            _automationElement = element;
        }

        public virtual void Report() {
            foreach (string report in _reportDetails) {
                Console.WriteLine(report);
            }
        }
        



        public virtual void SetUp(ControlType control) {
            controlType = control;            
        }

        public virtual void SetUp(ControlType control, ActionType action) {
            controlType = control;
            actionType = action;
        }

        public virtual void SetUp(ControlType control, ActionType action, string Criteria) {
            controlType = control;
            actionType = action;
            criteria = Criteria;
        }
        public virtual void SetUp(ControlType control, ActionType action, Point Criteria) {
            controlType = control;
            actionType = action;
            point = Criteria;
        }

        public virtual void TearDown() {}

        public virtual void Verify() {}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestStack.White.UIItems;
using TestStackTest.Abstracts;


namespace TestStackTest.Controls {
    public class ControlFactory : IFactory {
        private List<string> _reportDetails;
        public List<string> reportDetails {
            get {
                return _reportDetails;
            }

            set {
                _reportDetails.Concat(value);
            }
        }

        public IControl Assign(System.Windows.Automation.AutomationElement element, ControlType controlType, ActionType actionType) {
            IControl x = Produce(controlType, actionType);
            x.Assign(element);
            return x;            
        }

        public IControl Assign(IUIItem item, ControlType controlType, ActionType actionType) {
            IControl x = Produce(controlType, actionType);
            x.Assign(item);
            return x;
        }

        public IControl Assign(System.Windows.Automation.AutomationElement element, ControlType controlType, ActionType actionType, string Criteria) {
            IControl x = Produce(controlType, actionType, Criteria);
            x.Assign(element);
            return x;
        }

        public IControl Assign(IUIItem item, ControlType controlType, ActionType actionType, string Criteria) {
            IControl x = Produce(controlType, actionType, Criteria);
            x.Assign(item);
            return x;
        }

        public IControl Produce(ControlType controlType) {
            switch (controlType) {
                case ControlType.tab: return new TabControl(controlType);
                case ControlType.button: return new ButtonControl(controlType);
                case ControlType.textBox: return new TextBoxControl(controlType);
                case ControlType.winFormTextBox: return new WinFormTextBoxControl(controlType);
                case ControlType.checkBox: return new CheckBoxControl(controlType);
                case ControlType.radioButton: return new RadioButtonControl(controlType);
                case ControlType.winFormComboBox: return new WinFormComboBoxControl(controlType);
                case ControlType.listView: return new ListViewControl(controlType);
                case ControlType.dateTimePicker: return new DateTimePickerControl(controlType);
                case ControlType.keyboard: return new KeyboardControl(controlType);
                case ControlType.point: return new PointControl(controlType);
                default: return default(ControlBase);
            }
        }

        public IControl Produce(ControlType controlType, ActionType actionType) {
            switch (controlType) {
                case ControlType.tab: return new TabControl(controlType, actionType);
                case ControlType.button: return new ButtonControl(controlType, actionType);
                case ControlType.textBox: return new TextBoxControl(controlType, actionType);
                case ControlType.winFormTextBox: return new WinFormTextBoxControl(controlType, actionType);
                case ControlType.checkBox: return new CheckBoxControl(controlType, actionType);
                case ControlType.radioButton: return new RadioButtonControl(controlType, actionType);
                case ControlType.winFormComboBox: return new WinFormComboBoxControl(controlType, actionType);
                case ControlType.listView: return new ListViewControl(controlType, actionType);
                case ControlType.dateTimePicker: return new DateTimePickerControl(controlType, actionType);
                case ControlType.keyboard: return new KeyboardControl(controlType, actionType);
                case ControlType.point: return new PointControl(controlType, actionType);
                default: return default(ControlBase);
            }
        }

        public IControl Produce(ControlType controlType, ActionType actionType, string criteria) {
            switch (controlType) {
                case ControlType.tab: return new TabControl(controlType, actionType, criteria);
                case ControlType.button: return new ButtonControl(controlType, actionType);
                case ControlType.textBox: return new TextBoxControl(controlType, actionType, criteria);
                case ControlType.winFormTextBox: return new WinFormTextBoxControl(controlType, actionType, criteria);
                case ControlType.checkBox: return new CheckBoxControl(controlType, actionType);
                case ControlType.radioButton: return new RadioButtonControl(controlType, actionType, criteria);
                case ControlType.winFormComboBox: return new WinFormComboBoxControl(controlType, actionType, criteria);
                case ControlType.listView: return new ListViewControl(controlType, actionType, criteria);
                case ControlType.dateTimePicker: return new DateTimePickerControl(controlType, actionType, criteria);
                case ControlType.keyboard: return new KeyboardControl(controlType, actionType, criteria);
                default: return default(ControlBase);
            }
        }



        public virtual void Report() {
            foreach (string report in _reportDetails) {
                Console.WriteLine(report);
            }
        }
    }



}

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
using System.Windows.Automation;


namespace TestStackTest.TestBehaviours {
    public class WindowTools {
        public AutomationElement GetFirstDescendant(
          AutomationElement root,
          Func<AutomationElement.AutomationElementInformation, bool> condition) {
            var walker = TreeWalker.ControlViewWalker;
            var element = walker.GetFirstChild(root);
            while (element != null) {
                if (condition(element.Current)) {

                    return element;
                }
                var subElement = GetFirstDescendant(element, condition);
                if (subElement != null) {
                    return subElement;
                }
                element = walker.GetNextSibling(element);
            }
            return null;
        }

        public AutomationElement GetFirstChild(
            AutomationElement root,
            Func<AutomationElement.AutomationElementInformation, bool> condition) {
            var walker = TreeWalker.ControlViewWalker;
            var element = walker.GetFirstChild(root);

            while (element != null) {
                if (condition(element.Current)) {
                    return element;
                }
                element = walker.GetNextSibling(element);
            }
            return null;
        }
        public void waitForLoadingWindowToClose(Application application, int expectedCount) {
            int count;
            do {
                count = application.GetWindows().Count;
            } while (count > expectedCount);

        }

        public TabPages getTabPages(IUIItem container) {
            Tab containingTab = (Tab)container;
            TabPages tabPages = containingTab.Pages;
            return tabPages;
        }

        public ITabPage getTab(TabPages pages, string tabName) {
            ITabPage page = pages.Where(x => x.Name == tabName).FirstOrDefault();
            return page;
        }

        public List<IUIItem> getTabItems<T>(ITabPage page) where T : IUIItem, new() {
            TabPage tabPage = (TabPage)page;
            List<IUIItem> items = tabPage.Items.Where(x => x.GetType() == typeof(T)).ToList();
            return items;
        }

        public WinFormComboBox GetWinFormComboBox(Window window, string id) {
            return (WinFormComboBox)window.Items.Where(x => x.Id == id).FirstOrDefault();
        }


        public void MouseClickPoint(Window window, Point point) {
            window.Mouse.Location = point;
            window.Mouse.Click();

        }

        public void MouseDoubleClickPoint(Window window, Point point) {
            window.Mouse.Location = point;
            window.Mouse.DoubleClick(point);

        }



        public void MouseClickPointEnterText_VeryVeryBad(Window window, Point point, string text) {
            MouseClickPoint(window, point);
            window.Keyboard.Enter(text);
        }


        public Window GetWindow(Application application, string windowTitle) {
            return application.GetWindow(windowTitle);
        }

        public Window GetWindow(Application application, SearchCriteria searchCriteria) {
            return application.GetWindow(searchCriteria, InitializeOption.NoCache);

        }

        public Window GetWindow(List<Window> windows, string id) {
            return windows.Where(x => x.Id == id).FirstOrDefault();
        }


        public List<Window> GetWindows(Application application) {
            List<Window> windows = application.GetWindows();
            return windows;
        }

        public UIItemCollection GetWindowItems(Window window) {
            return window.Items;
        }


        public TUIItem GetIUIItem<TUIItem>(Window window, SearchCriteria searchCriteria) where TUIItem : IUIItem, new() {
            return window.Get<TUIItem>(searchCriteria);
        }



        public CheckBox GetCheckBox(List<IUIItem> items, string name) {
            IUIItem item = items.Where(x => x.Name == name).FirstOrDefault();
            return (CheckBox)item;
        }


        public Panel GetPanel(Window window, string name) {
            return (Panel)window.Items.Where(x => x.Name == name && x.GetType() == typeof(Panel)).FirstOrDefault();
        }



        public Button GetButton(Window window, SearchCriteria searchCriteria) {
            Button button = window.Get<Button>(searchCriteria);
            return button;
        }

        public Button GetButton(Window window, string name) {
            return (Button)window.Items.Where(x =>( x.Name == name || x.Name.Replace(" ","").Contains(name)) && x.GetType() == typeof(Button)).FirstOrDefault();
        }

        public Button GetButton(Window window, System.Drawing.Color color) {
            return (Button)window.Items.Where(x => x.BorderColor == color && x.GetType() == typeof(Button)).FirstOrDefault();            
        }


        public RadioButton GetRadioButton(Window window, SearchCriteria searchCriteria) {
            RadioButton radioButton = window.Get<RadioButton>(searchCriteria);
            return radioButton;
        }


        public DateTimePicker GetDateTimePicker(Window window, SearchCriteria searchCriteria) {
            DateTimePicker picker = window.Get<DateTimePicker>(searchCriteria);
            return picker;
        }



        public ListBox GetListBox(Window window, SearchCriteria searchCriteria) {
            ListBox listBox = window.Get<ListBox>(searchCriteria);
            return listBox;
        }

        public void SelectListBoxItem(ListBox listBox, string itemText) {
            listBox.Item(itemText).Select();
        }

        public ListView GetListView(Window window, SearchCriteria searchCriteria) {
            ListView listView = window.Get<ListView>(searchCriteria);
            return listView;
        }

        public ListViewRow SelectListViewRowName(ListView listView, string rowName) {
            ListViewRow row = listView.Rows.Where(x => x.Name.Contains(rowName)).FirstOrDefault();
            row.Select();
            return row;
        }



        public Boolean PostText(IUIItem item, string text) {
            if (item.Enabled) {
                item.Focus();
                item.Enter(text);
                return true;
            } else {
                return false;
            }

        }

        public Boolean UpdateComboBox(WinFormComboBox box, string value) {
            if (box.Enabled) {
                box.Click();
                ListItems items = box.Items;
                try {
                    box.Select(value);
                    
                } catch {
                    box.Select(items.Count - 1);

                    return false;
                }
                return true;
            } else { return false; }
        }

        public Boolean UpdateComboBox(WinFormComboBox box, int index) {
            if (box.Enabled) {
                box.Click();
                ListItems items = box.Items;
                try {
                    box.Select(index);

                } catch {
                    box.Select(items.Count - 1);

                    return false;
                }
                return true;
            } else { return false; }
        }




        public List<IUIItem> GetIUIItemList<UIItem>(Window window) {
            List<IUIItem> items = window.Items.Where(x => x.GetType() == typeof(UIItem)).ToList();
            return items;
        }


        public void ClickIUIItem(IUIItem item) {
            item.Click();
        }

        public void DoubleClickIUItem(IUIItem item) {
            item.DoubleClick();
        }

        public void setDateInCheckedPicker(Window window, DateTimePicker picker, string date) {
            picker.Focus();
            window.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);

            window.Keyboard.Enter(date);
        }

        public bool verifyContents(Dictionary<string, string> dictionary) {
            foreach (var x in dictionary) {
                if (x.Value.Length == 0) { return false; }
            }
            return true;
        }

        public Dictionary<string, IUIItem> CreateTextBoxDictionary(UIItemCollection items) {
            List<IUIItem> textBoxes = items.Where(x => x.GetType() == typeof(WinFormTextBox) || x.GetType() == typeof(TextBox)).ToList();
            Dictionary<string, IUIItem> TextBoxes = new Dictionary<string, IUIItem>();
            foreach (IUIItem x in textBoxes) {
                TextBoxes.Add(x.Id, x);
            }
            return TextBoxes;
        }

        public Dictionary<string, IUIItem> CreateComboBoxDictionary(UIItemCollection items) {
            List<IUIItem> comboBoxes = items.Where(x => x.GetType() == typeof(WinFormComboBox) || x.GetType() == typeof(ComboBox)).ToList();
            Dictionary<string, IUIItem> ComboBoxesDictionary = new Dictionary<string, IUIItem>();
            foreach (IUIItem x in comboBoxes) {
                ComboBoxesDictionary.Add(x.Id, x);
            }
            return ComboBoxesDictionary;
        }

        public Dictionary<string, IUIItem> CreateDatePickerDictionary(UIItemCollection items) {
            List<IUIItem> datePicker = items.Where(x => x.GetType() == typeof(DateTimePicker)).ToList();
            Dictionary<string, IUIItem> DatePickerDictionary = new Dictionary<string, IUIItem>();
            foreach (IUIItem x in datePicker) {
                DatePickerDictionary.Add(x.Id, x);
            }
            return DatePickerDictionary;
        }




        public void UpdateItemsFromDictionary(Dictionary<string, string> values, Dictionary<string, IUIItem> items) {
            IUIItem result;
            foreach (KeyValuePair<string, string> dictionaryItem in values) {
                if (items.TryGetValue(dictionaryItem.Key, out result)) {
                    PostText(result, dictionaryItem.Value);
                }
            }
        }

        public void UpdateComboBoxesFromDictionary(Dictionary<string, string> values, Dictionary<string, IUIItem> items) {
            IUIItem result;
            WinFormComboBox box;
            foreach (KeyValuePair<string, string> dictionaryItem in values) {
                if (items.TryGetValue(dictionaryItem.Key, out result)) {
                    box = (WinFormComboBox)result;
                    UpdateComboBox(box, dictionaryItem.Value);
                }
            }
        }

        public void UpdateDatePickersFromDictionary(Dictionary<string, string> values, Dictionary<string, IUIItem> items, Window window) {
            IUIItem result;
            DateTimePicker picker;
            foreach (KeyValuePair<string, string> dictionaryItem in values) {
                if (items.TryGetValue(dictionaryItem.Key, out result)) {
                    picker = (DateTimePicker)result;
                    setDateInCheckedPicker(window, picker, dictionaryItem.Value);
                }
            }

        }

        public void PostToTextBoxes(UIItemCollection items, Dictionary<string, string> valueDictionary) {
            UpdateItemsFromDictionary(valueDictionary, CreateTextBoxDictionary(items));
        }
        public void PostToComboBoxes(UIItemCollection items, Dictionary<string, string> valueDictionary) {
            UpdateComboBoxesFromDictionary(valueDictionary, CreateComboBoxDictionary(items));
        }
        public void PostToDatePicker(UIItemCollection items, Dictionary<string, string> valueDictionary, Window window) {
            UpdateDatePickersFromDictionary(valueDictionary, CreateDatePickerDictionary(items), window);
        }

        public TabPage GetTabPage(Tab tab, string name) {            
            ITabPage itp = tab.Pages.Where(x => x.Name == name).FirstOrDefault();
            return (TabPage)itp;
        }

        public void GetAndSelectTabPage(Tab tab, string name) {
            TabPage page = GetTabPage(tab, name);
            page.Select();
        }



    }

}

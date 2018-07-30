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
using GenerateXml;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using TestStackTest.TestBehaviours;


namespace TestStackTest {
    public class UtilityTools {


        public void reviewSupportedPatterns(AutomationElement element) {
            AutomationPattern[] patterns = element.GetSupportedPatterns();
            foreach (AutomationPattern pattern in patterns) {
                Console.WriteLine($"programmaticName {pattern.ProgrammaticName}");
                Console.WriteLine($"pattern name {Automation.PatternName(pattern)}");
            }
        }

        public AutomationElement GetFirstDescendant(
                AutomationElement root,
                Func<AutomationElement.AutomationElementInformation, bool> condition) {
            var walker = TreeWalker.ControlViewWalker;
            var element = walker.GetFirstChild(root);
            while (element != null) {
                Console.WriteLine($"");
                Console.WriteLine($"New Descendant");
                Console.WriteLine($"Type {element.Current.GetType().ToString()}");
                Console.WriteLine($"Name {element.Current.Name}");
                Console.WriteLine($"Class Name {element.Current.ClassName}");
                Console.WriteLine($"AutomationID {element.Current.AutomationId}");
                Console.WriteLine($"Bounds {element.Current.BoundingRectangle}");
                Console.WriteLine($"Control Type {element.Current.ControlType}");

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
                Console.WriteLine($"");
                Console.WriteLine($"NewChild");
                Console.WriteLine($"Type {element.Current.GetType().ToString()}");
                Console.WriteLine($"Name {element.Current.Name}");
                Console.WriteLine($"Class Name {element.Current.ClassName}");
                Console.WriteLine($"AutomationID {element.Current.AutomationId}");
                Console.WriteLine($"Bounds {element.Current.BoundingRectangle}");
                Console.WriteLine($"Control Type {element.Current.ControlType}");
                if (condition(element.Current)) {
                    return element;
                }
                element = walker.GetNextSibling(element);
            }
            return null;
        }

        public void TreeWalkerDesktop(AutomationElement root) {

            var walker = TreeWalker.RawViewWalker;
            var element = walker.GetFirstChild(root);

            while (element != null) {
                Console.WriteLine($"");
                Console.WriteLine($"Automation ID {element.Current.AutomationId}");
                Console.WriteLine($"class name {element.Current.ClassName}");
                Console.WriteLine($"control type {element.Current.ControlType}");
                Console.WriteLine($"framework id {element.Current.FrameworkId}");
                Console.WriteLine($"is content {element.Current.IsContentElement}");
                Console.WriteLine($"is control {element.Current.IsControlElement}");
                Console.WriteLine($"name {element.Current.Name}");
                Console.WriteLine($"process id {element.Current.ProcessId}");
                Console.WriteLine($"native window handle {element.Current.NativeWindowHandle}");

                if (walker.GetFirstChild(element) != null) {
                    element = walker.GetFirstChild(element);
                    TreeWalkerDesktop(element);
                }


                element = walker.GetNextSibling(element);
            }

        }

        public void ReviewProcesses() {
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes) {
                if (!String.IsNullOrEmpty(p.MainWindowTitle)) {
                    Console.WriteLine(p.MainWindowTitle);
                    Console.WriteLine(p.ProcessName);



                    foreach (ProcessThread t in p.Threads) {
                        if (!String.IsNullOrEmpty(t.ToString())) {
                            Console.WriteLine("");
                            Console.WriteLine($"thread id {t.Id}");
                            Console.WriteLine($"{t.ToString()}");
                        };
                    }
                }
            }



        }
        public void TreeScopeDesktop(AutomationElement root) {
            var children = root.FindAll(TreeScope.Children, Condition.TrueCondition);

            foreach (AutomationElement element in children) {
                Console.WriteLine($"");
                Console.WriteLine($"Automation ID {element.Current.AutomationId}");
                Console.WriteLine($"class name {element.Current.ClassName}");
                Console.WriteLine($"control type {element.Current.ControlType}");
                Console.WriteLine($"framework id {element.Current.FrameworkId}");
                Console.WriteLine($"is content {element.Current.IsContentElement}");
                Console.WriteLine($"is control {element.Current.IsControlElement}");
                Console.WriteLine($"name {element.Current.Name}");
                Console.WriteLine($"process id {element.Current.ProcessId}");
                Console.WriteLine($"native window handle {element.Current.NativeWindowHandle}");
            }

        }


        public bool CheckPath(string path) {
            if (!File.Exists(path)) { Console.WriteLine("Site Map Path Invalid"); return false; }
            return true;
        }

        public XElement GetSiteMap(string path) {
            XElement siteMap = XElement.Load(path);
            return siteMap;
        }

        public List<XElement> GetXmlElements(XElement group, string elementName) {
            XName element = XName.Get(elementName);
            return group.Elements(element).ToList();
        }

        public string CheckForAttribute(XElement element, string attributeName) {
            XName attribute = XName.Get(attributeName);
            return element.Attribute(attribute).Value;
        }

        public bool tester(string path, string elementName, string attributeName) {
            if (!CheckPath(path)) { return false; }
            foreach (var x in GetXmlElements(GetSiteMap(path), elementName).ToList()) {
                CheckForAttribute(x, attributeName);

            }
            return false;
        }

        public bool IsWindowMapped(string windowName, string path) {
            XElement map = XElement.Load(path);
            if (GetXmlElements(GetSiteMap(path), windowName).ToList().Count > 0) { return true; };
            return false;
        }


        public void serializeWindow(Window window, string windowName, string windowPath, string itemPath) {
            XmlToolSet ts = new XmlToolSet();
            string entryName;
            if (!CheckPath(windowPath)) {
                XmlDocument document = ts.newDoc("Windows");
                document.Save(windowPath);
            }
            if (!IsWindowMapped(windowName, windowPath)) {
                XmlDocument doc = new XmlDocument();
                doc.Load(windowPath);
                XmlNode baseNode = ts.CreateElement(doc, windowName);


                entryName = getWindowDetails(window);

                XmlNode node = ts.CreateElement(doc, entryName);
                XmlAttribute itemCount = ts.CreateAttribute(doc, "itemCount", window.Items.Count.ToString());
                node = ts.AddAttribute(node, itemCount);
                XmlAttribute isModal = ts.CreateAttribute(doc, "isModal", window.IsModal.ToString());
                node = ts.AddAttribute(node, isModal);
                XmlAttribute bounds = ts.CreateAttribute(doc, "bounds", window.Bounds.ToString());
                node = ts.AddAttribute(node, bounds);

                itemPath = $"{itemPath}{windowName}.xml";

                XmlAttribute url = ts.CreateAttribute(doc, "url", itemPath);
                node = ts.AddAttribute(node, url);

                //baseNode.AppendChild(node);
                //doc = ts.AddNode(doc, baseNode);

                doc = ts.AddNode(doc, node);
                doc.Save(windowPath);
                Console.WriteLine("New Window Added");
                serializeWindowItems(window, windowName, itemPath);
                return;
            }
            Console.WriteLine("Window Already Added");
        }


        public string getWindowDetails(Window window) {
            string windowName = getWindowLabel(window);
            string windowTabs = getWindowTabNames(window);
            string windowItems = getWindowItems(window);
            string windowRadioButtons = getSelectedRadioButton(window);

            //Considering Radio Buttons as the primary condition
            if (windowRadioButtons.Length != 0) { return $"{windowName}_{windowRadioButtons}_{windowItems}";}

            if (windowTabs.Length != 0) { return $"{windowName}{windowTabs}_{windowItems}"; } else { return $"{windowName}_{windowItems}"; }
        }


        public string getWindowItems(Window window) {
            string temp;
            int enabled = window.Items.Where(x => x.Enabled == true).ToList().Count;
            int total = window.Items.Count;
            temp = $"{enabled}_{total}";
            return temp;
        }

        public string getSelectedRadioButton(Window window) 
        {
            WindowTools wt = new WindowTools();
            List<IUIItem> items = wt.GetIUIItemList<RadioButton>(window);
            RadioButton button;
            foreach (IUIItem item in items) {
                button = (RadioButton)item;
                if (button.IsSelected) {
                    return button.Id;
                }
            }            
            return "";
        }



        public string getWindowLabel(Window window) {
            string tempName;
            if (window.Id != "") {
                tempName = $"{FilterName(window.Id)}";
            } else if (window.Name != "") {
                tempName = $"{FilterName(window.Name)}";
            } else if (window.PrimaryIdentification != "") {
                tempName = $"{FilterName(window.PrimaryIdentification)}";
            } else {
                tempName = $"{window.Location.X}_{window.Location.Y}";
            }
            return tempName;
        }
        

        public string getWindowTabNames(Window window) {
            string temp = "";
            WindowTools wt = new WindowTools();

            List<IUIItem> items = wt.GetIUIItemList<Tab>(window);
            Tab tab;
            TabPage tabPage;
            int track = 0;
            List<string> activeTabPages = new List<string>();
            if (items.Count > 0) {
                foreach (IUIItem item in items) {
                    tab = (Tab)item;
                    tabPage = (TabPage)tab.Pages.Where(page => page.IsSelected).FirstOrDefault();
                    
                    if (window.Id.Contains("Desktop") && track == 0) {
                        if (items.Count == 1) { activeTabPages.Add(FilterName(tabPage.Name)); }
                    }
                    else if (window.Id.Contains("Desktop") && track == 1) {
                        if (track == 1) {
                            switch (tab.Pages.Count) {
                                case 7:
                                    if (tab.Pages[0].Name.Contains("Client")) {
                                        activeTabPages.Add("ClientNoteBook");
                                        break;
                                    } else if (tab.Pages[0].Name.Contains("Volume")) {
                                        activeTabPages.Add("OrganizationNoteBook");
                                        break;
                                    } else { break; }
                                case 14:
                                    activeTabPages.Add("ProviderNoteBook");
                                    break;
                            }
                        }
                        activeTabPages.Add(FilterName(tabPage.Name));
                    } else {
                        activeTabPages.Add(FilterName(tabPage.Name));
                    }                    
                    track++;
                }

                if (activeTabPages.Count == 1) { return $"_{activeTabPages[0]}"; }

                foreach (string activeTabPage in activeTabPages) {
                    temp = $"{temp}_{activeTabPage}";
                }
            }
            return temp;
        }

        public string FilterName(string name) 
        {
            string temp = name.Replace(" ", "").Replace(@"/", "").Replace(",", "").Replace("‡", "");
            return temp;
        }


        /*
                public string getWindowDetails(Window window) {


                    if (window.Id != "") {
                        if (window.Id == "HomeHealthDesktop") {
                            WindowTools windowTools = new WindowTools();
                            Tab tab = (Tab)windowTools.GetIUIItemList<Tab>(window)[0];
                            ITabPage iTabPage = tab.Pages.Where(x => x.IsSelected).FirstOrDefault();
                            if (iTabPage.Name.Replace(" ", "").Contains("EditCarePlan")) {
                                return $"Id_{window.Id}_EditCarePlan_items_{window.Items.Count}";
                            }
                            return $"Id_{window.Id}_{iTabPage.Name.Replace(" ", "")}_items_{window.Items.Count}";
                        }
                        return $"Id_{window.Id}_items_{window.Items.Count}";
                    }
                    if (window.Name != "") {
                        return $"Name_{window.Name.Replace(" ", "")}_items_{window.Items.Count}";
                    }
                    if (window.PrimaryIdentification != "") {
                        return $"Prime_{window.PrimaryIdentification}_items_{window.Items.Count}";
                    }
                    return $"LOC_{window.Location.X}_{window.Location.Y}_items_{window.Items.Count}";
                }

                */
        public void serializeWindowItems(Window window, string windowName, string path) {
            XmlToolSet ts = new XmlToolSet();
            XmlDocument doc = ts.newDoc("Items");

            foreach (var item in window.Items) {
                string typeName = item.GetType().ToString();
                XmlNode node = ts.CreateElement(doc, typeName.Remove(0, typeName.LastIndexOf(".") + 1));
                XmlAttribute Label = ts.CreateAttribute(doc, "Label", windowName);
                node = ts.AddAttribute(node, Label);


                XmlAttribute Name = ts.CreateAttribute(doc, "Name", item.Name);
                node = ts.AddAttribute(node, Name);
                XmlAttribute PrimeId = ts.CreateAttribute(doc, "PrimeId", item.PrimaryIdentification);
                node = ts.AddAttribute(node, PrimeId);
                XmlAttribute Id = ts.CreateAttribute(doc, "Id", item.Id);
                node = ts.AddAttribute(node, Id);
                XmlAttribute isEnabled = ts.CreateAttribute(doc, "isEnabled", item.Enabled.ToString());
                node = ts.AddAttribute(node, isEnabled);
                XmlAttribute Bounds = ts.CreateAttribute(doc, "Bounds", item.Bounds.ToString());
                node = ts.AddAttribute(node, Bounds);
                XmlAttribute BorderColour = ts.CreateAttribute(doc, "BorderColour", item.BorderColor.ToString());
                node = ts.AddAttribute(node, BorderColour);


                doc = ts.AddNode(doc, node);
            }
            doc.Save(path);


            /*
             * XmlAttribute Name = ts.CreateAttribute(doc, "Name", Regex.Replace(item.Name, @"[^a-zA-Z0-9]", ""));
            node = ts.AddAttribute(node, Name);
            XmlAttribute PrimeId = ts.CreateAttribute(doc, "PrimeId", Regex.Replace(item.PrimaryIdentification, @"[^a-zA-Z0-9]", ""));
            node = ts.AddAttribute(node, PrimeId);
            XmlAttribute Id = ts.CreateAttribute(doc, "Id", Regex.Replace(item.Id, @"[^a-zA-Z0-9]", ""));
            node = ts.AddAttribute(node, Id);
            XmlAttribute isEnabled = ts.CreateAttribute(doc, "isEnabled", item.Enabled.ToString());
            node = ts.AddAttribute(node, isEnabled);
            XmlAttribute Bounds = ts.CreateAttribute(doc, "Bounds", item.Bounds.ToString());
            node = ts.AddAttribute(node, Bounds);
            XmlAttribute BorderColour = ts.CreateAttribute(doc, "BorderColour", item.BorderColor.ToString());
            node = ts.AddAttribute(node, BorderColour);

             * 
             * 
                            XmlNode node = ts.CreateElement(doc, item.PrimaryIdentification);
                            XmlAttribute itemCount = ts.CreateAttribute(doc, "itemCount", window.Items.Count.ToString());
                            node = ts.AddAttribute(node, itemCount);
                            XmlAttribute isModal = ts.CreateAttribute(doc, "isModal", window.IsModal.ToString());
                            node = ts.AddAttribute(node, isModal);
                            XmlAttribute bounds = ts.CreateAttribute(doc, "bounds", window.Bounds.ToString());
                            node = ts.AddAttribute(node, bounds);
                            doc = ts.AddNode(doc, node);
                            doc.Save(path);
                            */

        }


        /*
                UtilityTools utilityTools = new UtilityTools();
                utilityTools.DebugWindowItems(window);

                                List<Window> windows = windowTools.GetWindows(application);
                UtilityTools utilityTools = new UtilityTools();
                utilityTools.DebugWindows(windows);
                */

        public void ReviewIUItems(List<IUIItem> items) {
            foreach (IUIItem x in items) {
                Console.WriteLine($"");
                Console.WriteLine($"name {x.Name}");
                Console.WriteLine($"id {x.Id}");
                Console.WriteLine($"prime {x.PrimaryIdentification}");
                Console.WriteLine($"location {x.Location}");
                Console.WriteLine($"bounds {x.Bounds}");
            }
        }

        public void ReviewIUItems(UIItemCollection items) {
            foreach (IUIItem x in items) {
                Console.WriteLine($"");
                Console.WriteLine($"name {x.Name}");
                Console.WriteLine($"id {x.Id}");
                Console.WriteLine($"prime {x.PrimaryIdentification}");
                Console.WriteLine($"location {x.Location}");
                Console.WriteLine($"bounds {x.Bounds}");
                Console.WriteLine($"type {x.GetType()}");
            }

        }



        public void DebugWindows(List<Window> windows) {
            int track = 0;
            foreach (Window window in windows) {
                Console.WriteLine($"Window {track}");

                Console.WriteLine($"Window AccessKey {window.AccessKey}");
                Console.WriteLine($"Window ActionListener {window.ActionListener}");
                Console.WriteLine($"Window AutomationElement {window.AutomationElement}");
                Console.WriteLine($"Window BorderColor {window.BorderColor}");
                Console.WriteLine($"Window Bounds {window.Bounds}");
                Console.WriteLine($"Window DisplayState {window.DisplayState}");
                Console.WriteLine($"Window Enabled {window.Enabled}");
                Console.WriteLine($"Window Framework {window.Framework}");
                Console.WriteLine($"Window HelpText {window.HelpText}");
                Console.WriteLine($"Window Id {window.Id}");
                Console.WriteLine($"Window IsClosed {window.IsClosed}");
                Console.WriteLine($"Window IsCurrentlyActive {window.IsCurrentlyActive}");
                Console.WriteLine($"Window IsFocussed {window.IsFocussed}");
                Console.WriteLine($"Window IsModal {window.IsModal}");
                Console.WriteLine($"Window IsOffScreen {window.IsOffScreen}");
                Console.WriteLine($"Window Items.Count {window.Items.Count}");
                Console.WriteLine($"Window Location {window.Location}");
                Console.WriteLine($"Window MenuBar.Count{window.MenuBars.Count}");
                Console.WriteLine($"Window Name {window.Name}");
                Console.WriteLine($"Window PrimeID {window.PrimaryIdentification}");
                Console.WriteLine($"Window Title {window.Title}");
                Console.WriteLine($"Window Visible {window.Visible}");
                Console.WriteLine("");
                track++;
            }


        }


        public void DebugWindowItems(Window window) {

            Console.WriteLine("********************************************************");
            Console.WriteLine($"Window AccessKey {window.AccessKey}");
            Console.WriteLine($"Window ActionListener {window.ActionListener}");
            Console.WriteLine($"Window AutomationElement {window.AutomationElement}");
            Console.WriteLine($"Window BorderColor {window.BorderColor}");
            Console.WriteLine($"Window Bounds {window.Bounds}");
            Console.WriteLine($"Window DisplayState {window.DisplayState}");
            Console.WriteLine($"Window Enabled {window.Enabled}");
            Console.WriteLine($"Window Framework {window.Framework}");
            Console.WriteLine($"Window HelpText {window.HelpText}");
            Console.WriteLine($"Window Id {window.Id}");
            Console.WriteLine($"Window IsClosed {window.IsClosed}");
            Console.WriteLine($"Window IsCurrentlyActive {window.IsCurrentlyActive}");
            Console.WriteLine($"Window IsFocussed {window.IsFocussed}");
            Console.WriteLine($"Window IsModal {window.IsModal}");
            Console.WriteLine($"Window IsOffScreen {window.IsOffScreen}");
            Console.WriteLine($"Window Items.Count {window.Items.Count}");
            Console.WriteLine($"Window Location {window.Location}");
            Console.WriteLine($"Window MenuBar.Count{window.MenuBars.Count}");
            Console.WriteLine($"Window Name {window.Name}");
            Console.WriteLine($"Window PrimeID {window.PrimaryIdentification}");
            Console.WriteLine($"Window Title {window.Title}");
            Console.WriteLine($"Window Visible {window.Visible}");
            Console.WriteLine("");

            int track = 0;
            foreach (IUIItem item in window.Items) {
                Console.WriteLine($"item # {track}");
                Console.WriteLine($"item Type {item.GetType()}");
                Console.WriteLine($"item Accesskey {item.AccessKey}");
                //Console.WriteLine($"item ActionListener {item.ActionListener}");
                Console.WriteLine($"item AutomationElement {item.AutomationElement}");
                Console.WriteLine($"item BorderColor {item.BorderColor}");
                Console.WriteLine($"item Bounds {item.Bounds}");
                Console.WriteLine($"item Enabled {item.Enabled}");
                Console.WriteLine($"item Framework {item.Framework}");
                Console.WriteLine($"item Id {item.Id}");
                Console.WriteLine($"item IsFocussed {item.IsFocussed}");
                Console.WriteLine($"item IsOffScreen {item.IsOffScreen}");
                Console.WriteLine($"item Location {item.Location}");
                Console.WriteLine($"item Name {item.Name}");
                Console.WriteLine($"item PrimeID {item.PrimaryIdentification}");
                Console.WriteLine($"item Visible {item.Visible}");
                Console.WriteLine("");
                track++;
            }
            Console.WriteLine("********************************************************");
        }

        public void Review_Panel_Contents(Panel panel) {
            Console.WriteLine($"Panel Name {panel.Name}");
            Console.WriteLine($"Panel ID {panel.Id}");
            Console.WriteLine($"Panel Prime {panel.PrimaryIdentification}");
            int c = 0;
            foreach (var i in panel.Items) {
                Console.WriteLine($"Item Number {c}");
                Console.WriteLine($"Item Name {i.Name}");
                Console.WriteLine($"Item ID {i.Id}");
                Console.WriteLine($"Item Prime {i.PrimaryIdentification}");
                Console.WriteLine($"Item Type {i.GetType().ToString()}");
                Console.WriteLine($"IsOffScreen {i.IsOffScreen}");
                Console.WriteLine($"IsFocussed {i.IsFocussed}");
                Console.WriteLine($"Location {i.Location.ToString()}");
                Console.WriteLine($"IsVisible {i.Visible}");
                Console.WriteLine($"Framework {i.Framework}");
                Console.WriteLine($"Enabled {i.Enabled}");

                Console.WriteLine($"Bounds {i.Bounds.ToString()}");
                Console.WriteLine($"Boarder Colour {i.BorderColor.ToString()}");
                Console.WriteLine($"Automation Element {i.AutomationElement.ToString()}");
                Console.WriteLine($"Access Key {i.AccessKey.ToString()}");
                Console.WriteLine("");

                if (i.GetType() == typeof(Panel)) {
                    Panel p = (Panel)i;
                    Review_Panel_Contents(p);
                }

                c++;
            }
        }

        public void Review_Window_Panels(Window window) {
            Console.WriteLine($"Window Name {window.Name}");
            Console.WriteLine($"Window Id {window.Id}");
            Console.WriteLine($"Window PrimeID {window.PrimaryIdentification}");
            List<IUIItem> panels = window.Items.Where(x => x.GetType() == typeof(Panel)).ToList();
            int track = 0;
            foreach (var i in panels) {
                Console.WriteLine($"Panel # {track}");
                Review_Panel_Contents((Panel)i);
                track++;
            }

        }


        public void review_Tab(Tab tab) {
            Console.WriteLine($"");
            Console.WriteLine($"Name {tab.Name}");
            Console.WriteLine($"Id {tab.Id}");
            Console.WriteLine($"Position {tab.Location}");
            Console.WriteLine($"Page Count {tab.Pages.Count}");
            foreach (var x in tab.Pages) {
                review_TabPages(x);
            }

        }

        public void review_TabPages(ITabPage page) {
            TabPage tabPage = (TabPage)page;
            Console.WriteLine($"");
            Console.WriteLine($"Page Name: {page.Name}");
            Console.WriteLine($"TabPage Name: {tabPage.Name}");

            Console.WriteLine($"Page Id: {page.Id}");
            Console.WriteLine($"TabPage Id: {tabPage.Id}");

            Console.WriteLine($"Page Location: {page.Location}");
            Console.WriteLine($"TabPage Location: {tabPage.Location}");

            Console.WriteLine($"TabPage ItemCount: {tabPage.Items.Count}");
            var x = tabPage.Items;
            int track = 0;
            foreach (var y in x) {
                Console.WriteLine($"Item #: {track}");
                Console.WriteLine($"Item Name: {y.Name}");
                Console.WriteLine($"Item Id: {y.Id}");
                Console.WriteLine($"Item Location: {y.Location}");
                Console.WriteLine($"Item Type: {y.GetType()}");
            }

        }



    }
}

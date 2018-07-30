using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using System.Diagnostics;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.InputDevices;
using System.Windows.Automation;
using System.Reflection;


namespace TestSetsTestStack
{
    public class InitSUT {
        public Application app = null;
        public string exeLocation = null;
        public string workingDirectory = null;

        public InitSUT(string exeAddress, string workingDir)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(exeAddress)
            { WorkingDirectory = workingDirectory };
            exeLocation = exeAddress;
            workingDirectory = workingDir;
            app = Application.Launch(processStartInfo);

        }
    }

    public class ShutDownSUT {
        public ShutDownSUT(Application application)
        {
            application.Close();
            application.Dispose();
        }
    }



    public class Camera
    {
        private const string exeLocation = @"C:\Windows\system32\mspaint.exe";
        private const string workingDirectory = @"C:\workDir";
        public Application _Application;
        public Dictionary<string, int> Meter = new Dictionary<string, int>();
        public Dictionary<string, Dictionary<string, JItem>> Mark = new Dictionary<string, Dictionary<string, JItem>>();

        public Camera()
        {
            _Application = new InitSUT(exeLocation, workingDirectory).app;
            
            foreach (Window window in _Application.GetWindows())
            {
                ReviewTypes(window.Items);
                SetupGroups();
                GroupItems(window.Items);
            }
        
            
        }
        public void Snap() {
            int c = _Application.GetWindows().Count;
            Window window = _Application.GetWindows()[c-1];
            Console.WriteLine($"{window.Name}");
                Meter.Clear();
                Mark.Clear();
                ReviewTypes(window.Items);
                SetupGroups();
                GroupItems(window.Items);
        }



        //First Pass
        private void ReviewTypes(UIItemCollection items)
        {
            string holder;  
            foreach (IUIItem x in items)
            {
                holder = x.GetType().ToString();
                if (!Meter.ContainsKey(holder))
                { Meter.Add(holder, 1); }
                else
                { Meter[holder]++; }
            }                      
        }
        //Setup Groups
        private void SetupGroups()
        {
            foreach (KeyValuePair<string, int> type in Meter)
            {
                if (!Mark.ContainsKey(type.Key))
                {
                    Mark.Add(type.Key, new Dictionary<string, JItem>());
                }
            }
        }

        //Second Pass 
        private void GroupItems(UIItemCollection items)
        {
            string holder;
            int track = 0;
            JItem store;
            foreach (IUIItem item in items)
            {
                holder = item.GetType().ToString();
                if (Mark.ContainsKey(holder))
                {
                    store = new JItem(item, track.ToString());
                    Mark[holder].Add(store.Order, new JItem(item,store.Order));
                }
                track++;
            }
        }
    }

    public class JItem
    {
        public string Name;
        public string Id;
        public string Prime;
        public string Type;
        public string ShortType;
        public string Order;
        public string Label;
        public string Holder;
        public string Bounds;
        public string Location;
        public string Color;
        public string IsEnabled;
        public string IsFocussed;
        public string IsOffScreen;
        public JItem(IUIItem item, string order) {
            Holder = item.GetType().ToString();
            Name = item.Name;
            Id = item.Id;
            Prime = item.PrimaryIdentification;
            Type = Holder;
            ShortType = ClipType(Holder);
            Order = order;
            Label = LabelMaker(item);
            Bounds = item.Bounds.ToString();
            Location = item.Location.ToString();
            Color = item.BorderColor.ToString();
            IsEnabled = item.Enabled.ToString();
            IsFocussed = item.IsFocussed.ToString();
            IsOffScreen = item.IsOffScreen.ToString();
        }

        private string LabelMaker(IUIItem item)
        {
            if (item.Id.Length > 0) { return $"1_{item.Id}_{ShortType}_{Order}"; }
            if (item.PrimaryIdentification.Length > 0) { return $"2_{item.PrimaryIdentification}_{ShortType}_{Order}"; }
            if (item.Name.Length > 0) { return $"3_{ClipName(item.Name)}_{ShortType}_{Order}"; }
            return $"4_{ShortType}_{Order}";
        }

        private string ClipType(string text)
        {
            string newText = text.Remove(0, text.LastIndexOf(".") + 1);
            return newText;
        }

        private string ClipName(string text)
        {
            string newText = text.Replace(" ", "");                
            return newText;
        }
    }
}



    *********************************************************************





namespace Eggs {
    public static class Basket {
        public const string Path = @"C:\workDir\";
        private static XmlDocument Document;
        private static XmlNode Root;
        public static XmlToolKit XTK;
        public static Dictionary<int, string> Dictionary = new Dictionary<int, string>();


        static Basket() {
            Init();
            XTK = new XmlToolKit(Document);
        }

        private static string _a;
        public static string a {
            get => a = _a; set => _a = value; }

        public static void XmlReadi(string name = "Default.xml") {
            XDocument doc = XDocument.Load($"{Path}{name}");
            Console.WriteLine("In Readi");
            Console.WriteLine(doc.Root.Name);
            foreach (XNode x in doc.Root.DescendantNodes()) {
                if (x.Parent == doc.Root) {

                    // Console.WriteLine(x.ToString());
                } else {
                    Console.WriteLine(x.ToString());
                    using (XmlReader reader = x.CreateReader()) {
                        while (reader.Read()) {
                            Console.WriteLine(reader.Name);
                            Dictionary.Add(int.Parse(reader.GetAttribute("Order")), reader.GetAttribute("Label"));
                        }
                    }
                }

            }
            Console.WriteLine("Read Dictionary");

            for (var i = 0; i < Dictionary.Count; i++) {
                Console.WriteLine($"{Dictionary[i]}");

            }

            Console.ReadLine();
        }




        public static void XmlRead(string name = "Default.xml") {
            string attribute;
            using (XmlReader reader = XmlReader.Create($"{Path}{name}")) {
                while (reader.Read()) {
                    switch (reader.Name) {
                        case "Windows":
                            Console.WriteLine("Start of Windows Element");
                            break;
                        case "Window":
                            Console.WriteLine("In Window");
                            attribute = reader["id"];
                            if (attribute != null) {
                                Console.WriteLine($"Has Attribute {attribute}");
                            }
                            if (reader.Read()) {
                                Console.WriteLine($"Text Node: {reader.Value.Trim()}");
                            }
                            break;
                        case "Control":
                            Console.WriteLine("In Control");
                            attribute = reader["url"];
                            if (attribute != null) {
                                Console.WriteLine($"Has Attribute {attribute}");
                            }
                            break;
                    }
                }
            }
        }



        public static void XmlWrite(string name = "Default.xml") {
            if (name.Contains(".xml")) {
                Document.Save($"{Path}{name}");
            } else {
                Document.Save($"{Path}{name}.xml");
            }
        }

        public static void Run() {
            for (int i = 0; i < 5; i++) {
                XTK.NTK.Adopt(Root, XTK.NTK.Decorate(XTK.NTK.CreateNode("Window"), XTK.NTK.CreateAttribute("id", i.ToString())));
            }
            foreach (XmlNode x in Root.ChildNodes) {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Value);
                XTK.NTK.Adopt(x, XTK.NTK.Decorate(XTK.NTK.CreateNode("Control"), XTK.NTK.CreateAttribute("url", "www.are.you")));
            }
        }

        private static void Init() {
            Document = new XmlDocument();
            XmlNode docNode = Document.CreateXmlDeclaration("1.0", "UTF-8", null);
            Document.AppendChild(docNode);
            Root = Document.CreateElement("Items");
            Document.AppendChild(Root);

        }




        public static void ProcessSummary(Dictionary<string, Dictionary<string, JItem>> dictionary) {
            foreach (var x in dictionary) {
                XTK.NTK.Adopt(Root, ProcessEntries(x.Value, XTK.NTK.CreateNode(x.Key)));
            }
        }

        private static XmlNode ProcessEntries(Dictionary<string, JItem> dictionary, XmlNode node) {
            foreach (var x in dictionary) {
                XTK.NTK.Adopt(node, ProcessEntry(x.Value, XTK.NTK.CreateNode(x.Value.ShortType)));
            }
            return node;
        }
        private static XmlNode ProcessEntry(JItem item, XmlNode node) {
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Label", item.Label));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Name", item.Name));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Id", item.Id));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Prime", item.Prime));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Order", item.Order));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Type", item.Type));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Bounds", item.Bounds));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Location", item.Location));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("Color", item.Color));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("IsEnabled", item.IsEnabled));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("IsFocussed", item.IsFocussed));
            XTK.NTK.Decorate(node, XTK.NTK.CreateAttribute("IsOffScreen", item.IsOffScreen));


            return node;
        }

    }

    public class XmlToolKit {
        public NodeToolKit NTK;
        private XmlDocument _Document;
        public XmlToolKit(XmlDocument document) {
            _Document = document;
            NTK = new NodeToolKit(document);
        }
    }

    public class NodeToolKit {
        XmlDocument _Document;
        public NodeToolKit(XmlDocument document) {
            _Document = document;
        }

        public XmlNode CreateNode(string nodeName = "Element") {
            return _Document.CreateElement(nodeName);
        }
        public XmlAttribute CreateAttribute(string name = "Attribute", string value = "value") {
            XmlAttribute attribute = _Document.CreateAttribute(name);
            attribute.Value = value;
            return attribute;
        }
        public XmlNode Adopt(XmlNode parent, XmlNode child) {
            parent.AppendChild(child);
            return parent;
        }
        public XmlNode Decorate(XmlNode node, XmlAttribute attribute) {
            node.Attributes.Append(attribute);
            return node;
        }



    }
}
*/

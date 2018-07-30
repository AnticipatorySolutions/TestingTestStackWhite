using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace TestStackTest.TestBehaviours {
    public class XmlTools {

        public Dictionary<string, WindowPath> ReadWindowArchive(string path, string name) 
        {
            Dictionary<string, WindowPath> windows = new Dictionary<string, WindowPath>();
            WindowPath details;
            XDocument doc = XDocument.Load($"{path}{name}");
            foreach (XNode node in doc.Root.DescendantNodes()) 
            {                
                if (node.Parent == doc.Root) {
                    using (XmlReader reader = node.CreateReader()) {
                        while (reader.Read()) {
                            details = SetWindowDetails(reader);
                            windows.Add(details.label,details);
                        }
                    }
                }

                /*
                else 
                {
                    Console.WriteLine("Not Root Child");
                    Console.WriteLine(node.ToString());
                    using (XmlReader reader = node.CreateReader()) 
                    {
                        while (reader.Read()) 
                        {
                            Console.WriteLine(reader.Name);
                        }
                    }
                }
                */
            }

            return windows;
        }


        private WindowPath SetWindowDetails(XmlReader reader) 
        {
            WindowPath details = new WindowPath();
            details.label = reader.Name;
            details.url = reader.GetAttribute("url");
            details.isModal = reader.GetAttribute("isModal");
            details.itemCount = reader.GetAttribute("itemCount");
            details.enabledCount = reader.GetAttribute("enabledCount");
            details.bounds = reader.GetAttribute("bounds");
            return details;
        }
        
           
    }

    public class WindowPath 
    {
        private string _url;
        private string _enabledCount;
        private string _itemCount;
        private string _isModal;
        private string _bounds;
        private string _label;
        public string url { get { return _url; } set { _url = value; } }
        public string enabledCount { get { return _enabledCount; } set { _enabledCount = value; } }
        public string itemCount { get { return _itemCount; } set { _itemCount = value; } }
        public string isModal { get { return _isModal; } set { _isModal = value; } }
        public string bounds { get { return _bounds; } set { _bounds= value; } }
        public string label { get { return _label; } set { _label = value; } }
    }

    public class WindowItem 
    {
        private string _label;
        private string _name;
        private string _prime;
        private string _id;
        private string _isEnabled;
        private string _bounds;
        private string _borderColour;
        private string _type;
        private string _order;
        private string _value;
        public string label { get { return _label; } set { _label = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string prime { get { return _prime; } set { _prime = value; } }
        public string id { get { return _id; } set { _id = value; } }
        public string isEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        public string bounds { get { return _bounds; } set { _bounds = value; } }
        public string borderColour { get { return _borderColour; } set { _borderColour = value; } }
        public string type { get { return _type; } set { _type = value; } }
        public string order { get { return _order; } set { _order = value; } }
        public string value { get { return _value; } set { _value = value; } }
    }


}

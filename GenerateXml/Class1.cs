using System;
using System.Xml;


namespace GenerateXml
{
    public class XmlToolSet
    {
        const string Path = @"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\test.xml";
        public void Read(string path = Path) 
        {
            using (XmlReader reader = XmlReader.Create(path)) 
            {
                while (reader.Read()) 
                {
                    if (reader.IsStartElement()) 
                    {
                        switch (reader.Name) 
                        {
                            case "products":
                                Console.WriteLine("Start of products element");
                                break;
                            case "product":
                                Console.WriteLine("Start of product element");
                                string attribute = reader["id"];
                                if (attribute != null) 
                                {
                                    Console.WriteLine($"Has attribute {attribute}");
                                }
                                if (reader.Read()) 
                                {
                                    Console.WriteLine($"text node: {reader.Value.Trim()}");
                                }

                                break;

                        }


                    }
                }
            }

        }




        public XmlDocument newDoc(string elementName) 
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlNode windows = doc.CreateElement(elementName);
            doc.AppendChild(docNode);
            doc.AppendChild(windows);
            return doc;
        }


        public XmlDocument AddNode(XmlDocument doc, XmlNode node) 
        {
            doc.DocumentElement.AppendChild(node);
            return doc;
        }


        public XmlNode CreateElement(XmlDocument doc, string elementName) 
        {
            if (elementName == null || elementName.Length ==0) { elementName = "NoName"; }

            XmlNode node = doc.CreateElement(elementName);
            return node;
        }

        public XmlAttribute CreateAttribute(XmlDocument doc, string attributeName, string attributeValue)            
        {
            XmlAttribute attribute = doc.CreateAttribute(attributeName);
            attribute.Value = attributeValue;
            return attribute;
        }

        public XmlNode AddAttribute(XmlNode node, XmlAttribute attribute) 
        {
            node.Attributes.Append(attribute);
            return node;
        }


        public void Write() 
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode productsNode = doc.CreateElement("products");
            doc.AppendChild(productsNode);

            XmlNode productNode = doc.CreateElement("product");
            XmlAttribute productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = "01";
            productNode.Attributes.Append(productAttribute);
            productsNode.AppendChild(productNode);

            XmlNode nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode("Java"));
            productNode.AppendChild(nameNode);
            XmlNode priceNode = doc.CreateElement("Price");
            priceNode.AppendChild(doc.CreateTextNode("Free"));
            productNode.AppendChild(priceNode);

            // Create and add another product node.
            productNode = doc.CreateElement("product");
            productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = "02";
            productNode.Attributes.Append(productAttribute);
            productsNode.AppendChild(productNode);
            nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode("C#"));
            productNode.AppendChild(nameNode);
            priceNode = doc.CreateElement("Price");
            priceNode.AppendChild(doc.CreateTextNode("Free"));
            productNode.AppendChild(priceNode);

            doc.Save($@"S:\Dev_IMS\Development\Jordan\uiAutomation\systemMapping\test.xml");

//            doc.Save(Console.Out);
        }


    }    




}

using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClickNCheck
{
    public class SOAPResponsiveService
    {
        public void process()
        {
            XmlDocument xmlDOc = new XmlDocument();
            xmlDOc.Load("C:\\Users\\Xolani Dlamini\\Downloads\\Books.xml");

            foreach (XmlNode doc in xmlDOc.DocumentElement)
            {
                string name = doc.Attributes[0].Value;
                string checkId = doc["checkStatus"].InnerText;

            }
            

        }
    }
}

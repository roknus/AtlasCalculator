using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

namespace Serializer
{
    public class XMLClassNode : XMLNodeWithCost
    {
        [XmlElement("ClassName")]
        public string m_ClassName;

        public XMLClassNode()
        {

        }

        public XMLClassNode(ClassNode node)
            : base(node)
        {
            m_ClassName = node.ClassName;
        }
    }
}
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

namespace Serializer
{
    [XmlInclude(typeof(XMLNodeWithPrestige))]
    [XmlInclude(typeof(XMLClassNode))]
    [XmlInclude(typeof(XMLGodForm))]

    public class Cost
    {
        [XmlAttribute("type")]
        public int type;
        [XmlText]
        public int value;
    }

    public class XMLNodeWithCost : XMLNode
    {
        [XmlElement("Cost")]
        public Cost m_Cost;

        public XMLNodeWithCost()
        {
            m_Cost = new Cost();
        }

        public XMLNodeWithCost(NodeWithCost node)
            : base(node)
        {
            m_Cost = new Cost();
            m_Cost.value = node.m_Cost;
            m_Cost.type = (int)node.m_CostType;
        }
    }
}
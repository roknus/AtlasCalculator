using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

namespace Serializer
{
    [XmlInclude(typeof(XMLNodeWithTwoStat))]
    public class XMLNodeWithOneStat : XMLNodeWithPrestige
    {
        [XmlElement("Stat1Type")]
        public int m_Stat1;
        [XmlElement("Stat1Value")]
        public int value1;

        public XMLNodeWithOneStat()
        {

        }

        public XMLNodeWithOneStat(NodeWithOneStat node)
            : base(node)
        {
            m_Stat1 = (int)node.m_Stat1;
            value1 = node.value1;
        }
    }
}
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

namespace Serializer
{
    public class XMLNodeWithTwoStat : XMLNodeWithOneStat
    {
        [XmlElement("Stat2Type")]
        public int m_Stat2;
        [XmlElement("Stat2Value")]
        public int value2;

        public XMLNodeWithTwoStat()
        {

        }

        public XMLNodeWithTwoStat(NodeWithTwoStat node)
            : base(node)
        {
            m_Stat2 = (int)node.m_Stat2;
            value2 = node.value2;
        }
    }
}
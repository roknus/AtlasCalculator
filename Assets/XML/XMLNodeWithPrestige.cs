using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

namespace Serializer
{
    [XmlInclude(typeof(XMLTalentNode))]
    [XmlInclude(typeof(XMLNodeWithOneStat))]
    public class XMLNodeWithPrestige : XMLNodeWithCost
    {
        [XmlElement("Prestige")]
        public int m_Prestige;

        public XMLNodeWithPrestige()
        {

        }

        public XMLNodeWithPrestige(NodeWithPrestige node)
            : base(node)
        {
            m_Prestige = node.m_Prestige;
        }
    }
}
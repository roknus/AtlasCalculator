using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

namespace Serializer
{
    public class XMLTalentNode : XMLNodeWithPrestige
    {
        [XmlElement("TalentName")]
        public string m_TalentName;

        [XmlElement("Talent")]
        public Symbol m_Talent;

        public XMLTalentNode()
        {

        }

        public XMLTalentNode(SymbolNode node)
            : base(node)
        {
            m_TalentName = node.m_TalentName;
            m_Talent = node.Talent;
        }
    }
}
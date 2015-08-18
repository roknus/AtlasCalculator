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

        public XMLTalentNode()
        {

        }

        public XMLTalentNode(TalentNode node)
            : base(node)
        {
            m_TalentName = node.m_TalentName;
        }
    }
}
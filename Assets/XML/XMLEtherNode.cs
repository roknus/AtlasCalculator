using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

public class XMLEtherNode : XMLNode
{
    [XmlElement("EtherType")]
    public EtherType m_EtherType;

    public XMLEtherNode()
    {

    }

    public XMLEtherNode(EtherNode node)
        : base(node)
	{
        m_EtherType = node.m_EtherType;
	}
}

using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

public class XMLEmptyNode : XMLNode
{
    [XmlElement("Description")]
    public string description;

    public XMLEmptyNode()
    {

    }

    public XMLEmptyNode(EmptyNode node)
        : base(node)
	{
        description = node.description;
    }
}

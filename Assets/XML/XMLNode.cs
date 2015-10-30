using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using Serializer;

using UnityEngine;

[XmlInclude(typeof(XMLNodeWithCost))]
[XmlInclude(typeof(XMLEtherNode))]
public class XMLNode
{
	[XmlAttribute("id")]
	public int m_Id;
	[XmlAttribute("position-X")]
	public float m_X;
	[XmlAttribute("position-Z")]
    public float m_Z;

    [XmlAttribute("isOrigin")]
    public bool m_Origin;
	
	[XmlArray("neighbor")]
	[XmlArrayItem("int")]
    public List<int> neighbor = new List<int>();

	public XMLNode()
	{
		m_Id = 0;
	}

	public XMLNode(NodeBase node)
	{
		m_Id        = node.m_Id;
		m_X         = node.transform.position.x;
		m_Z         = node.transform.position.z;
        m_Origin    = node.m_Origin;

        foreach (int i in node.m_neighborsIds)
        {
            if (!neighbor.Contains(i))
                neighbor.Add(i);
        }
	}
}



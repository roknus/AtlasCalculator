using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

[XmlRoot("NodeCollection")]
public class NodeSerializer 
{
	
	[XmlArray("Nodes")]
	[XmlArrayItem("Node")]
	public List<XMLNode> Nodes;

	public NodeSerializer()
	{
		Nodes = new List<XMLNode>();
	}

	public void Add(NodeBase _n) { Nodes.Add(_n.GetSerialize()); }
}

using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

[XmlRoot("NodeCollection")]
public class UserNodeSerializer 
{
	
	[XmlArray("Nodes")]
	[XmlArrayItem("Node")]
	public List<int> Nodes;
	
	public UserNodeSerializer()
	{
		Nodes = new List<int>();
	}
	
	public void Add(NodeBase _n) { Nodes.Add(_n.m_Id); }
}

using UnityEngine;
using System.Collections;
using Serializer;

public class ClassNode : NodeWithCost 
{
    public string m_ClassName;

	public string ClassName
	{
		get
		{
			if(m_ClassName == "")
				return "N/A";
			else return m_ClassName;
		}
		set { m_ClassName = value; }
	}

	public override string GetName() { return "Class : " + m_ClassName; }

	public override XMLNode GetSerialize ()	{ return new XMLClassNode(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLClassNode classNode = node as XMLClassNode;

		ClassName = classNode.m_ClassName;
	}
}

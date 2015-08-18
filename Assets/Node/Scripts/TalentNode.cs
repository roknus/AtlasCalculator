using UnityEngine;
using System.Collections;
using Serializer;

public class TalentNode : NodeWithPrestige 
{
	public string m_TalentName;

	public override string GetName() { return "Talent : " + m_TalentName; }

	public override XMLNode GetSerialize ()	{ return new XMLTalentNode(this); }
	
	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);
		
		XMLTalentNode talentNode = node as XMLTalentNode;
		
		m_TalentName = talentNode.m_TalentName;
	}
}

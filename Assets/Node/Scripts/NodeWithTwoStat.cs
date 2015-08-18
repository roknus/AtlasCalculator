using UnityEngine;
using System.Collections;
using Serializer;

public enum Stat2
{
    Proficency = 0
}

public class NodeWithTwoStat : NodeWithOneStat
{
    public Stat2 m_Stat2;
	public int value2;

	public override XMLNode GetSerialize ()	{ return new XMLNodeWithTwoStat(this); }
	
	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);
		
		XMLNodeWithTwoStat nodeWith2Stat = node as XMLNodeWithTwoStat;
		
		m_Stat2 = (Stat2)nodeWith2Stat.m_Stat2;
		value2 = nodeWith2Stat.value2;
	}

    public override int GetProficency() { return value2; }
}

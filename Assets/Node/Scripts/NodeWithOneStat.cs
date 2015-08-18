using UnityEngine;
using System.Collections;
using Serializer;

public enum Stat1
{
    Might 		= 0,
	Stamina		= 1,
    Luck 		= 2,
    Valor 		= 3,
    Spirit 		= 4,
    Greatness 	= 5,
    Strenght 	= 6
}

public class NodeWithOneStat : NodeWithPrestige
{
	public Stat1 m_Stat1;
	public int value1;

	public override string GetName ()	{ return "Bonus : " + m_Stat1.ToString(); }
	
	public override XMLNode GetSerialize ()	{ return new XMLNodeWithOneStat(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLNodeWithOneStat nodeWith1Stat = node as XMLNodeWithOneStat;

		m_Stat1 = (Stat1)nodeWith1Stat.m_Stat1;
		value1 = nodeWith1Stat.value1;
	}
}

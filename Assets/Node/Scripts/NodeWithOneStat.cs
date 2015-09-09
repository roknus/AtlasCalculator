using UnityEngine;
using System.Collections;
using Serializer;

public enum Stat1
{
    Might = 0,
	Stamina		= 1,
    Luck 		= 2,
    Valor 		= 3,
    Spirit 		= 4,
    Greatness 	= 5,
    Strength 	= 6
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

    public override int GetGreatness() { return (m_Stat1 == Stat1.Greatness) ? value1 : 0; }
    public override int GetMight() { return (m_Stat1 == Stat1.Might) ? value1 : 0; }
    public override int GetStamina() { return (m_Stat1 == Stat1.Stamina) ? value1 : 0; }
    public override int GetStrength() { return (m_Stat1 == Stat1.Strength) ? value1 : 0; }
    public override int GetValor() { return (m_Stat1 == Stat1.Valor) ? value1 : 0; }
    public override int GetLuck() { return (m_Stat1 == Stat1.Luck) ? value1 : 0; }
    public override int GetSpirit() { return (m_Stat1 == Stat1.Spirit) ? value1 : 0; }
}

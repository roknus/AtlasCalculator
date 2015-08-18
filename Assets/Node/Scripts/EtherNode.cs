using UnityEngine;
using System.Collections;

public enum EtherType
{
    Sun,
    Coph,
    Het
}

public class EtherNode : NodeBase 
{
    public EtherType m_EtherType;
	
	public override string GetName() { return m_EtherType.ToString() + " Ether Slot"; }
	
	public override XMLNode GetSerialize ()	{ return new XMLEtherNode(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLEtherNode etherNode = node as XMLEtherNode;

		m_EtherType = (EtherType)etherNode.m_EtherType;
	}

    public override Cost GetCost()
    {
        return new Cost(0,0,0);
    }
}

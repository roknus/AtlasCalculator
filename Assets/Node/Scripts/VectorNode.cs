using UnityEngine;
using System.Collections;

public enum EtherType
{
    Sun,
    Coph,
    Het
}

public class VectorNode : NodeBase 
{
    public EtherType m_EtherType;

	public override string GetName() { return m_EtherType.ToString() + " Ether Slot"; }
	
	public override XMLNode GetSerialize ()	{ return new XMLEtherNode(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLEtherNode etherNode = node as XMLEtherNode;

		m_EtherType = (EtherType)etherNode.m_EtherType;

        Sprite[] sprites = Resources.LoadAll<Sprite>("NodesIcon/NodeIcons");

        switch (m_EtherType)
        {
            case EtherType.Sun:
                Locked      = sprites[50];
                Unlocked    = sprites[70];
                Simulated   = sprites[60];
                Calculated  = sprites[40];
                break;
            case EtherType.Het:
                Locked      = sprites[51];
                Unlocked    = sprites[71];
                Simulated   = sprites[61];
                Calculated  = sprites[41];
                break;
            case EtherType.Coph:
                Locked      = sprites[52];
                Unlocked    = sprites[72];
                Simulated   = sprites[62];
                Calculated  = sprites[42];             
                break;
        }

        m_SpriteRenderer.sprite = Locked;
	}

    public override Cost GetCost()
    {
        return new Cost(0,0,0);
    }
}

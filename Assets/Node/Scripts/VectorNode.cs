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
    private static string NodeIconPath = "NodesIcon/Vector";

	public override string GetName() { return m_EtherType.ToString() + " Ether Slot"; }
	
	public override XMLNode GetSerialize ()	{ return new XMLEtherNode(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLEtherNode etherNode = node as XMLEtherNode;

		m_EtherType = (EtherType)etherNode.m_EtherType;

        switch(m_EtherType)
        {
            case EtherType.Sun:
                Locked = Resources.Load<Sprite>(NodeIconPath + "/VectorBlue" + "/Vector_Blue_Locked");
                Unlocked = Resources.Load<Sprite>(NodeIconPath + "/VectorBlue" + "/Vector_Blue_Unlocked");
                Simulated = Resources.Load<Sprite>(NodeIconPath + "/VectorBlue" + "/Vector_Blue_Simulated");
                Calculated = Resources.Load<Sprite>(NodeIconPath + "/VectorBlue" + "/Vector_Blue_Calculated");           
                break;
            case EtherType.Het:
                Locked = Resources.Load<Sprite>(NodeIconPath + "/VectorGreen" + "/Vector_Green_Locked");
                Unlocked = Resources.Load<Sprite>(NodeIconPath + "/VectorGreen" + "/Vector_Green_Unlocked");
                Simulated = Resources.Load<Sprite>(NodeIconPath + "/VectorGreen" + "/Vector_Green_Simulated");
                Calculated = Resources.Load<Sprite>(NodeIconPath + "/VectorGreen" + "/Vector_Green_Calculated");
                break;
            case EtherType.Coph:
                Locked = Resources.Load<Sprite>(NodeIconPath + "/VectorRed" + "/Vector_Red_Locked");
                Unlocked = Resources.Load<Sprite>(NodeIconPath + "/VectorRed" + "/Vector_Red_Unlocked");
                Simulated = Resources.Load<Sprite>(NodeIconPath + "/VectorRed" + "/Vector_Red_Simulated");
                Calculated = Resources.Load<Sprite>(NodeIconPath + "/VectorRed" + "/Vector_Red_Calculated");
                break;
        }

        m_SpriteRenderer.sprite = Locked;
	}

    public override Cost GetCost()
    {
        return new Cost(0,0,0);
    }
}

using UnityEngine;
using System.Collections;
using Serializer;

public enum CostType
{
    RedSpark 		= 0,
    GreenSpark 		= 1,
    BlueSpark 		= 2,
    PinkSparks 	    = 3,
    Transformation 	= 4,
    Diamond 		= 5
}

public abstract class NodeWithCost : NodeBase
{
    public CostType m_CostType;
    public int m_Cost;

    public override void Deserialize(XMLNode node)
    {
        base.Deserialize(node);

        XMLNodeWithCost node2 = node as XMLNodeWithCost;

        m_Cost = node2.m_Cost.value;
		m_CostType = (CostType)node2.m_Cost.type;
	}

    public override Cost GetCost()
    {
        Cost cost = new Cost(0,0,0);
        if(m_CostType == CostType.RedSpark)
            cost.R += m_Cost;
        if(m_CostType == CostType.GreenSpark)
            cost.G += m_Cost;
        if(m_CostType == CostType.BlueSpark)
            cost.B += m_Cost;
        if (m_CostType == CostType.PinkSparks)
            cost.Pink += m_Cost;
        return cost;
    }
}

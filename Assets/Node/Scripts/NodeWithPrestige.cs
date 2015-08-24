using UnityEngine;
using System.Collections;
using Serializer;

public abstract class NodeWithPrestige : NodeWithCost 
{
    public int m_Prestige;

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLNodeWithPrestige prestigeNode = node as XMLNodeWithPrestige;

		m_Prestige = prestigeNode.m_Prestige;
	}

    public override int GetPrestige()
    {
        return m_Prestige;
    }
}

using UnityEngine;
using System.Collections;

public class EmptyNode : NodeBase 
{
    public string description;

	public override string GetName() { return "Empty"; }
	
	public override XMLNode GetSerialize ()	{ return new XMLEmptyNode(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);

		XMLEmptyNode emptyNode = node as XMLEmptyNode;

        description = emptyNode.description;
    }

    public override Cost GetCost()
    {
        return new Cost(0,0,0);
    }
}

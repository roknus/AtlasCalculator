using UnityEngine;
using System.Collections;
using Serializer;

public class GodForm : NodeWithCost 
{	
	public override string GetName() { return "God's Form"; }
	
	public override XMLNode GetSerialize ()	{ return new XMLGodForm(this); }

	public override void Deserialize (XMLNode node)
	{
		base.Deserialize (node);
	}
}

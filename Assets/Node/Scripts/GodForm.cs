using UnityEngine;
using UnityEngine.EventSystems;
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

	public void PointerEnter(BaseEventData _event)
	{
		base.OnPointerEnter (_event as PointerEventData);
	}

	public void PointerExit(BaseEventData _event)
	{
		base.OnPointerExit (_event as PointerEventData);
	}

	public void PointerClick(BaseEventData _event)
	{
		base.OnPointerClick (_event as PointerEventData);
	}
}

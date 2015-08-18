using UnityEngine;
using System.Collections.Generic;

public class NodeCostList : MonoBehaviour {

	public Transform NodeCostButton;
	
	public static NodeCostList Instance { get; private set; }

	void Awake () 
	{		
		if (Instance != null) {
			Destroy (gameObject);
			return;
		}
		
		Instance = this;
	}

	void Update () 
	{
	
	}

	public void Clear()
	{
		foreach (Transform t in transform)
			Destroy (t.gameObject);
	}

	public void Init(List<KeyValuePair<int, NodePath>> nodeList)
	{
		Clear ();
        for(int i = 0; i < 10; i++)
        {
            if (i >= nodeList.Count) break;

            KeyValuePair<int, NodePath> p = nodeList[i];

			Transform t = Instantiate(NodeCostButton) as Transform;
			t.SetParent(transform);
			t.GetComponent<NodeCostButton>().Init(  p.Value.Red,
                                                    p.Value.Green,
                                                    p.Value.Blue,
                                                    p.Value.TotSparks,
                                                    WorldScript.Instance.m_nodes[p.Key]);
		}
	}

	public void Init(NodePath path)
	{
		Clear ();
		foreach (NodeBase n in path.Path) {
            // TODO
		}
	}
}

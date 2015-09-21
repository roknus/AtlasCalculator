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
        for(int i = 0; i < 10; i++)
        {
            if (i >= nodeList.Count) break;

            KeyValuePair<int, NodePath> p = nodeList[i];

			Transform t = Instantiate(NodeCostButton) as Transform;
			t.SetParent(transform);
			t.GetComponent<NodeCostButton>().Init(p.Value, WorldScript.Instance.m_nodes[p.Key]);
		}
	}

	public void Init(NodePath path)
	{
	}

    public void OnEnable()
    {
        Clear();
    }

    public void FindCheapestGreatnessNodes()
    {
        List<KeyValuePair<int, NodePath>> res = new List<KeyValuePair<int, NodePath>>();
        foreach (int i in WorldScript.Instance.m_nodes.Keys)
        {
            NodeBase nodebase = WorldScript.Instance.m_nodes[i].GetComponent<NodeBase>();
            if (nodebase && !nodebase.bUnlocked && !nodebase.bSimulationUnlock && nodebase is NodeWithOneStat)
            {
                NodeWithOneStat node = nodebase as NodeWithOneStat;
                if (node.m_Stat1 == Stat1.Greatness)
                {
                    NodePath path = node.FindCheapestPath();
                    if(path != null)
                    {
                        res.Add(new KeyValuePair<int, NodePath>(i, path));
                    }
                }
            }
        }

        res.Sort((firstPair, nextPair) =>
        {
            return firstPair.Value.TotSparks < nextPair.Value.TotSparks ? -1 : (firstPair.Value.TotSparks > nextPair.Value.TotSparks ? 1 : 0);
        }
        );
        Init(res);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode] 
public abstract class NodeBase : MonoBehaviour
{
	public int 				m_Id;
	public List<Transform>  m_neighbors;
	public List<NodeBase>  	m_neighborsInfo;
    public List<int>        m_neighborsIds;
	public int 				m_weight;
    public bool             m_Origin;
    private bool            m_highLight;
	public bool 			HighLight
	{
		get
		{
			return m_highLight;
		}
		set
		{
			
			if(bUnlocked)
			{
				m_SpriteRenderer.color = EdgeScript.gold;
			}
			else if(value)
			{
				m_SpriteRenderer.color = Color.green;
			}
			else if(bSimulationUnlock)
			{
				m_SpriteRenderer.color = Color.red;				
			}
			else
			{
				m_SpriteRenderer.color = EdgeScript.lightBlue;
			}
			m_highLight = value;
		}
	}
    private bool            m_bSimulationUnlock;
    public bool             bSimulationUnlock
    {
        get
        {
			return (m_Origin || m_bSimulationUnlock);
		}
		set
        {
            if (value)
            {
                WorldScript.Instance.m_UnlockedPath_Simulation.Add(this);
                m_SpriteRenderer.color = Color.red;
            }
            else
            {
				WorldScript.Instance.m_UnlockedPath_Simulation.Remove(this);
				if(bUnlocked)
				{
					m_SpriteRenderer.color = EdgeScript.gold;
				}
				else if(HighLight)
				{
					m_SpriteRenderer.color = Color.green;				
				}
				else
				{
					m_SpriteRenderer.color = EdgeScript.lightBlue;
				}
            }
			m_bSimulationUnlock = value;
        }
    }

    private bool            m_bUnlocked;
    public bool bUnlocked
    {
        get
        {
            return (m_Origin || m_bUnlocked);
        }
        set
        {
            if (value)
            {
                //WorldScript.Instance.m_UnlockedPath.Add(this);
                m_SpriteRenderer.color = EdgeScript.gold;
            }
            else
            {
                //WorldScript.Instance.m_UnlockedPath.Remove(this);
                m_SpriteRenderer.color = EdgeScript.lightBlue;
            }
            m_bUnlocked = value;
        }
    }

    public SpriteRenderer m_SpriteRenderer;

	void Awake()
	{
        transform.SetParent(WorldScript.Instance.transform);

		m_SpriteRenderer.color = EdgeScript.lightBlue;
	}

    public void InitNode()
    {
		// Create this node neighbors lists
        foreach(int i in m_neighborsIds)
        {
			m_neighbors.Add(WorldScript.Instance.m_nodes[i]);
			m_neighborsInfo.Add(WorldScript.Instance.m_nodes[i].GetComponent<NodeBase>());
        }
		// Add this node to its neighbors in case it wasn't there
        for (int i = 0; i < m_neighbors.Count; i++)
        {
            NodeBase n = m_neighbors[i].GetComponent<NodeBase>();
            if (n != null)
            {
                if (!n.m_neighborsIds.Contains(m_Id))
                {
                    n.m_neighbors.Add(transform);
                    n.m_neighborsInfo.Add(this);
                    n.m_neighborsIds.Add(m_Id);
                }
            }
        }
    }

    public abstract Cost GetCost();
    public virtual int GetProficency() { return 0; }
	public abstract string GetName();
	public abstract XMLNode GetSerialize ();
	public virtual void Deserialize(XMLNode node)
	{
		m_Id                = node.m_Id;
        m_neighborsIds      = node.neighbor;
        m_Origin 			= node.m_Origin;
        transform.parent 	= WorldScript.Instance.transform;
        transform.position 	= new Vector3(node.m_X, 0, node.m_Z);
	}

    public void UnlockSimulationNode()
    {
        if (bSimulationUnlock || bUnlocked)
            return;
        if (CanUnlockSimulation())
        {
            bSimulationUnlock = true;
        }
    }

    public void LockSimulationNode()
    {
        if (bUnlocked || m_Origin || !bSimulationUnlock)
            return;
        if (CanLockSimulation())
        {
            bSimulationUnlock = false;
        }
    }

    public void UnlockNode()
    {
        if (bUnlocked)
            return;
        if (CanUnlock())
        {
            bUnlocked = true;
        }
    }

    public void LockNode()
    {
        if (!bUnlocked || m_Origin)
            return;
        if (CanLock())
        {
            bUnlocked = false;
        }
    }

    public bool CanUnlock()
    {
        for (int i = 0; i < m_neighbors.Count; i++)
        {
            if (m_neighbors[i].GetComponent<NodeBase>().bUnlocked)
                return true;
        }
        return false;
    }

    public bool CanUnlockSimulation()
    {
        for (int i = 0; i < m_neighbors.Count; i++)
        {
            if (m_neighbors[i].GetComponent<NodeBase>().bUnlocked || m_neighbors[i].GetComponent<NodeBase>().bSimulationUnlock)
                return true;
        }
        return false;
    }

    public bool CanLock()
    {
        if (CanReachOrigin())
        {
            return true;
        }
        bUnlocked = true;
        return false;
    }

    public bool CanLockSimulation()
    {
        if (CanReachOrigin(true))
        {
            return true;
        }
        bSimulationUnlock = true;
        return false;
    }

    // Need ALL unlocked neighbors to be connected
    public bool CanReachOrigin(bool simulation = false)
    {
        if (m_Origin)
            return true;

        if(simulation) {
            bSimulationUnlock = false;
        }else{
            bUnlocked = false;
        }
        for (int i = 0; i < m_neighbors.Count; i++)
		{
            if(simulation)
            {
                if (m_neighbors[i].GetComponent<NodeBase>().bSimulationUnlock)
                {
                    if (!m_neighbors[i].GetComponent<NodeBase>().CanReachOriginRecc(simulation))
                    {          
                        bSimulationUnlock = true;
                        return false;
                    }
                }
            }
            else
            {
                if (m_neighbors[i].GetComponent<NodeBase>().bUnlocked)
                {
                    if (!m_neighbors[i].GetComponent<NodeBase>().CanReachOriginRecc(simulation))
                    {          
                        bUnlocked = true;
                        return false;
                    }
                }
            }
        }        
        if(simulation) {
            bSimulationUnlock = true;
        }else{
            bUnlocked = true;
        }
        return true;
    }
    
    // Only need ONE neighbor to be connected because any other that won't, will be connected to this one who has one connected.
    public bool CanReachOriginRecc(bool simulation = false)
    {
        if (m_Origin)
            return true;
        
        if(simulation) {
            bSimulationUnlock = false;
        }else{
            bUnlocked = false;
        }

        for (int i = 0; i < m_neighbors.Count; i++)
		{
            if(simulation)
            {
                if(m_neighbors[i].GetComponent<NodeBase>().bUnlocked)
                {
                    bSimulationUnlock = true;
                    return true;
                }
                if (m_neighbors[i].GetComponent<NodeBase>().bSimulationUnlock)
                {
                    if (m_neighbors[i].GetComponent<NodeBase>().CanReachOriginRecc(simulation))
                    {              
                        bSimulationUnlock = true;
                        return true;
                    }
                }
            }
            else
            {
                if (m_neighbors[i].GetComponent<NodeBase>().bUnlocked)
                {
                    if (m_neighbors[i].GetComponent<NodeBase>().CanReachOriginRecc(simulation))
                    {              
                        bUnlocked = true;
                        return true;
                    }
                }
            }
        }        
        if(simulation) {
            bSimulationUnlock = true;
        }else{
            bUnlocked = true;
        }
        return false;
    }

    public void FindShortestPath()
    {
		NodePath path = new NodePath ();
        path.Add(this);
		NodeBase curr = this;
		NodeBase bestNeigh = curr;
		while (!curr.bUnlocked) {
			foreach(NodeBase n in curr.m_neighborsInfo)
			{
				if(n.m_weight < bestNeigh.m_weight)
					bestNeigh = n;
			}
			path.Add(bestNeigh);
			curr = bestNeigh;
		}
        //NodePath ret = FindShortestPathRecc(new NodePath());
		WorldScript.Instance.HighlightPath = path;
    }

    /*
    public NodePath FindShortestPathRecc(NodePath _path)
    {
        if (_path.Contains(this))
            return null;
        if (bUnlocked)
            return null;

        NodePath listCopy = new NodePath(_path);
        NodePath shortest = null;
        listCopy.Add(this);
        for (int i = 0; i < m_neighbors.Count; i++)
        {
            if (m_neighbors[i].GetComponent<NodeBase>().bUnlocked)
            {
                listCopy.Add(m_neighbors[i].GetComponent<NodeBase>());
                return listCopy;
            }
            else
            {
                if (shortest == null)
                    shortest = m_neighbors[i].GetComponent<NodeBase>().FindShortestPathRecc(listCopy);
                else
                {
                    NodePath ret = m_neighbors[i].GetComponent<NodeBase>().FindShortestPathRecc(listCopy);
                    if (ret != null && (ret.Count < shortest.Count))
                        shortest = ret;
                }
            }
        }

        return shortest;
    }*/

	public void FindCheapestPath(){		
		NodePath ret = FindCheapestPathRecc(new NodePath ());
		WorldScript.Instance.HighlightPath = ret;
	}
	
	public NodePath FindCheapestPathRecc(NodePath _path)
	{
		if (_path.Contains(this))
			return null;
		if (bUnlocked)
			return null;
		
		NodePath listCopy = new NodePath(_path);
		NodePath shortest = null;
		listCopy.Add(this);
		for (int i = 0; i < m_neighbors.Count; i++)
		{
			if (m_neighbors[i].GetComponent<NodeBase>().bUnlocked)
			{
				listCopy.Add(m_neighbors[i].GetComponent<NodeBase>());
				return listCopy;
			}
			else
			{
				if (shortest == null)
					shortest = m_neighbors[i].GetComponent<NodeBase>().FindCheapestPathRecc(listCopy);
				else
				{
					NodePath ret = m_neighbors[i].GetComponent<NodeBase>().FindCheapestPathRecc(listCopy);
					if (ret != null && (ret.TotSparks < shortest.TotSparks))
						shortest = ret;
				}
			}
		}
		return shortest;
	}

    // Pas fini
	public void FindCheapestPathDijkstra()
	{
		List<int> dist = new List<int>();
		List<int> prev = new List<int>();
		List<NodeBase> Q = new List<NodeBase> ();

		foreach (Transform t in WorldScript.Instance.m_nodes.Values) {
			dist.Add(int.MaxValue);
			prev.Add(-1);
			Q.Add(t.GetComponent<NodeBase>());
		}
		dist [m_Id] = 0;

		NodeBase u = this;
		while (Q.Count > 0) {
			//Look for node with least dist
			foreach(NodeBase n in Q)
			{
				if(dist[n.m_Id] < dist[u.m_Id]) u = n; 
			}

			if(u.bUnlocked) break;

			Q.Remove(u);

			foreach(NodeBase neigh in u.m_neighborsInfo)
			{
				int alt = dist[u.m_Id] + 1;
				if(alt  < dist[neigh.m_Id])	
				{
					dist[neigh.m_Id] = alt;
					prev[neigh.m_Id] = u.m_Id;
				}
			}
		}
	}

    void OnDrawGizmos()
    {
        for (int i = 0; i < m_neighbors.Count; i++)
        {
            if (m_neighbors[i] != null)
            {
                if (m_neighbors[i].GetComponent<NodeBase>().bUnlocked && bUnlocked)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.blue;

                Gizmos.DrawLine(transform.position, m_neighbors[i].position);
            }
        }
    }

    void OnMouseDown()
    {
        if (WorldScript.Instance.SimulationMode)
        {
            if (bUnlocked) 
                return;
            if (bSimulationUnlock) LockSimulationNode();
            else UnlockSimulationNode();

            PathCostPanel.Instance.SetPanel(WorldScript.Instance.m_UnlockedPath_Simulation);
        }
        else
        {
            if (bUnlocked) LockNode();
            else UnlockNode();

			WorldScript.Instance.CalculateNodesWeight();
        }
    }

    void OnMouseEnter()
    {
		NodeToolTipScript.Instance.gameObject.SetActive(true);
		NodeToolTipScript.Instance.SetValues(this);
    }

    void OnMouseOver()
    {
		NodeToolTipScript.Instance.transform.position = Input.mousePosition;
    }

    void OnMouseExit()
    {
		NodeToolTipScript.Instance.gameObject.SetActive(false);
    }
}


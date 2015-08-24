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
	public int				m_weightCost;
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
                m_SpriteRenderer.color = EdgeScript.gold;
            }
            else
            {
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
    public virtual int GetGreatness() { return 0; }
    public virtual int GetMight() { return 0; }
    public virtual int GetStamina() { return 0; }
    public virtual int GetStrength() { return 0; }
    public virtual int GetValor() { return 0; }
    public virtual int GetLuck() { return 0; }
    public virtual int GetSpirit() { return 0; }
    public virtual int GetPrestige() { return 0; }
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
		while (!curr.bUnlocked) 
        {
            // Node is unreachable because of ignore pink node
            if(curr.m_weight == int.MaxValue)
            {
                UiManager.Instance.ShowAlertMessage("Couldn't find any path without pink nodes");
                return;
            }
			foreach(NodeBase n in curr.m_neighborsInfo)
			{
				if(n.m_weight < bestNeigh.m_weight)
					bestNeigh = n;
			}
			path.Add(bestNeigh);
			curr = bestNeigh;
		}
		WorldScript.Instance.HighlightPath = path;
    }

	public void FindAndHighlightCheapestPath()
    {		
        WorldScript.Instance.HighlightPath = FindCheapestPath();
	}

    public NodePath FindCheapestPath()
    {
        NodePath path = new NodePath();
        path.Add(this);
        NodeBase curr = this;
        NodeBase bestNeigh = curr;
        while (!curr.bUnlocked)
        {
            // Node is unreachable because of ignore pink node
            if (curr.m_weightCost == int.MaxValue)
            {
                UiManager.Instance.ShowAlertMessage("Couldn't find any path without pink nodes");
                return null;
            }
            foreach (NodeBase n in curr.m_neighborsInfo)
            {
                if (n.m_weightCost <= bestNeigh.m_weightCost)
                    bestNeigh = n;
            }
            path.Add(bestNeigh);
            curr = bestNeigh;
        }
        return path;
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


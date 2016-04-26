using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public abstract class NodeBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public delegate void dUpdateColor();
    public dUpdateColor UpdateColor;

    public Sprite Locked;
    public Sprite Unlocked;
    public Sprite Simulated;
    public Sprite Calculated;

	public int 				    m_Id;
    public List<Transform> m_neighbors;
    public List<NodeBase>    m_neighborsInfo;
    public List<int> m_neighborsIds;
	public int 				    m_weight;
	public int				    m_weightCost;
    public bool                 m_Origin;
    public bool m_edgeInit { get; set; }
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
                m_SpriteRenderer.sprite = Unlocked;
            }
            else if (bSimulationUnlock)
            {
                m_SpriteRenderer.sprite = Simulated;
            }
			else if(value)
            {
                m_SpriteRenderer.sprite = Calculated;
			}
			else
            {
                m_SpriteRenderer.sprite = Locked;
			}
			m_highLight = value;
            UpdateColor();
		}
	}
    private bool            m_bSimulationUnlock;
    public bool             bSimulationUnlock
    {
        get
        {
			return (m_bSimulationUnlock);
		}
		set
        {
            if (value)
            {
                m_SpriteRenderer.sprite = Simulated;

                if (m_bSimulationUnlock) return; // Prevent bug that add node cost even if its already unlocked
                WorldScript.Instance.UnlockedPath_Simulation.Add(this);
            }
            else
            {
				if(bUnlocked)
                {
                    m_SpriteRenderer.sprite = Unlocked;
				}
				else if(HighLight)
                {
                    m_SpriteRenderer.sprite = Calculated;			
				}
				else
                {
                    m_SpriteRenderer.sprite = Locked;
                }

                if (!m_bSimulationUnlock) return; // Prevent bug that remove node cost even if its already locked
                WorldScript.Instance.UnlockedPath_Simulation.Remove(this);
            }
            m_bSimulationUnlock = value;
            UpdateColor();
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
                if (bSimulationUnlock)
                {
                    m_SpriteRenderer.sprite = Simulated;
                }
                else
                {
                    m_SpriteRenderer.sprite = Unlocked;
                }
            }
            else
            {
                m_SpriteRenderer.sprite = Locked;
            }
            m_bUnlocked = value;
            UpdateColor();
        }
    }

    public SpriteRenderer m_SpriteRenderer;

    void Awake()
	{
        m_edgeInit = false;
        transform.SetParent(WorldScript.Instance.transform);
	}

    public void InitNode()
    {
		// Create this node neighbors lists
        foreach(int i in m_neighborsIds)
        {
            Transform neigh = WorldScript.Instance.m_nodes[i];
            NodeBase n = neigh.GetComponent<NodeBase>();
            m_neighbors.Add(neigh);
            m_neighborsInfo.Add(n);

            if(!n.m_neighborsIds.Contains(m_Id))
            {
                n.m_neighbors.Add(transform);
                n.m_neighborsInfo.Add(this);
                n.m_neighborsIds.Add(m_Id);
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
        foreach (int i in node.neighbor)
            m_neighborsIds.Add(i);
        m_Origin 			= node.m_Origin;
        transform.parent 	= WorldScript.Instance.transform;
        transform.position 	= new Vector3(node.m_X, 0, node.m_Z);

        if (m_Origin)
            m_SpriteRenderer.sprite = Unlocked;
	}

    public void TryUnlockSimulationNode()
    {
        if (bSimulationUnlock || bUnlocked) return;
        if (CanUnlock()) bSimulationUnlock = true;
    }

    public void TryLockSimulationNode()
    {
        if (bUnlocked || !bSimulationUnlock)  return;
        if (CanLockSimulation()) bSimulationUnlock = false;
    }

    public bool TryUnlockNode()
    {
        if (bUnlocked) return false;
        if (CanUnlock ()) {
			bUnlocked = true;
			return true;
		}
		return false;
    }

    public void TryLockNode()
    {
        if (!bUnlocked || m_Origin) return;
        if (CanLock()) bUnlocked = false;
    }

    public bool CanUnlock()
    {
        foreach (NodeBase n in m_neighborsInfo)
        {
            if (n.bUnlocked || n.bSimulationUnlock)
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
        if (CanReachOrigin())
        {
            return true;
        }
        bSimulationUnlock = true;
        return false;
    }

    // Need ALL unlocked neighbors to be connected
    public bool CanReachOrigin()
    {
        if (m_Origin)
            return true;

        bool tmpS = bSimulationUnlock;
        bool tmpU = bUnlocked;

        if (tmpS)
        {
            bSimulationUnlock = false;
        }
        else
        {
            bUnlocked = false;
        }

        foreach (NodeBase n in m_neighborsInfo)
		{
            if (n.bUnlocked || n.bSimulationUnlock)
            {
                if (!n.CanReachOriginRecc())
                {
                    if (tmpS)
                    {
                        bSimulationUnlock = tmpS;
                    }
                    else
                    {
                        bUnlocked = tmpU;
                    }
                    return false;
                }
            }
        }
        if (tmpS)
        {
            bSimulationUnlock = tmpS;
        }
        else
        {
            bUnlocked = tmpU;
        }
        return true;
    }
    
    // Only need ONE neighbor to be connected because any other that won't, will be connected to this one who has one connected.
    public bool CanReachOriginRecc()
    {
        if (m_Origin)
            return true;

        bool tmpS = bSimulationUnlock;
        bool tmpU = bUnlocked;

        if (tmpS)
        {
            bSimulationUnlock = false;
        }
        else
        {
            bUnlocked = false;
        }

        foreach (NodeBase n in m_neighborsInfo)
		{
            if (n.bUnlocked || n.bSimulationUnlock)
            {
                if (n.CanReachOriginRecc())
                {
                    if (tmpS)
                    {
                        bSimulationUnlock = tmpS;
                    }
                    else
                    {
                        bUnlocked = tmpU;
                    }
                    return true;
                }
            }
        }
        if (tmpS)
        {
            bSimulationUnlock = tmpS;
        }
        else
        {
            bUnlocked = tmpU;
        }

        return false;
    }

    public void FindShortestPath()
    {
		NodePath path = new NodePath ();
        path.Add(this);
		NodeBase curr = this;
		NodeBase bestNeigh = curr;
		while (!curr.bUnlocked && !curr.bSimulationUnlock) 
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
        path.Remove(curr); // Since its already unlocked
		WorldScript.Instance.HighlightPath = path;
    }

	public void FindAndHighlightCheapestPath()
    {		
        NodePath path = FindCheapestPath();
        if (path == null)
        {
            UiManager.Instance.ShowAlertMessage("Couldn't find any path without pink nodes");
        }
        else
        {
            WorldScript.Instance.HighlightPath = path;
        }
	}

    public NodePath FindCheapestPath()
    {
        NodePath path = new NodePath();
        path.Add(this);
        NodeBase curr = this;
        NodeBase bestNeigh = curr;
        while (!curr.bUnlocked && !curr.bSimulationUnlock)
        {
            // Node is unreachable because of ignore pink node
            if (curr.m_weightCost == int.MaxValue)
            {
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
        path.Remove(curr); // Since its already unlocked
        return path;
    }

    void OnDrawGizmos()
    {
        foreach (Transform n in m_neighbors)
        {
            if (n.GetComponent<NodeBase>().bUnlocked && bUnlocked)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            Gizmos.DrawLine(transform.position, n.position);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!(Input.GetMouseButton(0) || Input.GetMouseButton(2)))
        {
            NodeToolTipScript.Instance.gameObject.SetActive(true);
            NodeToolTipScript.Instance.SetValues(this);
            NodeToolTipScript.Instance.SetAtMousePosition();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!(Input.GetMouseButton(0) || Input.GetMouseButton(2)))
        {
            NodeToolTipScript.Instance.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UiManager.Instance.ShowRightClickPanel(Input.mousePosition, this);
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (SimulationScript.Instance.SimulationMode)
            {
                if (bUnlocked) // To not let the user lock gold nodes in simulation
                    return;
                if (bSimulationUnlock) TryLockSimulationNode();
                else TryUnlockSimulationNode();

                UiManager.Instance.CostInfoPanel_Simulated.SetPanel(WorldScript.Instance.UnlockedPath_Simulation);
            }
            else
            {
                if (bUnlocked) TryLockNode();
                else TryUnlockNode();
            }
            WorldScript.Instance.CalculateNodesWeight();
        }
    }
}


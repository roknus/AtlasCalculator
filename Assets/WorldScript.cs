using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Serializer;

[ExecuteInEditMode] 
public class WorldScript : MonoBehaviour
{
	public bool EDITOR_MODE;
	public bool ATLAS_INITIALIZED;

	public bool SimulationMode;

    public Transform ClassNode;
    public Transform EtherNode;
    public Transform GodForm;

    public Transform GreatnessNode;
    public Transform LuckNode;
    public Transform MightNode;
    public Transform PurpleNode;
    public Transform SpiritNode;
    public Transform StaminaNode;
    public Transform StrenghtNode;
    public Transform TalentNode;
    public Transform ValorNode;
    
	public Transform        edge;
	public Dictionary<int, Transform>  m_nodes;

    public  NodePath        m_UnlockedPath;
    public  NodePath        m_UnlockedPath_Simulation;
	private NodePath        m_hightLightPath;

    private bool m_IgnorePinkNodes;
    public bool IgnorePinkNodes
    {
        get { return m_IgnorePinkNodes; }
        set { m_IgnorePinkNodes = value; }
    }

    public NodePath      HighlightPath
    { 
		get{ return m_hightLightPath; }
		set{
			CleanHighlight();
			if(value != null)
			{
				m_hightLightPath = value;
	            PathCostPanel.Instance.gameObject.SetActive(true);
                PathCostPanel.Instance.SetPanel(m_hightLightPath);
                if (PathStatsPanel.Instance != null)
                {
                    PathStatsPanel.Instance.SetPanel(m_hightLightPath);
                }
	            foreach (NodeBase n in m_hightLightPath.Path)
	            {
					n.HighLight = true;
				}
			}
		}
	}
	private string XMLAtlasGraph;

	public static WorldScript Instance { get; private set; }

	void Awake()
    {
		if (Instance != null) 
        {
			Destroy (gameObject);
			return;
		}
		else
		{
			Instance = this;
		}

		SimulationMode = false;
		m_UnlockedPath_Simulation = new NodePath ();
		m_nodes = new Dictionary<int, Transform>();

		if (EDITOR_MODE && !Application.isPlaying && !ATLAS_INITIALIZED) {
			StartCoroutine ("LoadAtlasXML");
			ATLAS_INITIALIZED = true;
		} else if (!EDITOR_MODE && Application.isPlaying) {
			StartCoroutine("LoadAtlasXML");			
		}
		else {
			InitNodes();
			InitEdges();
		}
	}

	void Start () 
	{
		if(!Application.isPlaying)
			return;

        UiManager.Instance.ShowCalculatorUI();
        UiManager.Instance.InitCalculatorUI();

        CTalentNode.Init();

        CalculateNodesWeight();
	}

	IEnumerator LoadAtlasXML()
	{
        WWW ret = new WWW("http://" + User.ServerHostname + "/atlas/AtlasCalculator/atlas3.xml");	
		
		yield return ret;
		
		// check for errors
		if (!string.IsNullOrEmpty(ret.error)) {
			Debug.Log ("WWW error : " + ret.error);
		} else {			
			XMLAtlasGraph = ret.text;
			LoadXML ();

            if(EDITOR_MODE)
            {
                // Do nothing
            }
            else if (User.Instance.Connected)
            {
                User.Instance.StartLoadUserXML();
			}
            else
            {
                User.Instance.StartLoadDefaultUserXML();
                Debug.Log("User is not connected or graph isnt loaded");
            }
		}
	}

	IEnumerator SendUserGraph(StringWriter _data)
	{		
		WWWForm form = new WWWForm ();
		form.AddField ("xml", _data.ToString());
        WWW www = new WWW("http://" + User.ServerHostname + "/atlas/AtlasCalculator/save_user_graph.php", form);	
		
		yield return www;

		if (www.error != null) {
			Debug.Log (www.error);
		}
	}

	public void InitNodes()
    {
		foreach (Transform c in transform)
        {
			NodeBase node = c.GetComponent<NodeBase>();
			if (EDITOR_MODE && node.m_Id == -1)
            { // New node in editor get a new ID
				node.m_Id = m_nodes.Count + 1;
            }

			// Create Dictionnary of all nodes
			m_nodes.Add(node.m_Id, c);
		}
	}

	public void ResetNodeWeight()
	{		
		// Init the base path too
		m_UnlockedPath = new NodePath();

		foreach (Transform c in transform)
		{
			NodeBase n = c.GetComponent<NodeBase>();
			if(n.bUnlocked)
			{
				m_UnlockedPath.Add(n);
				n.m_weight = 0;
				n.m_weightCost = 0;
			}
			else
			{
				n.m_weight = int.MaxValue;
				n.m_weightCost = int.MaxValue;
			} 
		}
	}	

    public void SwitchIgnorePinkNodes(bool _b)
    {
        IgnorePinkNodes = !IgnorePinkNodes;
        CalculateNodesWeight();
    }
	
	public void CalculateNodesWeight()
	{
		ResetNodeWeight ();
		
		foreach (NodeBase n in m_UnlockedPath.Path) 
		{
			foreach (NodeBase neigh in n.m_neighborsInfo)
            {
                // Avoid pink nodes
                if (WorldScript.Instance.IgnorePinkNodes && neigh is NodeWithCost)
                {
                    NodeWithCost nc = neigh as NodeWithCost;
                    if (nc.m_CostType == CostType.PinkSparks)
                    {
                        continue;
                    }
                }

				// If the neighbors has lower cost by passing by this node, update it
				if(neigh.m_weight > n.m_weight+1)
				{
					neigh.m_weight = 1;
					CalculateWeightRecc(neigh);
				}
				int cost = neigh.GetCost().Tot;
				if(neigh.m_weightCost > n.m_weightCost + cost)
				{
					neigh.m_weightCost = cost;
					CalculateWeightCostRecc(neigh);
				}
			}
		}
	}
	
	public void CalculateWeightRecc(NodeBase n)
	{		
		foreach(NodeBase neigh in n.m_neighborsInfo)
        {
            // Avoid pink nodes
            if (WorldScript.Instance.IgnorePinkNodes && neigh is NodeWithCost)
            {
                NodeWithCost nc = neigh as NodeWithCost;
                if (nc.m_CostType == CostType.PinkSparks)
                {
                    continue;
                }
            }

			if(neigh.m_weight > n.m_weight+1)
			{
				neigh.m_weight = n.m_weight+1;
				CalculateWeightRecc(neigh);
			}
		}
	}

	public void CalculateWeightCostRecc(NodeBase n)
	{
		foreach (NodeBase neigh in n.m_neighborsInfo)
        {
            // Avoid pink nodes
            if (WorldScript.Instance.IgnorePinkNodes && neigh is NodeWithCost)
            {
                NodeWithCost nc = neigh as NodeWithCost;
                if (nc.m_CostType == CostType.PinkSparks)
                {
                    continue;
                }
            }

			int cost = neigh.GetCost().Tot;
			if(neigh.m_weightCost > n.m_weightCost + cost)
			{
				neigh.m_weightCost = n.m_weightCost + cost;
				CalculateWeightCostRecc(neigh);
			}
		}
	}

	public void InitEdges()
	{		
		foreach (Transform n in m_nodes.Values) {
			NodeBase ns = n.GetComponent<NodeBase>();
			foreach(Transform neigh in ns.m_neighbors)
			{
				Transform e = Instantiate<Transform>(edge);
				EdgeScript es = e.GetComponent<EdgeScript>();
				es.Node1 = n;
				es.Node2 = neigh;
			}
		}		
	}
	
	public void CleanHighlight()
    {
        PathCostPanel.Instance.gameObject.SetActive(false);
		if(m_hightLightPath != null)
            foreach (NodeBase n in m_hightLightPath.Path)
            {
				n.HighLight = false;
			}
	}

    public void SaveAtlas()
    {
        if (EDITOR_MODE)
            SaveXML();
        else
            SaveUserGraph();
    }

    public void SwitchSimulation()
    {
		SimulationMode = !SimulationMode;

        if(!SimulationMode)
        {
            PathCostPanel.Instance.Clean();
            foreach(Transform n in m_nodes.Values)
            {
                n.GetComponent<NodeBase>().bSimulationUnlock = false;
            }
            UiManager.Instance.SimulationButton.GetComponentInChildren<Text>().text = "Simulation (OFF)";
        }
        else
        {
            m_UnlockedPath_Simulation = new NodePath();
            UiManager.Instance.SimulationButton.GetComponentInChildren<Text>().text = "Simulation (ON)";
        }
    }

	public void SaveXML()
	{
		var serializer = new XmlSerializer(typeof(NodeSerializer));
		NodeSerializer nodeSerializer = new NodeSerializer();
		using(var stream = new FileStream("C:/atlas.xml", FileMode.Create))
		{
			foreach(Transform t in m_nodes.Values)
			{
				nodeSerializer.Add(t.GetComponent<NodeBase>());
			}
			serializer.Serialize(stream, nodeSerializer);
		}
		Debug.Log("Serialization Done !");
	}

	public void LoadXML()
	{ 
		m_nodes.Clear();
		var serializer = new XmlSerializer(typeof(NodeSerializer));	
		NodeSerializer nodeSerializer;
		using(XmlReader stream = XmlReader.Create(new StringReader(XMLAtlasGraph)))
		{
			nodeSerializer = serializer.Deserialize(stream) as NodeSerializer;
		}		

		foreach(XMLNode n in nodeSerializer.Nodes)
		{
			Transform t = null;
			if(n is XMLNodeWithTwoStat)
			{
				switch(((XMLNodeWithTwoStat)n).m_Stat1)
				{
				case (int)Stat1.Might :
					t = Instantiate(MightNode) as Transform;
					break;
				case (int)Stat1.Stamina :
					t = Instantiate(StaminaNode) as Transform;
					break;
				}
			}
			else if(n is XMLNodeWithOneStat)
			{
                //Special case for purple nodes
                if (((XMLNodeWithOneStat)n).m_Cost.type == (int)(CostType.PinkSparks))
                {
                    t = Instantiate(PurpleNode) as Transform;
                }
                else
                {
                    switch (((XMLNodeWithOneStat)n).m_Stat1)
                    {
                        case (int)Stat1.Greatness:
                            t = Instantiate(GreatnessNode) as Transform;
                            break;
                        case (int)Stat1.Luck:
                            t = Instantiate(LuckNode) as Transform;
                            break;
                        case (int)Stat1.Spirit:
                            t = Instantiate(SpiritNode) as Transform;
                            break;
                        case (int)Stat1.Strength:
                            t = Instantiate(StrenghtNode) as Transform;
                            break;
                        case (int)Stat1.Valor:
                            t = Instantiate(ValorNode) as Transform;
                            break;
                    }
                }
			}
			else if(n is XMLTalentNode)
			{
				t = Instantiate(TalentNode) as Transform;
			}
			else if (n is XMLClassNode)
			{
				t = Instantiate(ClassNode) as Transform;
			}
			else if(n is XMLGodForm)
			{
				t = Instantiate(GodForm) as Transform;
			}	
			else if (n is XMLEtherNode)
			{
				t = Instantiate(EtherNode) as Transform;
			}

            if (t != null)
            {
                t.GetComponent<NodeBase>().Deserialize(n);
            }
            else
            {
                Debug.Log("Node has not been loaded");
            }
		}
        /* Shouldn't work since its in the same frame as init*/
		InitNodes();
        foreach (Transform c in transform)
        {
            c.GetComponent<NodeBase>().InitNode();
        }
        InitEdges();

		Debug.Log("Deserialization Done !");
	}

	private void SaveUserGraph()
	{
		var serializer = new XmlSerializer(typeof(UserNodeSerializer));
		UserNodeSerializer nodeSerializer = new UserNodeSerializer();

		StringWriter stream = new StringWriter ();
		foreach(Transform t in m_nodes.Values)
		{
			if(t.GetComponent<NodeBase>().bUnlocked)
				nodeSerializer.Add(t.GetComponent<NodeBase>());
		}
		serializer.Serialize(stream, nodeSerializer);
		StartCoroutine("SendUserGraph", stream);

		Debug.Log("Serialization of user graph Done !");
	}

	public void LoadUserGraph()
	{
		var serializer = new XmlSerializer(typeof(UserNodeSerializer));	
		UserNodeSerializer nodeSerializer;
		using(XmlReader stream = XmlReader.Create(new StringReader(User.Instance.XMLUserGraph)))
		{
			nodeSerializer = serializer.Deserialize(stream) as UserNodeSerializer;
		}

		foreach (int i in nodeSerializer.Nodes) 
        {
            NodeBase n = m_nodes[i].GetComponent<NodeBase>();
            if (n)
            {
                m_UnlockedPath.Add(n);
                n.bUnlocked = true;
            }
		}

        CalculateNodesWeight();
	}

	public static int CompareProficency(NodeBase n1, NodeBase n2)
	{
		if (n1 is NodeWithTwoStat && n2 is NodeWithTwoStat) {
			if(n1.GetCost() < n2.GetCost())
				return -1;
			else if(n1.GetCost() > n2.GetCost())
				return 1;
			return 0;
		}
		if (n1 is NodeWithTwoStat)
			return -1;
		if (n2 is NodeWithTwoStat)
			return 1;

		return (n1.GetCost() < n2.GetCost() ? -1 : 1);
	}

    public void Optimize(int proficency)
	{
        UiManager.Instance.ButtonProficencyText.text = "Calculating ...";
        UiManager.Instance.ButtonProficency.onClick.RemoveAllListeners();
        UiManager.Instance.ButtonProficency.onClick.AddListener(() => { InteruptOptimization(); });
		CleanHighlight();
        StartCoroutine(StartOptimize(proficency));
    }

    public void InteruptOptimization()
    {
        StopAllCoroutines();

		if (best != null) {
			NodePath bestPath = new NodePath ();
			foreach (NodeBase n in best) {
				bestPath.Add (n);
			}
			HighlightPath = bestPath;
		}

        UiManager.Instance.ButtonProficency.onClick.RemoveAllListeners();
        UiManager.Instance.ButtonProficency.onClick.AddListener(() => { UiManager.Instance.OpenProficencyWindow(); });
        UiManager.Instance.ButtonProficencyText.text = "Optimize Proficency";
	}
	
    public IEnumerator StartOptimize(int proficency)
    {
        best = null;
        List<NodeBase> voisin       = new List<NodeBase>();
        List<NodeBase> path         = new List<NodeBase>(m_UnlockedPath.Path);
        foreach (NodeBase n in m_UnlockedPath.Path)
        {
            foreach(NodeBase neigh in n.m_neighborsInfo)
            {
                if (!neigh.bUnlocked && !voisin.Contains(neigh))
                {
                    voisin.Add(neigh);
                }
            }
        }
		voisin.Sort (CompareProficency);
        yield return StartCoroutine(OptimizeRecc(voisin, path, 0, new Cost(0,0,0), proficency, 0));
		
		if (best != null) {
			NodePath bestPath = new NodePath ();
			foreach (NodeBase n in best) {
				bestPath.Add (n);
			}
			HighlightPath = bestPath;
		}

        UiManager.Instance.ButtonProficencyText.text = "Optimize Proficency";
	}
	
	public static List<NodeBase> best;
    public static Cost bestSparks;
    public IEnumerator OptimizeRecc(List<NodeBase> voisin, List<NodeBase> path, int proficency, Cost sparks, int val, int offset)
    {
        if(voisin.Count < 1) yield break;
        if (offset >= voisin.Count) yield break;

		List<NodeBase> newVoisin = new List<NodeBase>();
		for (int i = offset; i < voisin.Count; i++)
        {
			newVoisin.Add(voisin[i]);
		}

        List<NodeBase> pathCopy = new List<NodeBase>(path);

        NodeBase curr = newVoisin[0];
        pathCopy.Add(curr);
		int newPro = proficency + curr.GetProficency();
		Cost newSparks = sparks + curr.GetCost();
		
		if (newPro >= val)
        {
            if (best != null)
            {
                // If it's cheaper make it best !
                if (bestSparks > newSparks)
                { 
                    best = pathCopy;
                    bestSparks = newSparks;
                    UiManager.Instance.ButtonProficencyText.text = "Calculating (Best : " + bestSparks.Tot + " Sparks)";
				}
			} 
			else
			{ 
				best = pathCopy;
				bestSparks = newSparks;
                UiManager.Instance.ButtonProficencyText.text = "Calculating (Best : " + bestSparks.Tot + " Sparks)";
			}
		}
        else
		{			
			newVoisin.Remove (curr);
			
			/** Heuristic 1
			 * Sort the neighbors by their type, there is a big chance that the best path will be the one with most of green/red nodes.
			 * Plus, the ratio of cost/proficency is better as the cost is low, so sort it also by cost 
		 	*/ 
			foreach (NodeBase n in curr.m_neighborsInfo)
			{
				if (!newVoisin.Contains(n) &&!pathCopy.Contains(n))
				{
					if(newVoisin.Count == 0)
					{
						newVoisin.Add(n);					
					}
					else
					{
						int i;
						for(i = 0; i < newVoisin.Count; i++)
						{
							if(CompareProficency(n, newVoisin[i]) == -1)
							{
								break;
							}
						}
						if(i == newVoisin.Count) // Worst case when the n is the most expensive
						{
							newVoisin.Insert(i,n);
						}
						else // Insert the node right after 
						{
							newVoisin.Insert(i+1,n);
						}
					}
				}
			}
			bool cont = true;
            /** Heuristic 2
             * Calculate the difference between the current best sparks and current path sparks
             * the top ratio for proficency is 23 sparks for 1 proficency
             * So if sparkLeft * topRatio < val no need to continue because you won't be able te beat best even with perfect nodes
            */
            if (best != null)
            {
                Cost sparksLeft = bestSparks - newSparks;
                if ((newPro + (sparksLeft.Tot / 23.0f)) < val)
                {
					cont = false;
				}
			}
			if(cont)
            	yield return StartCoroutine(OptimizeRecc(newVoisin, pathCopy, newPro, newSparks, val, 0));
			
			if (offset < voisin.Count)
			{
				yield return StartCoroutine(OptimizeRecc(voisin, path, proficency, sparks, val, offset + 1));
			}
		}
	}

    public void Maximize(int r, int g, int b, int sparksLeft)
    {
        UiManager.Instance.ButtonProficencyText.text = "Calculating ...";
        UiManager.Instance.ButtonProficency.onClick.RemoveAllListeners();
        UiManager.Instance.ButtonProficency.onClick.AddListener(() => { InteruptOptimization(); });
        CleanHighlight();
        StartCoroutine(StartMaximize(r, g, b, sparksLeft));
    }

	public IEnumerator StartMaximize(int r, int g, int b, int sparksLeft)
    {
        best = null;
        bestProficency = 0;
        List<NodeBase> voisin = new List<NodeBase>();
        List<NodeBase> path = new List<NodeBase>(m_UnlockedPath.Path);
        foreach (NodeBase n in m_UnlockedPath.Path)
        {
            foreach (NodeBase neigh in n.m_neighborsInfo)
            {
                if (!neigh.bUnlocked && !voisin.Contains(neigh))
                {
                    voisin.Add(neigh);
                }
            }
        }
        voisin.Sort(CompareProficency);
		yield return StartCoroutine(MaximizeRecc(voisin, path, 0, new Cost(r, g, b, sparksLeft), 0));

        if (best != null)
        {
            NodePath bestPath = new NodePath();
            foreach (NodeBase n in best)
            {
                bestPath.Add(n);
            }
            HighlightPath = bestPath;
        }

        UiManager.Instance.ButtonProficencyText.text = "Optimize Proficency";
    }
    
    public static int bestProficency;
    public IEnumerator MaximizeRecc(List<NodeBase> voisin, List<NodeBase> path, int proficency, Cost sparks, int offset)
    {
        if (voisin.Count < 1) yield break;
        if (offset >= voisin.Count) yield break;

        List<NodeBase> newVoisin = new List<NodeBase>();
        for (int i = offset; i < voisin.Count; i++)
        {
            newVoisin.Add(voisin[i]);
        }

        List<NodeBase> pathCopy = new List<NodeBase>(path);

        NodeBase curr = newVoisin[0]; 
        pathCopy.Add(curr);
        int newPro = proficency + curr.GetProficency();
        Cost newSparks = sparks - curr.GetCost();

        if (newSparks.isNegative()) yield break;

        if (newPro > bestProficency || (newPro == bestProficency && newSparks.Tot > bestSparks.Tot))
        {
            //Debug.Log(newPro + " " + newSparks.R + " " + newSparks.G + " " + newSparks.B + " ");
            best = pathCopy;
            bestProficency = newPro;
            bestSparks = newSparks;
            UiManager.Instance.ButtonProficencyText.text = "Calculating (Best : " + bestProficency + " Proficency)";
        }

        newVoisin.Remove(curr);
            
		/** Heuristic 1
			* Sort the neighbors by their type, there is a big chance that the best path will be the one with most of green/red nodes.
			* Plus, the ratio of cost/proficency is better as the cost is low, so sort it also by cost 
		*/ 
		foreach (NodeBase n in curr.m_neighborsInfo)
		{
			if (!newVoisin.Contains(n) &&!pathCopy.Contains(n))
			{
				newVoisin.Add(n);	
			}
		}
        newVoisin.Sort(CompareProficency);

		bool cont = true;
        /** Heuristic 2
            * Calculate the difference between the current best sparks and current path sparks
            * the top ratio for proficency is 23 sparks for 1 proficency
            * So if sparkLeft * topRatio < val no need to continue because you won't be able te beat best even with perfect nodes
        */
        if (best != null)
        {
            //Cost sparksLeft = sparks - newSparks;
            if ((newPro + (newSparks.Tot / 23.0f)) < bestProficency)
            {
                cont = false;
            }
		}
		if(cont)
            yield return StartCoroutine(MaximizeRecc(newVoisin, pathCopy, newPro, newSparks, 0));

        if (offset < voisin.Count)
        {
            yield return StartCoroutine(MaximizeRecc(voisin, path, proficency, sparks, offset + 1));
        }
    }
}

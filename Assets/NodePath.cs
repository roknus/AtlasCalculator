using UnityEngine;
using System.Collections.Generic;

public class NodePath
{
    public HashSet<NodeBase> Path { get; set; }
    public int Count { get { return Path.Count; } }

    public Cost Cost;
    public int Red { get { return Cost.R; } }
    public int Green { get { return Cost.G; } }
    public int Blue { get { return Cost.B; } }
    public int TotSparks { get { return Cost.Tot; } }

	public int PurpleSparks { get { return Cost.Purple; } }

    public int Proficency;

    public NodePath()
    {
        Path = new HashSet<NodeBase>();
        Cost = new Cost(0, 0, 0);
        Proficency = 0;
    }
    public NodePath(NodePath _other)
    {
        Path = new HashSet<NodeBase>(_other.Path);
        Cost = new Cost(_other.Cost);
        Proficency = _other.Proficency;
    }

    public void Add(NodeBase _n) 
    {
		if (_n != null)
        {
            Path.Add(_n);
            if (!_n.bUnlocked)
            {
                Cost += _n.GetCost();
                Proficency += _n.GetProficency();
            }
		}
    }

    public void Remove(NodeBase _n)
    {
		if (_n != null )
        {
            Path.Remove(_n);

            Cost -= _n.GetCost();
            Proficency -= _n.GetProficency();
        }
    }

    public bool Contains(NodeBase _n) { return Path.Contains(_n); }
}

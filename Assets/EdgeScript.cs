using UnityEngine;
using System.Collections;

public class EdgeScript : MonoBehaviour {

	public Transform Node1;
	public Transform Node2;

    private Color m_Color1;
    private Color m_Color2;

	public static Color gold = new Color(0.77f, 0.7f, 0.34f);
    public static Color lightBlue = new Color(0.2f, 0.2f, 0.7f);

	private LineRenderer m_lineRenderer;

	void Start () 
	{
		m_lineRenderer = GetComponent<LineRenderer> ();

        Vector3 dir = Node2.position - Node1.position;
        dir.Normalize();
		if (m_lineRenderer && Node1 && Node2) {
			m_lineRenderer.SetPosition (0, Node1.position + dir * 0.5f);
			m_lineRenderer.SetPosition (1, Node2.position - dir * 0.5f);
		}
	}
	
	void Update () 
    {
		if(Node1 == null || Node2 == null)
		{
			Destroy(gameObject);
			return;
		}

		m_Color1 = lightBlue;
		m_Color2 = lightBlue;

        m_lineRenderer.SetWidth(0.3f, 0.3f);

		if ((Node1.GetComponent<NodeBase>().HighLight && Node2.GetComponent<NodeBase>().HighLight) ||
		    (Node1.GetComponent<NodeBase>().bUnlocked && Node2.GetComponent<NodeBase>().HighLight) ||
		    (Node1.GetComponent<NodeBase>().HighLight && Node2.GetComponent<NodeBase>().bUnlocked))
        {
            m_Color1 = Color.green;
            m_Color2 = Color.green;
            m_lineRenderer.SetWidth(0.6f, 0.6f);
        }

        if ((Node1.GetComponent<NodeBase>().bSimulationUnlock && Node2.GetComponent<NodeBase>().bSimulationUnlock) ||
            (Node1.GetComponent<NodeBase>().bUnlocked && Node2.GetComponent<NodeBase>().bSimulationUnlock) ||
            (Node1.GetComponent<NodeBase>().bSimulationUnlock && Node2.GetComponent<NodeBase>().bUnlocked))
        {
            m_Color1 = Color.red;
            m_Color2 = Color.red;
            m_lineRenderer.SetWidth(0.6f, 0.6f);
        }

        if (Node1.GetComponent<NodeBase>().bUnlocked && Node2.GetComponent<NodeBase>().bUnlocked)
        {
            m_Color1 = gold;
            m_Color2 = gold;
            m_lineRenderer.SetWidth(0.6f, 0.6f);
        }

        m_lineRenderer.SetColors(m_Color1, m_Color2);		
	}
}

using UnityEngine;
using System.Collections;

public class EdgeScript : MonoBehaviour {

	public Transform Node1;
	public Transform Node2;

    private NodeBase m_Node1;
    private NodeBase m_Node2;

    private Color m_Color1;
    private Color m_Color2;

    public Color unlocked;
    public Color locked;
    public Color calculated;

	//private LineRenderer m_lineRenderer;
	private SpriteRenderer m_SpriteRenderer;

	void Start ()
    {
        m_Node1 = Node1.GetComponent<NodeBase>();
        m_Node2 = Node2.GetComponent<NodeBase>();

		//m_lineRenderer = GetComponent<LineRenderer> ();
		m_SpriteRenderer = GetComponent<SpriteRenderer> ();

        Vector3 dir = Node2.position - Node1.position;
        dir.Normalize();
		if (m_SpriteRenderer && Node1 && Node2) 
		{
			transform.position = ((Node1.position + (dir * m_Node1.transform.localScale.x / 2.2f)) + (Node2.position - (dir * m_Node2.transform.localScale.x / 2.2f))) / 2;
			transform.LookAt(Node2.position);
			transform.Rotate(new Vector3(90,0,0));
			transform.localScale = new Vector3(0.5f, Vector3.Distance(Node1.position + (dir * m_Node1.transform.localScale.x / 2.2f) , Node2.position - (dir * m_Node2.transform.localScale.x / 2.2f)) * 1.55f, 0.5f);

		
            //Trick to make edge longer or shorter depending on the node size
			//m_lineRenderer.SetPosition (0, Node1.position + dir * m_Node1.transform.localScale.x / 2.2f);
			//m_lineRenderer.SetPosition(1, Node2.position - dir * m_Node2.transform.localScale.x / 2.2f);
		}    

        m_Node1.UpdateColor += UpdateColor;
        m_Node2.UpdateColor += UpdateColor;

        UpdateColor();
	}
	
	public void UpdateColor () 
    {
		if(Node1 == null || Node2 == null)
		{
			Destroy(gameObject);
			return;
		}

		m_SpriteRenderer.color = locked;

		if ((Node1.GetComponent<NodeBase>().HighLight && Node2.GetComponent<NodeBase>().HighLight) ||
		    (Node1.GetComponent<NodeBase>().bUnlocked && Node2.GetComponent<NodeBase>().HighLight) ||
            (Node1.GetComponent<NodeBase>().HighLight && Node2.GetComponent<NodeBase>().bUnlocked) ||
            (Node1.GetComponent<NodeBase>().bSimulationUnlock && Node2.GetComponent<NodeBase>().HighLight) ||
            (Node1.GetComponent<NodeBase>().HighLight && Node2.GetComponent<NodeBase>().bSimulationUnlock))
        {
			m_SpriteRenderer.color = calculated;
        }

        if (Node1.GetComponent<NodeBase>().bUnlocked && Node2.GetComponent<NodeBase>().bUnlocked)
        {
			m_SpriteRenderer.color = unlocked;
        }

        if ((Node1.GetComponent<NodeBase>().bSimulationUnlock && Node2.GetComponent<NodeBase>().bSimulationUnlock) ||
            (Node1.GetComponent<NodeBase>().bUnlocked && Node2.GetComponent<NodeBase>().bSimulationUnlock) ||
            (Node1.GetComponent<NodeBase>().bSimulationUnlock && Node2.GetComponent<NodeBase>().bUnlocked))
        {
			m_SpriteRenderer.color = Color.red;
        }	
	}
}

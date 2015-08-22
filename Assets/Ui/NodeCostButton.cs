using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NodeCostButton : MonoBehaviour 
{
    public Text Greatness;
	public Text R;
	public Text G;
	public Text B;
	public Text T;

	private Transform node;

	void Start () {
	
	}

	void Update () {
	
	}

	public void Init(NodePath _path, Transform _node)
	{
        Greatness.text = _node.GetComponent<NodeBase>().GetGreatness().ToString();
        R.text = _path.Red.ToString();
        G.text = _path.Green.ToString();
        B.text = _path.Blue.ToString();
        T.text = _path.TotSparks.ToString();

		node = _node;
		
		GetComponent<Button>().onClick.AddListener(delegate() 
		                                           {
			CameraController.Instance.FocusNode(node);
            WorldScript.Instance.HighlightPath = _path;
		});
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NodeCostButton : MonoBehaviour {

	public Text R;
	public Text G;
	public Text B;
	public Text T;

	private Transform node;

	void Start () {
	
	}

	void Update () {
	
	}

	public void Init(int _R, int _G, int _B, int _T, Transform _node)
	{
		R.text = _R.ToString();
		G.text = _G.ToString();
		B.text = _B.ToString();
		T.text = _T.ToString();

		node = _node;
		
		GetComponent<Button>().onClick.AddListener(delegate() 
		                                           {
			CameraController.Instance.FocusNode(node);
		});
	}
}

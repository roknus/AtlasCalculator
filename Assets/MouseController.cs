using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseController : MonoBehaviour {

	public RectTransform 	RightClickOption;
	public Button			FindShortestPathButton;
	public Button			FindCheapestPathButton;

	void Start () {
	
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonUp (1)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 1 << 8))
		   	{
				RightClickOption.position = Input.mousePosition;
				RightClickOption.gameObject.SetActive(true);
				FindShortestPathButton.onClick.RemoveAllListeners();
				FindShortestPathButton.onClick.AddListener(delegate() 
				                                           {
           		hit.transform.GetComponent<NodeBase>().FindShortestPath();
				RightClickOption.gameObject.SetActive(false);
				});
				FindCheapestPathButton.onClick.RemoveAllListeners();
				FindCheapestPathButton.onClick.AddListener(delegate() 
				                                           {
					hit.transform.GetComponent<NodeBase>().FindAndHighlightCheapestPath();
					RightClickOption.gameObject.SetActive(false);
				});
			}
		}
	}
}

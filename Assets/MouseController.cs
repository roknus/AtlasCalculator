using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseController : MonoBehaviour {

	//public RectTransform 	RightClickOption;
	//public Button			FindShortestPathButton;
	//public Button			FindCheapestPathButton;

	void Start () {
	
	}
	
	void Update () 
	{
        // Do not hover UI
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp (1))
        {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 1 << 8))
		   	{
                UiManager.Instance.ShowRightClickPanel(Input.mousePosition, hit.transform.GetComponent<NodeBase>());
			}
		}
	}
}

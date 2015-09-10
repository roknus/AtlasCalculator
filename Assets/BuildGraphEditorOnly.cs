using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode] 
public class BuildGraphEditorOnly : MonoBehaviour {
	
	public static Transform currentlySelected;

	public static BuildGraphEditorOnly Instance { get; private set; }

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
	}

	void Start () 
	{
		currentlySelected = null;
	}

	void OnGUI()
	{
     /*
		if(Application.isPlaying)
			return;

		if (Event.current.type == EventType.Layout || Event.current.type == EventType.Repaint)
		{
			EditorUtility.SetDirty(this); // this is important, if omitted, "Mouse down" will not be display
		}
		if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
		{
			//Debug.Log("Left-Mouse Down");
		}
		if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
		{			
			RaycastHit hit;
			Vector3 mousePose = new Vector3(Event.current.mousePosition.x, Screen.height - Event.current.mousePosition.y, 0);
			Ray ray = Camera.main.ScreenPointToRay (mousePose);
			if (Physics.Raycast (ray, out hit, 1 << 8)) {			
				if (currentlySelected == null)
					currentlySelected = hit.transform;
				else {
                    NodeBase nodeScript = hit.transform.GetComponent<NodeBase>();
					nodeScript.m_neighbors.Add (currentlySelected);
					EditorUtility.SetDirty(nodeScript);
					currentlySelected = null;
				}
			}
		}
		if (Event.current.type == EventType.MouseUp && Event.current.button == 1)
		{
			//Debug.Log("Right-Click Up");
		}    
        */
	}
}

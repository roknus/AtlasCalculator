using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NavigationScript : MonoBehaviour 
{
    public InputField username;
    public InputField password;

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Selectable next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			
			if (next!= null) {
				
				InputField inputfield = next.GetComponent<InputField>();
				if (inputfield !=null) inputfield.OnPointerClick(new PointerEventData(EventSystem.current));  //if it's an input field, also set the text caret
				
				EventSystem.current.SetSelectedGameObject(next.gameObject, new BaseEventData(EventSystem.current));
			}		
		}
        if (Input.GetKeyUp(KeyCode.Return) && (EventSystem.current.currentSelectedGameObject == username.gameObject || EventSystem.current.currentSelectedGameObject == password.gameObject))
        {
			User.Instance.Connect();
		}
	}
}

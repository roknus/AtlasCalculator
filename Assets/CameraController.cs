using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float    XSpeed;
	public float    YSpeed;

    public int      zoomSpeed;
    private float   m_ZDistance;
    public float    ZDistance
    {
        get { return m_ZDistance; }
        set { m_ZDistance = Mathf.Clamp(value, 10, 60); }
    }
    private bool    bSmoothZoomCoroutine;

	public static CameraController Instance { get; private set; }

	void Awake () 
	{		
		if (Instance != null) {
			Destroy (gameObject);
			return;
		}
		
		Instance = this;

        m_ZDistance = 30;
        bSmoothZoomCoroutine = false;
	}

	void Update () 
	{
		if (Input.GetMouseButton (0)) 
		{
			float h = XSpeed * -Input.GetAxis("Mouse X");
			float v = YSpeed * -Input.GetAxis("Mouse Y");

            // ZDistance factor make the camera feels the same speed whatever the distance
            transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * XSpeed * ZDistance, Space.World);			
		}
        float w = Input.GetAxis("Mouse ScrollWheel");
        if(w != 0)
        {
            ZDistance += -w * 100;
            StartCoroutine("SmoothZoom");
        }
	}

    IEnumerator SmoothZoom()
    {
        if (!bSmoothZoomCoroutine)
        {
            bSmoothZoomCoroutine = true;
            while (Mathf.Abs(transform.position.y - ZDistance) > 1.0f)
            {
                if (transform.position.y < ZDistance)
                {
                    transform.Translate(new Vector3(0, 0, -Mathf.Abs(transform.position.y - ZDistance)) * zoomSpeed * Time.deltaTime, Space.Self);
                }
                else
                {
                    transform.Translate(new Vector3(0, 0, Mathf.Abs(transform.position.y - ZDistance)) * zoomSpeed * Time.deltaTime, Space.Self);
                }
                yield return null;
            }
            bSmoothZoomCoroutine = false;
        }
    }

    public void SwitchFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
		
	public void FocusNode(Transform node)
	{
		transform.position = new Vector3 (node.position.x, 20, node.position.z - 12);
	}
}

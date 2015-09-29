using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    private const int MIN_CAMERA_DIST   = 10;
    private const int MAX_CAMERA_DIST   = 70;

    private const int MIN_X_CAMERA = -300;
    private const int MAX_X_CAMERA = 400;

    private const int MIN_Z_CAMERA = -190;
    private const int MAX_Z_CAMERA = 130;

	public float    XSpeed;
	public float    YSpeed;

    public float      zoomSpeed;
    private float   m_ZDistance;
    public float    ZDistance
    {
        get { return m_ZDistance; }
        set { m_ZDistance = Mathf.Clamp(value, MIN_CAMERA_DIST, MAX_CAMERA_DIST); }
    }
    private bool bSmoothZoomCoroutine;
    private bool bSmoothFocusCoroutine;

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
        // Left click or middle click but not hover UI
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && (Input.GetMouseButton (0) || Input.GetMouseButton(2))) 
		{
			float h = XSpeed * -Input.GetAxis("Mouse X");
			float v = YSpeed * -Input.GetAxis("Mouse Y");

            // ZDistance factor make the camera feels the same speed whatever the distance
            //transform.Translate(new Vector3(h, 0, v) * XSpeed * ZDistance, Space.World);
            transform.position = transform.position + (new Vector3(h, 0, v) * XSpeed * ZDistance);
            // Limit movement on X and Z axis
            ClampCameraPos();
		}
        float w = Input.GetAxis("Mouse ScrollWheel");
        if(w != 0)
        {
            ZDistance += -w * 100;
            if (!bSmoothZoomCoroutine)
            {
                StartCoroutine("SmoothZoom");
            }
        }
	}

    private void ClampCameraPos()
    {
        Vector3 pos = transform.position;
        if (pos.x < MIN_X_CAMERA) pos.x = MIN_X_CAMERA;
        else if (pos.x > MAX_X_CAMERA) pos.x = MAX_X_CAMERA;

        if (pos.z < MIN_Z_CAMERA) pos.z = MIN_Z_CAMERA;
        else if (pos.z > MAX_Z_CAMERA) pos.z = MAX_Z_CAMERA;

        transform.position = pos;
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
                    transform.Translate(new Vector3(0, 0, -Mathf.Abs(transform.position.y - ZDistance)) * zoomSpeed, Space.Self);
                }
                else
                {
                    transform.Translate(new Vector3(0, 0, Mathf.Abs(transform.position.y - ZDistance)) * zoomSpeed, Space.Self);
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

    IEnumerator SmoothFocus(Vector3 _dest)
    {
        if(!bSmoothFocusCoroutine)
        {
            bSmoothFocusCoroutine = true;
            while (Vector3.Distance(transform.position, _dest) > 5.0f)
            {
                Vector3 dir = (_dest - transform.position);
                dir.Normalize();
                transform.Translate(dir * 0.7f);

                yield return null;
            }
            bSmoothFocusCoroutine = false;
        }
    }
		
	public void FocusNode(Transform node)
	{
        //Vector3 temp = transform.position;
        transform.position = new Vector3(node.position.x, 0, node.position.z);
        transform.Translate(new Vector3(0, 0, -ZDistance), Space.Self);
        /*Vector3 dest = transform.position;
        transform.position = temp;
        StartCoroutine(SmoothFocus(dest));*/
	}
}

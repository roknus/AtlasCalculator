using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    private const int MIN_CAMERA_DIST   = 10;
    private const int MAX_CAMERA_DIST   = 70;

    private const int MIN_X_CAMERA = -300;
    private const int MAX_X_CAMERA = 350;

    private const int MIN_Z_CAMERA = -150;
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
        // Left click or middle click
		if (Input.GetMouseButton (0) || Input.GetMouseButton(2)) 
		{
			float h = XSpeed * -Input.GetAxis("Mouse X");
			float v = YSpeed * -Input.GetAxis("Mouse Y");

            // ZDistance factor make the camera feels the same speed whatever the distance
            transform.Translate(new Vector3(h, 0, v) * XSpeed * ZDistance, Space.World);
            // Limit movement on X and Z axis
            ClampCameraPos();
		}
        float w = Input.GetAxis("Mouse ScrollWheel");
        if(w != 0)
        {
            ZDistance += -w * 100;
            StartCoroutine("SmoothZoom");
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
		
	public void FocusNode(Transform node)
	{
		transform.position = new Vector3 (node.position.x, 20, node.position.z - 12);
	}
}

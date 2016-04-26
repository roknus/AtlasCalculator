using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public delegate void CameraBehavior();

public class CameraController : MonoBehaviour
{
    public GameObject TopCamera;
    public GameObject IsoCamera;

    private CameraBehavior CameraUpdate;

    private const float MIN_CAMERA_DIST   = 0.0f;
    private const float MAX_CAMERA_DIST   = 6.0f;

	private const float MIN_X_CAMERA = -300;
	private const float MAX_X_CAMERA = 400;

	private const float MIN_Z_CAMERA = -190;
	private const float MAX_Z_CAMERA = 130;

	public float    XSpeed;
	public float    YSpeed;
    //public float    m_MoveSpeed;
    public float 	MoveSpeed
    {
		get { return m_ZDistance * 0.4f + 0.5f; }
		//set { m_MoveSpeed = Mathf.Clamp(value, 0.5f, 2.9f); }
    }

    public float    zoomSpeed;
    public float   	m_ZDistance;
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

        ZDistance = 3.0f;
        //MoveSpeed = 1.7f;
        bSmoothZoomCoroutine = false;
        CameraUpdate = IsoCameraUpdate;
		transform.position = new Vector3(15, 0, -10);
	}

    void Start()
    {
        // Ugly but... set the camera to top view (useful when switching scene and want to keep same view)
        if (User.Instance.isTopView) SetTopView();
    }

	void Update () { CameraUpdate(); }

    private void IsoCameraUpdate()
    {
        // Left click or middle click but not hover UI
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && (Input.GetMouseButton (0) || Input.GetMouseButton(2))) 
		{
			float h = XSpeed * -Input.GetAxis("Mouse X");
			float v = YSpeed * -Input.GetAxis("Mouse Y");

            // ZDistance factor make the camera feels the same speed whatever the distance
            //transform.Translate(new Vector3(h, 0, v) * XSpeed * ZDistance, Space.World);
            transform.position = transform.position + (new Vector3(h, 0, v) * MoveSpeed);
            // Limit movement on X and Z axis
            ClampCameraPos();
		}
        float w = Input.GetAxis("Mouse ScrollWheel");
        if(w != 0)
        {
            float delta = ZDistance;
            ZDistance += -w;
            //MoveSpeed += -w * 0.4f;
            delta -= ZDistance;

            TopCamera.GetComponent<Camera>().orthographicSize -= delta * 4.0f;

            Vector3 translation = new Vector3(0, 0, delta);
			IsoCamera.transform.Translate(translation * zoomSpeed, Space.Self);

            /* Removed for v0.60
            if (!bSmoothZoomCoroutine)
            {
                StartCoroutine("SmoothZoom");
            }
             * */
        }
	}

    private void TopCameraUpdate()
    {
        // Left click or middle click but do not hover UI
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && (Input.GetMouseButton(0) || Input.GetMouseButton(2)))
        {
            float h = XSpeed * -Input.GetAxis("Mouse X");
            float v = YSpeed * -Input.GetAxis("Mouse Y");

            // ZDistance factor make the camera feels the same speed whatever the distance
            //transform.Translate(new Vector3(h, 0, v) * XSpeed * ZDistance, Space.World);
            transform.position = transform.position + (new Vector3(h, 0, v) * MoveSpeed);
            // Limit movement on X and Z axis
            ClampCameraPos();
        }
        float w = Input.GetAxis("Mouse ScrollWheel");
        if (w != 0)
        {
            float delta = ZDistance;
            ZDistance += -w;
            //MoveSpeed += -w * 0.4f;
            delta -= ZDistance;

			TopCamera.GetComponent<Camera>().orthographicSize -= delta * 4.0f;
			
			Vector3 translation = new Vector3(0, 0, delta);
			IsoCamera.transform.Translate(translation * zoomSpeed, Space.Self);
            
                /*
            Vector3 translation = new Vector3(0, 0, delta);
            transform.Translate(translation * zoomSpeed, Space.Self);

            /* Removed for v0.60
            if (!bSmoothZoomCoroutine)
            {
                StartCoroutine("SmoothZoom");
            }
             * */
        }
    }

    public void SetTopView()
    {
        TopCamera.SetActive(true);
        IsoCamera.SetActive(false);
        UiManager.Instance.TopViewButton.interactable = false;
        UiManager.Instance.IsoViewButton.interactable = true;
        CameraUpdate = TopCameraUpdate;
    }

    public void SetIsoView()
    {
        TopCamera.SetActive(false);
        IsoCamera.SetActive(true);
        UiManager.Instance.TopViewButton.interactable = true;
        UiManager.Instance.IsoViewButton.interactable = false;
        CameraUpdate = IsoCameraUpdate;
    }

    public void SwitchCamera()
    {
        if (User.Instance.isTopView)
        {
            SetIsoView();
        }
        else
        {
            SetTopView();
        }
        User.Instance.isTopView = !User.Instance.isTopView;
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
        transform.position = node.position;
        //transform.Translate(new Vector3(0, 0, -ZDistance), Space.Self);
        /*Vector3 dest = transform.position;
        transform.position = temp;
        StartCoroutine(SmoothFocus(dest));*/
	}
}

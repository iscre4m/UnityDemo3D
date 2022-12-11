using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Sphere;
    [SerializeField]
    private GameObject HeadLight;

    private Vector3 sphereCam;
    private float zoom;

    private const float MIN_ZOOM = 0;
    private const float MAX_ZOOM = 2;
    private const float ZOOM_SENS = 10;
    private const float MAX_VERTICAL = 75;
    private const float MIN_VERTICAL = 25;
    private const float VERTICAL_SENS = 2;
    private const float HORIZONTAL_SENS = 4;
    private const short HEAD_LIGHT_VERTICAL_ANGLE = 40;

    private float camAngleVertical;
    private float camAngleHorizontal;

    void Start()
    {
        sphereCam = transform.position - Sphere.transform.position;
        zoom = 1;
        camAngleVertical = transform.eulerAngles.x;
        camAngleHorizontal = transform.eulerAngles.y;
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            zoom -= Input.mouseScrollDelta.y / ZOOM_SENS * Time.timeScale;
            if (zoom < MIN_ZOOM)
            {
                zoom = MIN_ZOOM;
            }
            if (zoom > MAX_ZOOM)
            {
                zoom = MAX_ZOOM;
            }
        }

        camAngleHorizontal += Input.GetAxis("Mouse X") *
        HORIZONTAL_SENS * Time.timeScale;
        camAngleVertical -= Input.GetAxis("Mouse Y") *
        VERTICAL_SENS * Time.timeScale;

        if (camAngleVertical < MIN_VERTICAL)
        {
            camAngleVertical = MIN_VERTICAL;
        }
        if (camAngleVertical > MAX_VERTICAL)
        {
            camAngleVertical = MAX_VERTICAL;
        }
    }

    void LateUpdate()
    {
        transform.position = Sphere.transform.position + Quaternion.Euler(0, camAngleHorizontal, 0) * sphereCam * zoom;

        transform.eulerAngles = new Vector3(
            camAngleVertical,
            camAngleHorizontal,
            0
        );

        HeadLight.transform.eulerAngles = new Vector3(
            HEAD_LIGHT_VERTICAL_ANGLE,
            camAngleHorizontal,
            0
        );
    }
}

using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 jump = Vector3.up * 200;
    private Vector3 forceDirection;

    private const float FORCE_AMPL = 2;

    [SerializeField]
    private GameObject cam;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(jump);
        }
        float fx = Input.GetAxis("Horizontal");
        float fy = Input.GetAxis("Vertical");
        rigidBody.AddForce(new Vector3(fx, 0, fy));

        forceDirection = cam.transform.forward;
        forceDirection.y = 0;
        forceDirection = forceDirection.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}

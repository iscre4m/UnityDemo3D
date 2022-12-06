using UnityEngine;

public class HeadLight : MonoBehaviour
{
    [SerializeField]
    private GameObject Sphere;

    private Vector3 shift;

    void Start()
    {
        shift = transform.position - Sphere.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Sphere.transform.position + shift;
    }
}
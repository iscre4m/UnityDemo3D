using UnityEngine;

public class SphereChaser : MonoBehaviour
{
    [SerializeField]
    private GameObject Sphere;

    private Vector3 shift;

    void Start()
    {
        shift = transform.position - Sphere.transform.position;
    }

    void Update()
    {
        transform.position = Sphere.transform.position + shift;
    }
}

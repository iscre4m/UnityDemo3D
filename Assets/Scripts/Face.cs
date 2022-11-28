using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    private Vector3 shift;

    void Start()
    {
        shift = transform.position - sphere.transform.position;
    }

    void Update()
    {
        transform.position = sphere.transform.position + shift;
    }
}

using UnityEngine;

public class FirstGate : MonoBehaviour
{
    private const float TIMEOUT = 15;
    private float timeout;

    void Start()
    {
        timeout = TIMEOUT;
    }

    void Update()
    {
        timeout -= Time.deltaTime;

        if (timeout < 0)
        {
            gameObject.SetActive(false);

            return;
        }

        transform.position = new Vector3(
            transform.position.x,
            transform.localScale.y * (-1/2f + timeout / TIMEOUT),
            transform.position.z);
    }
}

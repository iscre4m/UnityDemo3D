using UnityEngine;

public class FirstGate : MonoBehaviour
{
    private const float TIMEOUT = 20;
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

        transform.localScale = new Vector3(timeout / TIMEOUT, timeout / TIMEOUT, transform.localScale.z);
    }
}

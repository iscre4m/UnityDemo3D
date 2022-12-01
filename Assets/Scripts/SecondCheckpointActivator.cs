using UnityEngine;

public class SecondCheckpointActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SecondCheckpoint.IsActivated = true;
    }
}
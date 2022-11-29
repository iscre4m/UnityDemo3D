using UnityEngine;

public class FinalCheckpointActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FinalCheckpoint.IsActivated = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCheckpointActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SecondCheckpoint.IsActivated = true;
    }
}

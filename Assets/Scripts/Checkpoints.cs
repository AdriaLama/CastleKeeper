using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Vector2 respawnPosition;
    public int checkpointID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(respawnPosition, checkpointID);

            if (checkpointID >= 2)
            {
                PickItems pickItems = other.GetComponent<PickItems>();
                if (pickItems != null)
                {
                    if (pickItems.hasHook)
                        CheckpointManager.Instance.AddObjeto("Hook");

                    if (pickItems.hasGrab)
                        CheckpointManager.Instance.AddObjeto("Grab");
                }
            }
        }

        
    }
}

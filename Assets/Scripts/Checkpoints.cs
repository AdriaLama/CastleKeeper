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
        }

        
    }
}

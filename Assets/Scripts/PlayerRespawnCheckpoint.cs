using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnCheckpoint : MonoBehaviour
{
    private void Start()
    {
        if (CheckpointManager.Instance != null)
        {
            Vector2 pos = CheckpointManager.Instance.getLastCheckPointPosition();

            if (pos != Vector2.zero)
            {
                transform.position = pos;
            }
        }
    }

}

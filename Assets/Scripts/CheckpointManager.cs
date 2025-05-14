using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public Vector2 lastCheckpointPosition;
    public int lastCheckpointID = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector2 position, int id)
    {
        lastCheckpointPosition = position;
        lastCheckpointID = id;
        
    }

    public Vector2 getLastCheckPointPosition()
    {
        return lastCheckpointPosition;
    }

    public void ResetData()
    {
        lastCheckpointID = 0;
        lastCheckpointPosition = Vector2.zero;
    }

}

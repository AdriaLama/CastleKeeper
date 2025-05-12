using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchBoss : MonoBehaviour
{
    private bool isCamera;
    public GameObject mainCamera;
    private Camera cam;
    void Start()
    {
        cam = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCamera)
        {
            cam.transform.position = new Vector3(12f, 3f, -10);
            cam.orthographicSize = 10.75f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCamera = true;
            mainCamera.transform.parent = null;
        }
    }
}

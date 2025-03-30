using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGargoyle : MonoBehaviour
{
    public float velocidad;

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        Destroy(gameObject, 4f);
    }
}

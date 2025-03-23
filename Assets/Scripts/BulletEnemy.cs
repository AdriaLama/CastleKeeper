using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float velocidad;

    void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * Vector2.right);
        Destroy(gameObject, 4f);
    }


}

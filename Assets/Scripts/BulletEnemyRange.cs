using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyRange : MonoBehaviour
{
    public float velocidad;
    private Vector2 direccion;

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
        Destroy(gameObject, 4f);
    }

    public void SetDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion.normalized;
        float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaRoja : MonoBehaviour
{
    private Vidas vd;
    public Image Sangre;
    private float r;
    private float g;
    private float b;
    private float a;
    void Start()
    {
        vd = FindObjectOfType<Vidas>();
        r = Sangre.color.r;
        g = Sangre.color.g;
        b = Sangre.color.b;
        a = Sangre.color.a;
    }

    void Update()
    {
        a -= 0.15f * Time.deltaTime;
        a = Mathf.Clamp(a, 0f, 0.15f);
        ChangeColor();

        if (vd.playerHit)
        {
            a += 0.15f;
            a = Mathf.Clamp(a, 0f, 0.15f);
            ChangeColor();
            vd.playerHit = false;
          
        }
    }

    private void ChangeColor()
    {
        Color c = new Color(r, g, b, a);
        Sangre.color = c;
    }
}


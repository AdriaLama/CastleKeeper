using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaRoja : MonoBehaviour
{
    private Vidas vd;
    private MovimientoPJ pj;
    public Image Sangre;
    private float r;
    private float g;
    private float b;
    private float a;

    void Start()
    {
        
        r = Sangre.color.r;
        g = Sangre.color.g;
        b = Sangre.color.b;
        a = Sangre.color.a;

        pj = FindObjectOfType<MovimientoPJ>();
    }

    void Update()
    {
        a -= 0.15f * Time.deltaTime;
        a = Mathf.Clamp(a, 0f, 0.15f);
        ChangeColor();

        if (pj.isKnock)
        {
            a += 0.15f;
            a = Mathf.Clamp(a, 0f, 0.15f);
            ChangeColor();
           
        }
    }

    private void ChangeColor()
    {
        Color c = new Color(r, g, b, a);
        Sangre.color = c;
    }
}


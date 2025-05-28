using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransicionNegra : MonoBehaviour
{
    private tpPlayer tp;
    private MovimientoPJ bs;
    public Image imagenNegra;
    private float r;
    private float g;
    private float b;
    private float a;

    void Start()
    {

        r = imagenNegra.color.r;
        g = imagenNegra.color.g;
        b = imagenNegra.color.b;
        a = imagenNegra.color.a;

        tp = FindObjectOfType<tpPlayer>();
        bs = FindObjectOfType<MovimientoPJ>();
    }

    void Update()
    {
        a -= 1f * Time.deltaTime;
        a = Mathf.Clamp(a, 0f, 1f);
        ChangeColor();

        if (tp.isTp1 || tp.isTp2 || bs.isBossScene)
        {
            a += 1f;
            a = Mathf.Clamp(a, 0f, 1f);
            ChangeColor();

            if (tp.isTp1)
            {
                StartCoroutine(falseIsTrue1());
            }
            if (tp.isTp2)
            {
                StartCoroutine(falseIsTrue2());
            }
            if (bs.isBossScene)
            {
                StartCoroutine(falseIsBossScene());
            }
        }
    }

    private void ChangeColor()
    {
        Color c = new Color(r, g, b, a);
        imagenNegra.color = c;
    }

    private IEnumerator falseIsTrue1()
    {
        yield return new WaitForSeconds(1.5f);
        tp.isTp1 = false;
    }
    private IEnumerator falseIsTrue2()
    {
        yield return new WaitForSeconds(1.5f);
        tp.isTp2 = false;
    }
    private IEnumerator falseIsBossScene()
    {
        yield return new WaitForSeconds(1.5f);
        bs.isBossScene = false;
    }
}


using UnityEngine;

public class TransicionNegra : MonoBehaviour
{
    private tpPlayer tp;
    public Animator transicionAnimacion;
    public Animator transicionAnimacion2;

    void Start()
    {
        tp = FindObjectOfType<tpPlayer>();
    }

    void Update()
    {
        if (tp.isTp1)
        {
            transicionAnimacion.SetTrigger("Begin");
        }

        if (tp.isTp2)
        {
            
            transicionAnimacion2.SetTrigger("Begin");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidReciever : MonoBehaviour
{
    public static LiquidReciever main;
    public LiquidContainer Container;
    public bool isMain = false;
    LiquidWobble myWobbler;
    private void Awake()
    {
        if (isMain)
            main = this;
        Container = transform.parent.GetComponentInChildren<LiquidContainer>();
        myWobbler = transform.parent.GetComponentInChildren<LiquidWobble>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<LiquidPourer>(out LiquidPourer pourer))
        {
            ReadjustHeight();
            myWobbler.MakeRipple(.03f);
        }
    }
    public void ReadjustHeight()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Container.Fill * 2 - 1, transform.localPosition.z);
    }
}

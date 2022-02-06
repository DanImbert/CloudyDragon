using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidReciever : MonoBehaviour
{
    public static LiquidReciever main;
    public LIquidContainer myContainer;
    LiquidWobble myWobbler;
    private void Awake()
    {
        main = this;
        myContainer = transform.parent.GetComponentInChildren<LIquidContainer>();
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
        transform.localPosition = new Vector3(transform.localPosition.x, myContainer.Fill * 2 - 1, transform.localPosition.z);
    }
}

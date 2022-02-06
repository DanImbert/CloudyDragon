using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidReciever : MonoBehaviour
{
    public static LiquidReciever main;
    public LIquidContainer Container;
    LiquidWobble myWobbler;
    private void Awake()
    {
        main = this;
        Container = transform.parent.GetComponentInChildren<LIquidContainer>();
        myWobbler = transform.parent.GetComponentInChildren<LiquidWobble>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<LiquidPourer>(out LiquidPourer pourer))
        {
            ReadjustHeight();
            
        }
    }
    public void ReadjustHeight()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Container.Fill * 2 - 1, transform.localPosition.z);
    }
}

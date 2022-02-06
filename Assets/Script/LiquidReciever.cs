using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidReciever : MonoBehaviour
{
    public static LiquidReciever main;
    LIquidContainer myContainer;
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
            pourer.TransferLiquid(myContainer);
            ReadjustHeight();
            myWobbler.MakeRipple(.03f);
            if (myContainer.GetFillPercent() >= 1)
                GameController.main.EndTheGame(myContainer);
        }
    }
    public void ReadjustHeight()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, myContainer.Fill * 2 - 1, transform.localPosition.z);
    }
}

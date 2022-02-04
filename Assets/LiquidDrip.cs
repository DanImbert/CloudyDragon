using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidDrip : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Disable", 1);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}

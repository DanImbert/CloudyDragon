using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerController : MonoBehaviour
{
    public bool ToggleTransparent = true;
    public GameObject Lid;
    public GameObject OpaqueBody;
    public GameObject TransparentBody;
   
   

    [HideInInspector] public LiquidReciever reciever;

    private void Awake()
    {
        SetTransparent(false);
        reciever = GetComponentInChildren<LiquidReciever>();
    }
    public void SetTransparent(bool value)
    {
        Lid.SetActive(!value);
        if (ToggleTransparent)
        {
            OpaqueBody.SetActive(!value);
            TransparentBody.SetActive(value);
        }
        
    }
}

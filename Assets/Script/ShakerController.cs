using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerController : MonoBehaviour
{
    public GameObject Lid;
    public GameObject OpaqueBody;
    public GameObject TransparentBody;

    private void Awake()
    {
        SetTransparent(false);
    }
    public void SetTransparent(bool value)
    {
        OpaqueBody.SetActive(!value);
        Lid.SetActive(!value);
        TransparentBody.SetActive(value);
    }
}

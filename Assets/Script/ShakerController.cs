using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerController : MonoBehaviour
{
    public GameObject Lid;
    public GameObject OpaqueBody;
    public GameObject TransparentBody;
    public AudioSource shakeSound;

    public LiquidReciever reciever;

    private void Awake()
    {
        SetTransparent(false);
        reciever = GetComponentInChildren<LiquidReciever>();
    }
    public void SetTransparent(bool value)
    {
        Lid.SetActive(!value);
        OpaqueBody.SetActive(!value);
        TransparentBody.SetActive(value);
        shakeSound.Play();
    }
}

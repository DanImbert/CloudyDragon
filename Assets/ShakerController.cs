using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerController : MonoBehaviour
{
    Animator animator;
    public GameObject Lid;
    public GameObject OpaqueBody;
    public GameObject TransparentBody;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetTransparent(bool t)
    {
        OpaqueBody.SetActive(!t);
        Lid.SetActive(!t);
        TransparentBody.SetActive(t);
    }
}

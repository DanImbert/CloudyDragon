using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBottle : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    public float BottleHeight = 0;
    public LiquidSO mainLiquid;
    public GameObject cork;
    public AudioSource pingSelect;
   
    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }
    private void Start()
    {
    LiquidHolder lc = GetComponentInChildren<LiquidHolder>();
        if (mainLiquid != null)
        {
            lc.AddLiquid(mainLiquid, lc.Volume * lc.Fill);
           
        }
        lc.OnVolumeChange(true);
        SetDisplayMode();
    }
    public void MoveToPosition(Vector3 pos)
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, pos, .25f);
        pingSelect.Play();
    }
    public void ResetPosition()
    {
        if (!LeanTween.isTweening(gameObject))
        {
            LeanTween.move(gameObject, startPos, 1);
            LeanTween.rotate(gameObject, startRot.eulerAngles, 1);
           
           
}
    }
    public void SetDisplayMode()
    {
        GetComponentInChildren<LiquidPourer>().enabled = false;
        if (cork != null)
            cork.SetActive(true);
        
    }
    public void SetPourMode()
    {
        GetComponentInChildren<LiquidPourer>().enabled = true;
        if (cork != null)
            cork.SetActive(false);
        
    }
}

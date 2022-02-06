using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBottle : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    public float BottleHeight = 0;
    public LiquidSO mainLiquid;
    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }
    private void Start()
    {
    LIquidContainer lc = GetComponentInChildren<LIquidContainer>();
        if (mainLiquid != null)
        {
            lc.AddLiquid(mainLiquid, lc.Volume * lc.Fill);
        }
        lc.OnVolumeChange(true);
    }
    public void MoveToPosition(Vector3 pos)
    {
        LeanTween.move(gameObject, pos, .25f);
    }
    public void ResetPosition()
    {
        if (!LeanTween.isTweening(gameObject))
        {
            LeanTween.move(gameObject, startPos, .5f);
            LeanTween.rotate(gameObject, startRot.eulerAngles, .5f);
        }
    }
}

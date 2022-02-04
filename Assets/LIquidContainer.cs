using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    Material mat;
    MeshRenderer mesh;
    [Range(0,1)] public float Fill = .5f;
    public Vector2 Volume;
    public LiquidSO myLiquid;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = Material.Instantiate(mesh.material);
        mesh.material = mat;
        Volume = new Vector2(transform.lossyScale.y*.33f, transform.lossyScale.x)+Vector2.one*.01f;
    }
    private void Start()
    {
        if (myLiquid!=null)
            ChangeLiquid(myLiquid);
    }
   public void ChangeLiquid(LiquidSO nLiquid)
    {
        Debug.Log("Change liquid " + nLiquid.name); 
        myLiquid = nLiquid;
        mesh.material.SetColor("_Tint", myLiquid.BodyColor);
        mesh.material.SetColor("_RimColor", myLiquid.RimColor);
        mesh.material.SetFloat("_RimPower", myLiquid.RimStrength);

        mesh.material.SetColor("_TopColor", myLiquid.RimColor);
        mesh.material.SetColor("_FoamColor", myLiquid.FoamColor);
        mesh.material.SetFloat("_Rim", 0);
    }
    private void Update()
    {
        if (Fill > 0)
        {
            mesh.enabled = true;
            float fill = Mathf.Abs(transform.up.y);
            fill = Mathf.Abs(fill * Volume.y + (1 - fill) * Volume.x);
            fill *= (-.5f + (1 - Fill) * 2);
            mesh.material.SetFloat("_FillAmount", fill);
        }
        else
        {
            mesh.enabled = false;
        }
    }
}

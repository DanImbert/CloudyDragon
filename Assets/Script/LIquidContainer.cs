using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIquidContainer : MonoBehaviour
{
    Material mat;
    MeshRenderer mesh;
    [Range(0,1)] public float Fill = .5f;
    public Vector2 Dimensions;
    public float Volume = 0;
    public LiquidSO myLiquid;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = Material.Instantiate(mesh.material);
        mesh.material = mat;
        Dimensions = new Vector2(transform.lossyScale.y, transform.lossyScale.x)+Vector2.one*.01f;
        Volume =  Dimensions.x * Dimensions.x * Mathf.PI * Dimensions.y;
        Dimensions.x*=0.33f;
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
        UpdateMaterial();
    }
    private void UpdateMaterial()
    {
        if (Fill > 0)
        {
            mesh.enabled = true;
            float fill = Mathf.Abs(transform.up.y);
            fill = Mathf.Abs(fill * Dimensions.y + (1 - fill) * Dimensions.x);
            fill *= (-.5f + (1 - Fill) * 2);
            mesh.material.SetFloat("_FillAmount", fill);
        }
        else
        {
            mesh.enabled = false;
        }
    }
    public float SubstractVolume(float svolume)
    {
        svolume *= 1/Volume;
        if (Fill<svolume)
        {
            svolume = Fill;
        }
        Fill -= svolume;
        UpdateMaterial();
        return svolume;
    }
    public void AddLiquid()
    {

    }
    public void TransferLiquid()
    {

    }
}

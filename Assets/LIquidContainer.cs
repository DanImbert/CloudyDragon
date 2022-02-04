using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIquidContainer : MonoBehaviour
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
        float fill = Volume.y - (Fill-.5f)*2;
        mesh.material.SetFloat("_FillAmount", fill);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIquidContainer : MonoBehaviour
{
    Material mat;
    MeshRenderer mesh;
    [Range(0, 1)] public float Fill = .5f;
    [Range(0, 1)] public float FillPercent = 1;
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
        InitContents();
    }
    private void Update()
    {
        UpdateFoam();
    }
    void ChangeColor()
    {

        Color BodyColor = Color.black;
        Color RimColor = Color.black;
        Color FoamColor = Color.black ;

        float RimStrength = 0;
        foamStrength = 0;

        foreach (KeyValuePair<LiquidSO, float> liquid in contents)
        {
            if (liquid.Value > 0)
            {
                float percent = liquid.Value / totalVolume;

                BodyColor.r += liquid.Key.BodyColor.r * percent;
                BodyColor.g += liquid.Key.BodyColor.g * percent;
                BodyColor.b += liquid.Key.BodyColor.b * percent;

                RimColor.r += liquid.Key.RimColor.r * percent;
                RimColor.g += liquid.Key.RimColor.g * percent;
                RimColor.b += liquid.Key.RimColor.b * percent;

                FoamColor.r += liquid.Key.FoamColor.r * percent;
                FoamColor.g += liquid.Key.FoamColor.g * percent;
                FoamColor.b += liquid.Key.FoamColor.b * percent;

                RimStrength += liquid.Key.RimStrength * percent;
                foamStrength += liquid.Key.Foaming * percent;

            }
        }


        mesh.material.SetColor("_Tint", BodyColor);
        mesh.material.SetColor("_RimColor", RimColor);
        mesh.material.SetFloat("_RimPower", RimStrength);

        mesh.material.SetColor("_TopColor", RimColor);
        mesh.material.SetColor("_FoamColor", FoamColor);
        UpdateMaterial();
    }
    public float totalVolume = 0;
    float GetTotalVolume()
    {
        float v = 0;
        foreach (KeyValuePair<LiquidSO, float> liquid in contents)
        {
            v += liquid.Value;
        }
        return v;
    }
    public float GetFillPercent()
    {
        return totalVolume / Volume;
    }
    private void UpdateMaterial()
    {
        Fill = GetFillPercent();
        if (Fill > 0)
        {
            mesh.enabled = true;
            float fill = Mathf.Abs(transform.up.y);
            fill = Mathf.Abs(fill * Dimensions.y + (1 - fill) * Dimensions.x);
            fill *= (-.5f + (1 - Fill * FillPercent) * 2);
            mesh.material.SetFloat("_FillAmount", fill);
        }
        else
        {
            mesh.enabled = false;
        }
    }

    #region contents
    Dictionary<LiquidSO, float> contents;
    public void InitContents()
    {
        contents = new Dictionary<LiquidSO, float>();
        if (myLiquid!=null)
        {
            AddLiquid(myLiquid, Volume * Fill) ;
        }
        OnVolumeChange(true);
    }
    public void OnVolumeChange(bool color)
    {
        totalVolume = GetTotalVolume();
        if (color)
         ChangeColor();
    }
    public bool SubstractVolume(float amount)
    {
        bool isEmpty = true;
        Debug.Log("Substract " + amount + "drink from " + name + " total volume " + totalVolume);
        float percent = amount / contents.Count;

        Dictionary<LiquidSO,float> changes = new Dictionary<LiquidSO, float>();
        foreach (KeyValuePair<LiquidSO, float> liquid in contents)
        {
            if (liquid.Value>0)
            {
                changes.Add(liquid.Key, liquid.Value * percent);
                isEmpty = false;
            }
        }
        foreach (KeyValuePair<LiquidSO, float> substract in changes)
        {
            SubstractLiquid(substract.Key, substract.Value);
        }
        OnVolumeChange(false);
            return isEmpty;
    }
    public void  SubstractLiquid(LiquidSO liquid, float amount)
    {
        //Debug.Log("Substract " + amount + "drink from " + name + " total volume " + GetTotalVolume());
        if (contents.ContainsKey(liquid) && contents[liquid] > 0)
        {
            contents[liquid] = Mathf.Max(0, contents[liquid] - amount);
        }
    }
    public void AddLiquid(LiquidSO liquid, float amount)
    {
        amount = Mathf.Min(amount, Volume - totalVolume);
        if (amount <= 0)
            return;
        if (contents.ContainsKey(liquid))
            contents[liquid] += amount;
        else
            contents.Add(liquid, amount);
        UpdateMaterial();
    }
    public void TransferLiquid(LIquidContainer cOther, float amount, bool substract)
    {
        float percent = Mathf.Min(1,amount / totalVolume);
        //Debug.Log("Transfer " + amount + " from " + name + " to " + cOther.name + " " + cOther.totalVolume);

        Dictionary<LiquidSO, float> changes = new Dictionary<LiquidSO, float>();
        foreach (KeyValuePair<LiquidSO, float> liquid in contents)
        {
            if (liquid.Value > 0)
            {
                cOther.AddLiquid(liquid.Key, liquid.Value * percent);
                if (substract)
                    changes.Add(liquid.Key, liquid.Value * percent);
            }
        }
        foreach (KeyValuePair<LiquidSO, float> substracted in changes)
        {
            SubstractLiquid(substracted.Key, substracted.Value);
        }
        OnVolumeChange(false);
        cOther.OnVolumeChange(true);
    }
    public float GetVolumeContent(LiquidSO liquid)
    {
        if (contents.ContainsKey(liquid))
            return contents[liquid];
        return 0;
    }
    public float GetVolumeContentPercentage(LiquidSO liquid)
    {
        return GetVolumeContent(liquid)/Volume;
    }

    #endregion
    #region Foam
    float foaming = 0;
    float foamStrength = 0f;
    public void IncreaseFoam()
    {

    }
    public void UpdateFoam()
    {

        mesh.material.SetFloat("_Rim", foaming);
    }

    #endregion
}

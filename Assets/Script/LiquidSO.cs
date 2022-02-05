using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drink", menuName = "CloudyDragons/Drink")]

public class LiquidSO : ScriptableObject
{
    [Header("Body")]
    public Color BodyColor;
    public Color RimColor;
    [Range(0,10)]
    public float RimStrength;

    [Header("Foam")]
    public Color FoamColor;
    public float Foaming;
    public float DeFoaming;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "CloudyDragons/Recipe")]
public class RecipeSO : ScriptableObject
{
    public CocktailMix[] Requirements;
    [Serializable]
    public class CocktailMix
    {
        public LiquidSO Spirit;
        public float Percentage;
    }
}

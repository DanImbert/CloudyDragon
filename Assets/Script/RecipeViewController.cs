using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeViewController : MonoBehaviour
{
    TextMeshProUGUI recipeText;
    void Awake()
    {
        recipeText = GetComponentInChildren<TextMeshProUGUI>();


    }

    void Start()
    {
        recipeText.text = GameController.main.winCondition.name+"\n";

        foreach (RecipeSO.CocktailMix req in GameController.main.winCondition.Requirements)
        {
            recipeText.text += "\n" + req.Spirit.name +": " + Mathf.Floor(req.Percentage*10)+" part";
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeViewController : MonoBehaviour
{
    TextMeshProUGUI recipeText;
    public AudioSource RecipeSound;
    void Awake()
    {
        recipeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnEnable()
    {
        //TODO Audio: Add Bringup Recipe Sound
        RecipeSound.Play();
    }

    void Start()
    {
        recipeText.text = GameController.main.winCondition.name+"\n";

        foreach (RecipeSO.CocktailMix req in GameController.main.winCondition.Requirements)
        {
            recipeText.text += "\n" + req.Spirit.name +": " + Mathf.Floor(req.Percentage*100)+"ml";
        }
    }
}

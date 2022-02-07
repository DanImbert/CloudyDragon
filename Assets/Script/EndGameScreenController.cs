using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScreenController : MonoBehaviour
{
    TextMeshProUGUI title;
    GameObject ScoreView;
    private void Awake()
    {
        title = transform.Find("Cocktail Name").GetComponent<TextMeshProUGUI>();
        ScoreView = transform.Find("Rating").gameObject;
    }
    public void AssessDrink(LiquidHolder cocktailFinal)
    {
        title.text = GameController.main.winCondition.name;

        float score = 5;
       /* foreach (RecipeSO.CocktailMix component in GameController.main.winCondition.Requirements)
        {
            float delta = Mathf.Abs(component.Percentage - cocktailFinal.GetVolumeContent(component.Spirit));
            score -= delta * 5;
        }*/
        for (int I=0; I< ScoreView.transform.childCount; I++)
        {
            ScoreView.transform.GetChild(I).gameObject.SetActive(I < score);
        }
    }

}

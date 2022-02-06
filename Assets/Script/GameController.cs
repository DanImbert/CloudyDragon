using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController main;
    public RecipeSO winCondition;
    public GameObject BarViewMenu;
    public GameObject ShelfViewMenu;
    public EndGameScreenController EndGameScreen;
    public ShelfViewController ShelfView;
    public BarViewController BarView;

    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        GoToShelfView();
    }

    public void GoToBarView()
    {
        ChangeView(Window.bar);
    }
    public void GoToShelfView()
    {
        ChangeView(Window.shelf);
    }
    enum Window
    {
        bar,
        shelf,
        endgame
    }
    void ChangeView(Window view)
    {
        BarViewMenu.gameObject.SetActive(view == Window.bar);
        BarView.gameObject.SetActive(view == Window.bar);
        ShelfView.gameObject.SetActive(view == Window.shelf);
        ShelfViewMenu.gameObject.SetActive(view == Window.shelf);
        ShelfViewMenu.gameObject.SetActive(view == Window.shelf);
        EndGameScreen.gameObject.SetActive(view == Window.endgame);

        switch (view)
        {
            case Window.shelf:
                CameraController.main.MoveToPosition(ShelfView.transform);
                break;
            case Window.bar:
            case Window.endgame:
                CameraController.main.MoveToPosition(BarView.transform);
                break;
        }
    }
    public void EndTheGame(LIquidContainer cocktailFinal)
    {
        ChangeView(Window.endgame);
        EndGameScreen.AssessDrink(cocktailFinal);
        BarView.OnVictory();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController main;
    public RecipeSO winCondition;
    public GameObject BarViewMenu;
    public GameObject ShelfViewMenu;
    public GameObject ShakeViewMenu;
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
        ChangeView(GameState.bar);
    }
    public void GoToShelfView()
    {
        ChangeView(GameState.shelf);
    }
    public enum GameState
    {
        bar = 0,
        shelf = 1,
        shaker = 2,
        endgame = 3
    }
    public GameState state;
    void ChangeView(GameState view)
    {
        state = view;
        BarViewMenu.gameObject.SetActive(view == GameState.bar);
        BarView.gameObject.SetActive(view == GameState.bar || view == GameState.shaker);
        BarViewController.main.shaker.SetTransparent(view == GameState.bar);
        ShelfView.gameObject.SetActive(view == GameState.shelf);
        ShelfViewMenu.gameObject.SetActive(view == GameState.shelf);
        ShelfViewMenu.gameObject.SetActive(view == GameState.shelf);
        ShakeViewMenu.gameObject.SetActive(view == GameState.shaker);
        EndGameScreen.gameObject.SetActive(view == GameState.endgame);

        switch (view)
        {
            case GameState.shelf:
                CameraController.main.MoveToPosition(ShelfView.transform);
                break;
            case GameState.bar:
            case GameState.endgame:
            case GameState.shaker:
                CameraController.main.MoveToPosition(BarView.transform);
                break;
        }
    }
    public void GoToNextPhase()
    {
        if (state == GameState.shaker)
        {
            ChangeView(GameState.endgame);
            EndGameScreen.AssessDrink(BarView.shaker.GetComponent<LiquidContainer>());
            BarView.OnVictory();
        }
        else
        {
            ChangeView(GameState.shaker);
            BarView.ClearDrink();
        }
    }
}

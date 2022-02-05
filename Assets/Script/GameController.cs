using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject BarViewMenu;
    public GameObject ShelfViewMenu;
    public ShelfViewController ShelfView;
    public BarViewController BarView;

    private void Start()
    {
        GoToShelfView();
    }

    public void GoToBarView()
    {
        ChangeView(true);
    }
    public void GoToShelfView()
    {
        ChangeView(false);
    }
    void ChangeView(bool bar)
    {
        BarViewMenu.gameObject.SetActive(bar);
        BarView.gameObject.SetActive(bar);
        ShelfView.gameObject.SetActive(!bar);
        ShelfViewMenu.gameObject.SetActive(!bar);
    }
    public void EndTheGame()
    {

    }
}

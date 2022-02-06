using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfViewController : MonoBehaviour
{
    public static ShelfViewController main;
    public GameObject BottleParent;
    SelectableBottle[] Interactables;
    SelectableBottle SelectedBottle;

    private void Awake()
    {
        main = this;
        Interactables = GetComponentsInChildren<SelectableBottle>();
    }
    void Update()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (SelectedBottle == null)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray fingerRay = Camera.main.ScreenPointToRay(touch.position);
                        foreach (RaycastHit hit in Physics.RaycastAll(fingerRay, Camera.main.farClipPlane))
                        {
                            if (hit.collider.tag == "Interactable" && hit.collider.TryGetComponent<SelectableBottle>(out SelectableBottle bottle))
                            {
                                SelectBottle(bottle);
                                break;
                            }
                        }
                    }

                }
            }
            else
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        OnBottleConfirmed();
                    }
                }
            }
        }
    }
    public void SelectBottle(SelectableBottle bottle)
    {
        SelectedBottle = bottle;
        ShelfMenuController.main.ChangeText(bottle.mainLiquid == null ? "EMPTY" : bottle.mainLiquid.name);
        SelectedBottle.MoveToPosition(transform.position + transform.forward * 3 + bottle.BottleHeight * Vector3.down*.5f);
    }
    public void ClearSelection()
    {
        SelectedBottle.ResetPosition();
        ShelfMenuController.main.ClearText();
        SelectedBottle = null;
    }
    public void OnBottleConfirmed()
    {
        BarViewController.main.ChangeDrink(SelectedBottle);
        GameController.main.GoToBarView();
    }
    public void ReturnBottle(SelectableBottle bottle)
    {
        bottle.transform.SetParent(BottleParent.transform);
        bottle.ResetPosition();
    }
    private void OnDisable()
    {
        ClearSelection();
    }
}

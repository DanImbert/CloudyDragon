using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfViewController : MonoBehaviour
{
    SelectableBottle[] Interactables;
    SelectableBottle SelectedBottle;

    private void Awake()
    {
        Interactables = GetComponentsInChildren<SelectableBottle>();
    }
    void Update()
    {
            foreach (Touch touch in Input.touches)
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (touch.phase == TouchPhase.Began)
                {
                }
            }
        }
    }
    public void SelectBottle()
    {

    }
}

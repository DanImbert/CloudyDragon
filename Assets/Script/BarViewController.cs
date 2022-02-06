using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarViewController : MonoBehaviour
{
    public static BarViewController main;
    public GameObject DrinkController;
    public GameObject BottleParent;
    Animator animator;
    void Awake()
    {
        main = this;
        animator = DrinkController.GetComponent<Animator>();
    }

    bool pouring = false;
    void Update()
    {
        pouring = false;
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && (myDrink == null || !LeanTween.isTweening(myDrink.gameObject)))
        { 
                foreach (Touch touch in Input.touches)
            {
                if (touch.phase > TouchPhase.Began)
                {
                    pouring = true;
                }
            }
        }
        animator.SetBool("Pouring", pouring);
    }
    private void OnDisable()
    {
        animator.SetBool("Pouring", false);
        ClearDrink();
    }
    public void OnVictory()
    {
        animator.Play("Victory");
    }
    SelectableBottle myDrink;
    public void ChangeDrink(SelectableBottle nBottle)
    {
        myDrink = nBottle;
        Vector3 oPos = nBottle.transform.position;
        Quaternion oRot = nBottle.transform.rotation;
        myDrink.transform.SetParent(BottleParent.transform);

        myDrink.transform.position = oPos;
        myDrink.transform.rotation = oRot;

        LeanTween.cancel(myDrink.gameObject);
        LeanTween.moveLocal(myDrink.gameObject, Vector3.zero + myDrink.BottleHeight * Vector3.down, 1);
        LeanTween.rotateLocal(myDrink.gameObject, Vector3.zero, 1);
    }
    public void ClearDrink()
    {
        if (myDrink!=null)
            ShelfViewController.main.ReturnBottle(myDrink);
        myDrink = null;
    }
}

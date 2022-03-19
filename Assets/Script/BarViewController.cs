using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarViewController : MonoBehaviour
{
    public static BarViewController main;
    public GameObject DrinkController;
    public GameObject BottleParent;
    public ShakerController shaker;
    Animator animator;
    void Awake()
    {
        main = this;
        animator = DrinkController.GetComponent<Animator>();
        shaker = DrinkController.GetComponentInChildren<ShakerController>();
    }
    void Update()
    {
        bool pouring = false;
        bool shaking = false;
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (GameController.main.state == GameController.GameState.shaker)
            {
                //TODO Audio: Start shaking
                shaking = Input.touches.Length>0;
            }
            else if (myDrink == null || !LeanTween.isTweening(myDrink.gameObject))
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase > TouchPhase.Began)
                    {
                        pouring = true;
                    }
                }
            }
        }
        animator.SetBool("Pouring", pouring);
        animator.SetBool("Shaking", shaking);
    }
    private void OnDisable()
    {
        animator.SetBool("Pouring", false);
        animator.SetBool("Shaking", false);
        ClearDrink();
    }
    public void OnVictory()
    {
        //TODO Audio: Victory Sound
        animator.SetTrigger("Victory");
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
        myDrink.GetComponentInChildren<LiquidPourer>().ChangeTarget(shaker.reciever);

        LeanTween.cancel(myDrink.gameObject);
        LeanTween.moveLocal(myDrink.gameObject, Vector3.zero + myDrink.BottleHeight * Vector3.down * .5f, 1);
        LeanTween.rotateLocal(myDrink.gameObject, Vector3.zero, 1);

        myDrink.SetPourMode();
    }
    public void ClearDrink()
    {
        if (myDrink != null)
        {
            ShelfViewController.main.ReturnBottle(myDrink);
            myDrink.SetDisplayMode();
        }
        myDrink = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarViewController : MonoBehaviour
{
    public GameObject DrinkController;
    Animator animator;
    void Awake()
    {
        animator = DrinkController.GetComponent<Animator>();
    }

    bool pouring = false;
    void Update()
    {
        pouring = false;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase > TouchPhase.Began)
            {
                pouring = true;
            }
        }
        animator.SetBool("Pouring", pouring);
    }
    private void OnDisable()
    {
        animator.SetBool("Pouring", false);
    }
}

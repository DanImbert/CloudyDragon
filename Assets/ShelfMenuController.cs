using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfMenuController : MonoBehaviour
{
    public static ShelfMenuController main;
    TMPro.TextMeshProUGUI text;
    void Awake()
    {
        main = this;
        text = transform.Find("Drink Title").GetComponent<TMPro.TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        if (text!=null)
            ClearText();
    }
    public void ChangeText(string value)
    {
        text.text = value;
    }
    public void ClearText()
    {
        text.text = "";
    }
}

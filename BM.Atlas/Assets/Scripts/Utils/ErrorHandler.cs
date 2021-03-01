using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ErrorHandler : MonoBehaviour
{
    private GameObject bg;
    private GameObject text;
    private Text errorText;

    void Awake()
    {
        bg = gameObject.transform.GetChild(0).gameObject;
        text = gameObject.transform.GetChild(1).gameObject;
        errorText = text.GetComponent<Text>();
    }

    public void DisplayErrorMessage(string errorMsg) 
    {
        errorText.text = errorMsg;
        bg.SetActive(true);
        text.SetActive(true);
    }
}

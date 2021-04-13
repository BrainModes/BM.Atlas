using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class radioButtonGroup : MonoBehaviour
{

    // public Button[] buttonGroup;
    protected Button currentButton;
    protected Button previousButton;

    public void buttonGroupClick(Button thisButton){
        previousButton = currentButton;
        currentButton = thisButton;

        if (currentButton != null){
            currentButton.interactable = false;
        }
        
        if(previousButton != null){
            previousButton.interactable = true;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtonManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject infoPanel;

    public GameObject stupidQuad;

    public GameObject infoButton;

    void Start()
    {
        //make sure its hidden
        closeInfoPanel();  
    }

    public void openInfoPanel(){
        infoPanel.SetActive(true);
        stupidQuad.SetActive(true);
        infoButton.SetActive(false);
    }

    public void closeInfoPanel(){

        infoPanel.SetActive(false);
        stupidQuad.SetActive(false);
        infoButton.SetActive(true);
    }


}

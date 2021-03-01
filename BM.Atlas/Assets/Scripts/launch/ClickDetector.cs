using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    AtlasRegionsAndFunctionsManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = AtlasRegionsAndFunctionsManager.instance;
    }

    private void OnMouseDown()
    {
        manager.MouseDownOnScalp();
    }

}

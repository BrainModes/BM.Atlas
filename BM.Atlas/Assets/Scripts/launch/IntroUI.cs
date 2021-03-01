using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntroUI : MonoBehaviour
{
    public string welcomeMsg = "Welcome to the BrainAtlas!";
    
    public string clickMsg = "You can interact with the 3d brain map by using a mouse to select regions of the brain by clicking and dragging. Use the scrollwheel to zoom in and out on the brain.";
    public string touchMsg = "You can interact with the 3d brain map by tapping, dragging and pinching the brain. You can also tap to select items in the accordion menu on the left-hand side.";
    public string clickStartMsg = "Click the screen to get started.";
    public string touchStartMsg = "Tap the screen to get started.";
    private string introText;
    private Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        introText += welcomeMsg;

#if UNITY_WEBGL
        introText += "\n\n";
        introText += clickMsg;
        introText += "\n\n";
        introText += clickStartMsg;
#else
        if (Input.touchSupported) {
            introText += "\n\n";
            introText += touchMsg;
            introText += "\n\n";
            introText += touchStartMsg;
        } else {
            introText += "\n\n";
            introText += clickMsg;
            introText += "\n\n";
            introText += clickStartMsg;
        }
#endif
        txt.text = introText;

    }

}

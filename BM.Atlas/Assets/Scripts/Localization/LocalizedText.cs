using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(RTLText))]
public class LocalizedText : MonoBehaviour
{

    public string key;
    public bool isCenter = false;
    private Text theOriginalTextObject;
    private RTLText theRTLTextObject;
    private LocalizationManager localizationManager;
    private bool initialized = false;
    private bool languageToggle;
    private string lastKey;

    void Start()
    {
        localizationManager = LocalizationManager.instance;
        lastKey = key;
        languageToggle = localizationManager.GetLanguageToggle();
        theOriginalTextObject = GetComponent<Text>();
        theRTLTextObject = GetComponent<RTLText>();
        theOriginalTextObject.text = "Loading...";
    }

    void Update()
    {
        if (localizationManager.GetIsReady() && !initialized) 
        {

            Initialise();
        }

        // update value of text when the language is switched
        if (languageToggle != localizationManager.GetLanguageToggle())
        {
            updateTextTranslations();
            languageToggle = !languageToggle;
        }

        // update value of text when the key is switched
        // needed for infobox and other dynamic texts
        if (key != lastKey)
        {
            updateTextTranslations();
            lastKey = key;
        }

    }

    private void Initialise() 
    {
        updateTextTranslations();
        initialized = true;
    }

    private void updateTextTranslations()
    {
        theRTLTextObject.originalText = localizationManager.GetLocalizedValue(key);
        theRTLTextObject.convertDirection = RTL.ConvertDirection.Forward;
        theRTLTextObject.wordWrap = false;
        theRTLTextObject.wordWrapWidth = theOriginalTextObject.rectTransform.rect.width;
        theRTLTextObject.Convert();
        
        //check the original text anchor layout of the object before realigning left, right or center for RTL case
        TextAnchor textAlighnment = theOriginalTextObject.alignment;

        if (isCenter)
        {
            
            if(textAlighnment == TextAnchor.UpperLeft || textAlighnment == TextAnchor.UpperRight || textAlighnment == TextAnchor.UpperCenter){
                textAlighnment = TextAnchor.UpperCenter;
            }else if(textAlighnment == TextAnchor.LowerLeft || textAlighnment == TextAnchor.LowerRight || textAlighnment == TextAnchor.LowerCenter){
                textAlighnment = TextAnchor.LowerCenter;
            }else{
                textAlighnment = TextAnchor.MiddleCenter;
            }
            
        }
        else if (LocalizationManager.instance.GetLocalizedValue("text_anchor") == "right")
        {
            if(textAlighnment == TextAnchor.UpperLeft || textAlighnment == TextAnchor.UpperRight || textAlighnment == TextAnchor.UpperCenter){
                textAlighnment = TextAnchor.UpperRight;
                }else if(textAlighnment == TextAnchor.LowerLeft || textAlighnment == TextAnchor.LowerRight || textAlighnment == TextAnchor.LowerCenter){
                    textAlighnment = TextAnchor.LowerRight;
                }else{
                    textAlighnment = TextAnchor.MiddleRight;
                }
            
        }
        else
        {
            if(textAlighnment == TextAnchor.UpperLeft || textAlighnment == TextAnchor.UpperRight || textAlighnment == TextAnchor.UpperCenter){
                textAlighnment = TextAnchor.UpperLeft;
                }else if(textAlighnment == TextAnchor.LowerLeft || textAlighnment == TextAnchor.LowerRight || textAlighnment == TextAnchor.LowerCenter){
                textAlighnment = TextAnchor.LowerLeft;
                }else{
                    textAlighnment = TextAnchor.MiddleLeft;
            }
        }
    }

    public bool GetLanguageToggle(){

        return languageToggle;

    }
}

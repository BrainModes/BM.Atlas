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

            languageToggle = localizationManager.GetLanguageToggle();
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
        theRTLTextObject.wordWrapWidth = theOriginalTextObject.rectTransform.rect.width;
        theRTLTextObject.wordWrap = true;
        theRTLTextObject.Convert();

        //check the original text anchor layout of the object before realigning left, right or center for RTL case
        TextAnchor textAlighnment = theOriginalTextObject.alignment;
        bool textIsUpper = textAlighnment == TextAnchor.UpperLeft || textAlighnment == TextAnchor.UpperRight || textAlighnment == TextAnchor.UpperCenter;
        bool textIsLower = textAlighnment == TextAnchor.LowerLeft || textAlighnment == TextAnchor.LowerRight || textAlighnment == TextAnchor.LowerCenter;
      
        if (isCenter)
        {
            
            if(textIsUpper)
            {
                theOriginalTextObject.alignment = TextAnchor.UpperCenter;
            }else if(textIsLower)
            {
                theOriginalTextObject.alignment = TextAnchor.LowerCenter;
            }else{
                theOriginalTextObject.alignment = TextAnchor.MiddleCenter;
            }
            
        }
        else if (LocalizationManager.instance.GetLocalizedValue("text_anchor") == "right")
        {

            if (textIsUpper)
            {
                theOriginalTextObject.alignment = TextAnchor.UpperRight;
            }
            else if(textIsLower)
            {
                theOriginalTextObject.alignment = TextAnchor.LowerRight;
            }
            else
            {
                theOriginalTextObject.alignment = TextAnchor.MiddleRight;
            }

        }
        else
        {
            if(textIsUpper)
            {
                theOriginalTextObject.alignment = TextAnchor.UpperLeft;
            }else if(textIsLower){
                theOriginalTextObject.alignment = TextAnchor.LowerLeft;
            }else{
                theOriginalTextObject.alignment = TextAnchor.MiddleLeft;
            }
        }
     }

}
